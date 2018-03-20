using Gecko;
using Gecko.Events;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OlxParser
{
    public partial class mForm : Form
    {

        #region properties & fields 

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);
        private string Query { get; set; }
        private string RequestedLinkUrl { get; set; }
        private string RequestedOrderUrl { get; set; }
        private GeckoWebBrowser _browserLinkParser { get; set; }
        private GeckoWebBrowser _browserOrderLoader { get; set; }
        private DateTime LastLinkLoaded { get; set; }
        private DateTime LastOrderLoaded { get; set; }


        #endregion

        #region constructors

        public mForm()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            _browserLinkParser = new GeckoWebBrowser();
            _browserOrderLoader = new GeckoWebBrowser();

            SubscribeBrowsers();
            SetDDSettings();

            var settings = SettingsManager.GetSettings();
            txtSearch.Text = settings.SearchText;

            StopApp();
            PopulateLabels();
        }

        #endregion

        #region step functions 

        private void LoadNextPage()
        {
            if (!tmrRestarter.Enabled)
                return;

            var settings = SettingsManager.GetSettings();
            var links = settings.GetNotHandledLinks();
            PopulateLabels();

            if (links.Any())
            {
                if (string.IsNullOrEmpty(RequestedLinkUrl))
                {
                    RequestedLinkUrl = links[0];
                    AddListItem($"Navigate to {links[0]}");
                    _browserLinkParser.Navigate(links[0]);
                }
            }
            else
                AddListItem("No links to parse!");
        }

        private void LoadNextOrder()
        {
            if (!tmrRestarter.Enabled)
                return;

            var settings = SettingsManager.GetSettings();
            var links = settings.GetNotHandledOrderLinks();
            PopulateLabels();

            if (links.Any())
            {
                if (string.IsNullOrEmpty(RequestedOrderUrl))
                {
                    RequestedOrderUrl = links[0];
                    AddListItem($"Navigate to {links[0]}");
                    _browserOrderLoader.Navigate(links[0]);
                }
            }
            else
                AddListItem("No orders to parse!");
        }

        private void SearchOlx()
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                AddListItem("Please enter search text!");
            else
            {
                var settings = SettingsManager.GetSettings();

                if (!settings.Links.Any())
                {
                    txtSearch.Text = txtSearch.Text.Replace(" ", "-");
                    var url = GetUrlBySearchText(txtSearch.Text);
                    Query = Regex.Match(url, @"q-(.*)").Groups[1].Value;

                    settings.SearchText = txtSearch.Text;
                    SettingsManager.SaveSettings(settings);
                    _browserSearchLinks.Navigate(url);
                }
            }
        }

        #endregion

        #region UIEventHandlers

        private void tmrRestarter_Tick(object sender, System.EventArgs e)
        {
            var secondsToRestart = 20;

            var secondsAgo = DateTime.Now.AddSeconds(-secondsToRestart);
            if (secondsAgo > LastLinkLoaded || secondsAgo > LastOrderLoaded)
            {
                LastLinkLoaded = DateTime.Now;
                LastOrderLoaded = DateTime.Now;

                AddListItem($"{secondsToRestart} seconds ellapsed! Starting!");
                ClearCookies();
                ClearRequestedUrls();

                UnsubscribeBrowsers();
                SubscribeBrowsers();

                FetchAllDate();
            }
        }

        private void ClearRequestedUrls()
        {
            RequestedOrderUrl = null;
            RequestedLinkUrl = null;
        }

        private void FetchAllDate()
        {
            SearchOlx();
            LoadNextPage();
            LoadNextOrder();
        }

        private void btnClearStatuses_Click(object sender, EventArgs e)
        {
            lstBoxStatus.Items.Clear();
        }

        private void btnDrawPopular_Click(object sender, EventArgs e)
        {
            DrawMostPopularView();
        }

        private void nmrTop_ValueChanged(object sender, EventArgs e)
        {
            DrawMostPopularView();
        }

        private void lstBoxTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxTop.SelectedIndex == -1)
                return;

            var value = GetUrlFromLine(lstBoxTop.SelectedItem.ToString());
            if (!string.IsNullOrEmpty(value))
            {
                Process.Start("chrome.exe", value);
                Clipboard.SetText(value);
            }
        }

        private void lstBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxStatus.SelectedIndex == -1)
                return;

            var value = GetUrlFromLine(lstBoxStatus.SelectedItem.ToString());
            if (!string.IsNullOrEmpty(value))
            {
                Process.Start("chrome.exe", value);
                Clipboard.SetText(value);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartApp();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopApp();
        }

        private void btnOpenTop_Click(object sender, EventArgs e)
        {
            OpenTopList();
        }

        private void nmrStep_ValueChanged(object sender, EventArgs e)
        {
            var numeric = (NumericUpDown)sender;
            var selectedVaue = (int)numeric.Value;
            var settings = SettingsManager.GetSettings();
            SettingsManager.SaveSettings(settings);
            PopulateLabels();
        }

        private void ddSettings_DropDown(object sender, EventArgs e)
        {
            StopApp();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            var fileNames = GetExistedSettings();

            var settingToSave = txtSettingsName.Text;

            if (string.IsNullOrEmpty(settingToSave))
            {
                MessageBox.Show("Please enter the name of the setting!");
                return;
            }

            if (fileNames.Contains(settingToSave))
                MessageBox.Show("Setting with such name exists! please enter another name!");
            else
            {
                StopApp();
                SettingsManager.ActiveSettingsName = settingToSave;
                var settings = SettingsManager.GetSettings();
                settings.SearchText = txtSearch.Text;
                SettingsManager.SaveSettings(settings);


                SetDDSettings(settingToSave);
            }
        }

        private void chkDescending_CheckedChanged(object sender, EventArgs e)
        {
            DrawMostPopularView();
        }

        private void btnClearSettings_Click(object sender, EventArgs e)
        {
            SettingsManager.ClearSettings();
        }

        private void ddSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsManager.ActiveSettingsName = $"{ddSettings.Items[ddSettings.SelectedIndex]}";
            var settings = SettingsManager.GetSettings();
            btnClearStatuses.PerformClick();
            PopulateLabels();
        }

        #endregion

        #region helper functions

        private void ClearCookies()
        {
            nsICookieManager cookieMan;
            cookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            cookieMan = Xpcom.QueryInterface<nsICookieManager>(cookieMan);
            cookieMan.RemoveAll();
            AddListItem("Cookie cleared!");
        }

        private void StartApp()
        {
            tmrRestarter.Start();
            PopulateLabels();
            SubscribeBrowsers();
        }

        private void StopApp()
        {
            tmrRestarter.Stop();
            UnsubscribeBrowsers();
        }

        private void ClearLabels()
        {
            lblToParse.Text = GVars.LabelsText.LabelLinksLoaded(0, 0);
            lblOrdersLoaded.Text = GVars.LabelsText.LabelOrdersLoaded(0, 0);
            lblStatus.Text = GVars.ProgramStatuses.Stoped;
            txtSearch.Text = string.Empty;
            txtSettingsName.Text = string.Empty;
        }

        private void PopulateLabels()
        {
            var settings = SettingsManager.GetSettings();
            txtSearch.Text = settings.SearchText;

            lblStatus.Text = tmrRestarter.Enabled ? GVars.ProgramStatuses.InProgress : GVars.ProgramStatuses.Stoped;
            lblOrdersLoaded.Text = GVars.LabelsText.LabelOrdersLoaded(settings.HandledOrderLinks.Count, settings.OrderLinks.Count);
            lblToParse.Text = GVars.LabelsText.LabelLinksLoaded(settings.HandledLinks.Count, settings.Links.Count);
        }

        private void UnsubscribeBrowsers()
        {
            _browserSearchLinks.DocumentCompleted -= SearchPageLoaded;
            _browserLinkParser.DocumentCompleted -= LinkLoaded;
            _browserOrderLoader.DocumentCompleted -= OrderLoaded;
        }

        private void SubscribeBrowsers()
        {
            _browserSearchLinks.DocumentCompleted += SearchPageLoaded;
            _browserLinkParser.DocumentCompleted += LinkLoaded;
            _browserOrderLoader.DocumentCompleted += OrderLoaded;
        }

        private List<string> GetExistedSettings()
        {
            var fileNames = Directory.GetFiles(SettingsManager.GetAppDataFolder(), "*.json")
                                    .Select(Path.GetFileName)
                                    .ToList();
            return fileNames;
        }

        private void SetDDSettings(string settingName = null)
        {
            ddSettings.Items.Clear();
            var fileNames = GetExistedSettings();

            foreach (var filename in fileNames)
                ddSettings.Items.Add(filename.Replace(".json", ""));

            if (fileNames.Any())
            {
                if (!string.IsNullOrEmpty(settingName))
                {
                    var indexOfSetting = ddSettings.Items.IndexOf(settingName);
                    ddSettings.SelectedIndex = indexOfSetting;
                }
                else
                    ddSettings.SelectedIndex = 0;
            }
            else
            {
                ddSettings.Items.Add("Settings");
                SettingsManager.ActiveSettingsName = "Settings";
            }
        }

        private void AddListItem(string text)
        {
            if (lstBoxStatus.Items.Count > 200)
                for (var i = 0; i < 100; i++)
                    lstBoxStatus.Items.RemoveAt(i);

            lstBoxStatus.Items.Add(text);

            var nItems = lstBoxStatus.Height / lstBoxStatus.ItemHeight;
            lstBoxStatus.TopIndex = lstBoxStatus.Items.Count - nItems;
        }

        public void BringMainWindowToFront(IntPtr mainWindowHandle)
        {
            SetForegroundWindow(mainWindowHandle);
        }

        private void DrawMostPopularView()
        {
            var urls = GetTopUrlCounters();

            lstBoxTop.Items.Clear();
            foreach (var item in urls)
                lstBoxTop.Items.Add($"Views: {item.Count}, Url: {item.Url}");
        }

        private List<UrlCounter> GetTopUrlCounters()
        {
            var settigns = SettingsManager.GetSettings();

            var urls = settigns.UrlWithCounts;
            if (!chkDescending.Checked)
                urls = settigns.UrlWithCounts.OrderBy(u => u.Count).ToList();
            return urls.Take((int)nmrTop.Value).ToList();
        }

        private string GetUrlBySearchText(string searchText)
        {
            var uri = new Uri($"https://www.olx.ua/list/q-{searchText}");
            return uri.AbsoluteUri;
        }

        private void BuildAndSaveLinks()
        {
            var settings = SettingsManager.GetSettings();
            var url = GetUrlBySearchText(settings.SearchText);

            var links = new List<string>();
            for (var i = 1; i <= settings.LastPage; i++)
                links.Add($"{url}/?page={i}");

            settings.Links = links;

            SettingsManager.SaveSettings(settings);
            AddListItem("Links saved");
            AddListItem("Ready search step!");
        }

        private string GetUrlFromLine(string line)
        {
            var url = string.Empty;
            if (Regex.IsMatch(line, "(https://.*)"))
                url = Regex.Match(line, "(https://.*)").Groups[1].Value;
            return url;
        }

        private void OpenTopList()
        {
            var urls = GetTopUrlCounters();

            if (urls.Count() > 50)
            {
                MessageBox.Show("To many links to open!");
                return;
            }

            Process process = new Process();
            process.StartInfo.FileName = @"chrome.exe";

            for (int i = 0; i < urls.Count(); i++)
            {
                if (i == 0)
                    process.StartInfo.Arguments = urls[0].Url + " --new-window";
                else
                    process.StartInfo.Arguments = urls[i].Url;
                process.Start();

                if (i == 0)
                    BringMainWindowToFront(process.Handle);
            }
        }

        #endregion

        #region BrowserDocumentCompletedEvents

        private void SearchPageLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var settings = SettingsManager.GetSettings();
            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            var lastPage = 0;
            htmlDoc.LoadHtml(pageData);

            var url = e.Uri.AbsoluteUri;
            txtUrl.Text = url;

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (hrefValue.Contains(Query) && hrefValue.Contains("?page="))
                {
                    var match = Convert.ToInt32(Regex.Match(hrefValue, GVars.Regex.Page).Groups[1].Value);
                    if (lastPage < match)
                        lastPage = match;
                }
            }
            settings.LastPage = lastPage;
            SettingsManager.SaveSettings(settings);
            BuildAndSaveLinks();
        }

        private void LinkLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var settings = SettingsManager.GetSettings();
            var url = e.Uri.AbsoluteUri;
            txtUrl.Text = url;

            if (!url.Contains("/q-"))
                return;

            AddListItem($"Link loaded - {url}");

            if (settings.HandledLinks.Contains(url))
            {
                if (!settings.Links.Contains(url))
                    settings.Links.Add(url);

                RequestedLinkUrl = null;
                FetchAllDate();
                return;
            }

            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlDoc.LoadHtml(pageData);
            foreach (var link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                var hrefValue = link.GetAttributeValue("href", string.Empty);
                if (!hrefValue.Contains(GVars.Urls.OrderUrl1) || !hrefValue.Contains(GVars.Urls.OrderUrl2)) continue;

                var index1 = hrefValue.IndexOf("#", StringComparison.Ordinal);
                if (index1 > 0)
                    hrefValue = hrefValue.Substring(0, index1);

                var index2 = hrefValue.IndexOf("?", StringComparison.Ordinal);
                if (index2 > 0)
                    hrefValue = hrefValue.Substring(0, index2);

                if (!settings.OrderLinks.Contains(hrefValue))
                    settings.OrderLinks.Add(hrefValue);
            }


            if (!url.Contains("?page="))
                url = $"{url}?page=1";

            settings.HandledLinks.Add(url != RequestedLinkUrl ? RequestedLinkUrl : url);
            SettingsManager.SaveSettings(settings);
            RequestedLinkUrl = null;
            LastLinkLoaded = DateTime.Now;
            FetchAllDate();
        }

        private void OrderLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var settings = SettingsManager.GetSettings();
            var url = e.Uri.AbsoluteUri;

            if (url.Contains("googleads") || url.Contains("about:blank"))
                return;


            AddListItem($"Order loaded - {url}");
            if (url.Contains("#from404"))
            {
                url = url.Replace("#from404", "");
                settings.HandledOrderLinks.Add(url);
                settings.HandledOrderLinks.Add(RequestedOrderUrl);
                SettingsManager.SaveSettings(settings);
            }

            if (settings.HandledOrderLinks.Contains(url))
            {
                if (!settings.OrderLinks.Contains(url))
                    settings.OrderLinks.Add(url);

                RequestedOrderUrl = null;
                FetchAllDate();
                return;
            }
            
            settings.HandledOrderLinks.Add(url != RequestedOrderUrl ? RequestedOrderUrl : url);

            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlDoc.LoadHtml(pageData);
            var div = htmlDoc.GetElementbyId("offerbottombar");
            var count = Convert.ToInt32(Regex.Match(div.InnerHtml, @"росмотры:<strong>(\d+)</strong>").Groups[1].Value);

            settings.UrlWithCounts.Add(new UrlCounter() { Count = count, Url = url });
            settings.UrlWithCounts = settings.UrlWithCounts.OrderByDescending(uc => uc.Count).ToList();

            SettingsManager.SaveSettings(settings);
            RequestedOrderUrl = null;
            LastOrderLoaded = DateTime.Now;
            FetchAllDate();
        }

        #endregion
    }
}

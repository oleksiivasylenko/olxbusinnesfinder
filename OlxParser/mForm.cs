using Gecko;
using Gecko.Events;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace OlxParser
{
    public partial class mForm : Form
    {
        private string Query { get; set; }
        private string Url { get; set; }
        private int LastPage { get; set; }
        private string RequestedLinkUrl { get; set; }
        private string RequestedOrderUrl { get; set; }

        public mForm()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            _browser.DocumentCompleted += PageLoaded;
            SetDDSettings();

            var settings = SettingsManager.GetSettings();
            nmrStep.Value = (int)settings.CurrentStep;
            txtSearch.Text = settings.SearchText;
            btnStop.PerformClick();
        }

        private List<string> GetExistedSettigns()
        {
            var fileNames = Directory.GetFiles(SettingsManager.GetAppDataFolder(), "*.json")
                                    .Select(Path.GetFileName)
                                    .ToList();
            return fileNames;
        }
        private void SetDDSettings(int selectedIndex = 0)
        {
            ddSettings.Items.Clear();
            var fileNames = GetExistedSettigns();

            foreach (var filename in fileNames)
                ddSettings.Items.Add(filename.Replace(".json", ""));

            if (fileNames.Any())
            {
                ddSettings.SelectedIndex = selectedIndex;
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

        private void PageLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(pageData);

            var url = e.Uri.AbsoluteUri;
            txtUrl.Text = url;

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (hrefValue.Contains(Query) && hrefValue.Contains("?page="))
                {
                    var match = Convert.ToInt32(Regex.Match(hrefValue, GVars.Regex.Page).Groups[1].Value);
                    if (LastPage < match)
                        LastPage = match;
                }
            }

            BuildAndSaveLinks();
            _browser.DocumentCompleted -= PageLoaded;
        }

        private void ClearCookies()
        {
            nsICookieManager cookieMan;
            cookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            cookieMan = Xpcom.QueryInterface<nsICookieManager>(cookieMan);
            cookieMan.RemoveAll();
            AddListItem("Cookie cleared!");
        }

        private void LinkLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var settings = SettingsManager.GetSettings();
            var url = e.Uri.AbsoluteUri;
            txtUrl.Text = url;

            AddListItem($"Link loaded - {url}");

            if (!url.Contains("/q-"))
                return;

            if (settings.HandledLinks.Contains(url))
            {
                if (!settings.Links.Contains(url))
                    settings.Links.Add(url);

                LoadNextPage();
                return;
            }

            settings.HandledOrderLinks.Add(url != RequestedLinkUrl ? RequestedLinkUrl : url);
            SettingsManager.SaveSettings(settings);

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

            settings.HandledLinks.Add(url);
            SettingsManager.SaveSettings(settings);
            LoadNextPage();
        }

        private void OrderLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var settings = SettingsManager.GetSettings();
            var url = e.Uri.AbsoluteUri;

            if (url.Contains("googleads") || url.Contains("about:blank"))
                return;

            AddListItem($"Order loaded - {url}");

            if (settings.HandledOrderLinks.Contains(url))
            {
                if (!settings.OrderLinks.Contains(url))
                    settings.OrderLinks.Add(url);

                LoadNextOrder();
                return;
            }
            settings.HandledOrderLinks.Add(url != RequestedOrderUrl ? RequestedOrderUrl : url);
            SettingsManager.SaveSettings(settings);

            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlDoc.LoadHtml(pageData);
            var div = htmlDoc.GetElementbyId("offerbottombar");
            var count = Convert.ToInt32(Regex.Match(div.InnerHtml, @"росмотры:<strong>(\d+)</strong>").Groups[1].Value);

            settings.UrlWithCounts.Add(new UrlCounter() { Count = count, Url = url });
            settings.UrlWithCounts = settings.UrlWithCounts.OrderByDescending(uc => uc.Count).ToList();

            settings.HandledOrderLinks.Add(url);
            SettingsManager.SaveSettings(settings);

            LoadNextOrder();
        }

        private void BuildAndSaveLinks()
        {
            var links = new List<string>();
            for (var i = 1; i <= LastPage; i++)
                links.Add($"{Url}/?page={i}");

            var settings = SettingsManager.GetSettings();
            settings.LastPage = LastPage;
            settings.Links = links;
            settings.CurrentStep = ProgressStep.Two_FetchOrderLinks;

            SettingsManager.SaveSettings(settings);
            AddListItem("Links saved");
            AddListItem("Ready search step!");
        }

        private void LoadNextPage()
        {
            if (!tmrRestarter.Enabled)
                return;

            Thread.Sleep(200);
            var settings = SettingsManager.GetSettings();
            var links = settings.GetNotHandledLinks();
            lblToParse.Text = GVars.LabelsText.LabelLinksLoaded(links.Count, settings.Links.Count);
            lblOrdersLoaded.Text = GVars.LabelsText.LabelOrdersLoaded(0, settings.OrderLinks.Count);

            if (links.Any())
            {
                RequestedLinkUrl = links[0];
                AddListItem($"Navigate to {links[0]}");
                _browser.Navigate(links[0]);
            }
            else
            {
                AddListItem("FetchLinksReady");
                settings.CurrentStep = ProgressStep.Three_FetchOrdersData;
                SettingsManager.SaveSettings(settings);
            }
        }

        private void GetLinks()
        {
            ClearDocumentCompletedEvents();
            _browser.DocumentCompleted += LinkLoaded;
            LoadNextPage();
        }

        private void GetViewsCount()
        {
            ClearDocumentCompletedEvents();
            _browser.DocumentCompleted += OrderLoaded;
            LoadNextOrder();
        }

        private void ClearDocumentCompletedEvents()
        {
            _browser.DocumentCompleted -= LinkLoaded;
            _browser.DocumentCompleted -= PageLoaded;
            _browser.DocumentCompleted -= OrderLoaded;
        }

        private void LoadNextOrder()
        {
            if (!tmrRestarter.Enabled)
                return;

            Thread.Sleep(100);
            var settings = SettingsManager.GetSettings();
            var links = settings.GetNotHandledOrderLinks();
            lblOrdersLoaded.Text = GVars.LabelsText.LabelOrdersLoaded(links.Count, settings.OrderLinks.Count);
            lblToParse.Text = GVars.LabelsText.LabelLinksLoaded(settings.Links.Count - settings.HandledLinks.Count, settings.Links.Count);

            if (links.Any())
            {
                RequestedOrderUrl = links[0];
                AddListItem($"Navigate to {links[0]}");
                _browser.Navigate(links[0]);
            }
            else
                AddListItem("OrderParsingReady");
        }

        private void DrawMostPopularView()
        {
            var settigns = SettingsManager.GetSettings();

            var urls = settigns.UrlWithCounts;
            if (!chkDescending.Checked)
                urls = settigns.UrlWithCounts.OrderBy(u => u.Count).ToList();

            var top = urls.Take((int)nmrTop.Value);
            lstBoxTop.Items.Clear();
            foreach (var item in top)
                lstBoxTop.Items.Add($"Viewvs: {item.Count}, Url: {item.Url}");
        }

        private void SearchOlx()
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                AddListItem("Please exter search text!");
            else
            {
                ClearDocumentCompletedEvents();
                _browser.DocumentCompleted += PageLoaded;

                txtSearch.Text = txtSearch.Text.Replace(" ", "-");
                var uri = new Uri($"https://www.olx.ua/list/q-{txtSearch.Text}");
                Url = uri.AbsoluteUri;
                Query = Regex.Match(Url, @"q-(.*)").Groups[1].Value;

                var settings = SettingsManager.GetSettings();
                settings.SearchText = txtSearch.Text;
                settings.LastSavedDate = DateTime.Now;
                SettingsManager.SaveSettings(settings);
                _browser.Navigate(uri.AbsoluteUri);
            }
        }

        private void tmrRestarter_Tick(object sender, System.EventArgs e)
        {
            var secondsToRestart = 20;
            var settings = SettingsManager.GetSettings();
            lblStatus.Text = GVars.ProgramStatuses.InProgress;

            if (settings.LastSavedDate >= DateTime.Now.AddSeconds(-secondsToRestart)) return;

            nmrStep.Value = (int)settings.CurrentStep;
            AddListItem($"{secondsToRestart} seconds ellapsed! restart!");
            ClearCookies();

            switch (settings.CurrentStep)
            {
                case ProgressStep.Three_FetchOrdersData:
                    AddListItem("restarting orders");
                    SettingsManager.SaveSettings(settings);
                    ClearDocumentCompletedEvents();
                    GetViewsCount();
                    break;
                case ProgressStep.Two_FetchOrderLinks:
                    AddListItem("restarting parse link");
                    SettingsManager.SaveSettings(settings);
                    ClearDocumentCompletedEvents();
                    GetLinks();
                    break;
                default:
                    AddListItem("restarting search");
                    SettingsManager.SaveSettings(settings);
                    ClearDocumentCompletedEvents();
                    SearchOlx();
                    break;
            }
        }

        private void tmrPageLoaded_Tick(object sender, System.EventArgs e)
        {

        }

        private void btnClearStatuses_Click(object sender, EventArgs e)
        {
            lstBoxStatus.Items.Clear();
        }

        private void nmrTop_ValueChanged(object sender, EventArgs e)
        {
            DrawMostPopularView();
        }

        private string GetUrlFromLine(string line)
        {
            var url = string.Empty;
            if (Regex.IsMatch(line, "(https://.*)"))
                url = Regex.Match(line, "(https://.*)").Groups[1].Value;
            return url;
        }

        private void btnDrawPopular_Click(object sender, EventArgs e)
        {
            DrawMostPopularView();
        }

        private void lstBoxTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxTop.SelectedIndex == -1)
                return;

            var value = GetUrlFromLine(lstBoxTop.SelectedItem.ToString());
            if (!string.IsNullOrEmpty(value))
                Clipboard.SetText(value);
        }

        private void lstBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxStatus.SelectedIndex == -1)
                return;

            var value = GetUrlFromLine(lstBoxStatus.SelectedItem.ToString());
            if (!string.IsNullOrEmpty(value))
                Clipboard.SetText(value);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrRestarter.Start();
            lblStatus.Text = GVars.ProgramStatuses.InProgress;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lblStatus.Text = GVars.ProgramStatuses.Stoped;
            tmrRestarter.Stop();
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
            txtSearch.Text = settings.SearchText;
            nmrStep.Value = (int)settings.CurrentStep;
            btnClearStatuses.PerformClick();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            var fileNames = GetExistedSettigns();

            var settingToSave = txtSettingsName.Text;

            if (string.IsNullOrEmpty(settingToSave))
            {
                MessageBox.Show("PleaseEnterNameOfTheSetting!");
                return;
            }

            if (fileNames.Contains(settingToSave))
                MessageBox.Show("Setting with such name exists! please enter another name!");
            else
            {
                btnStop.PerformClick();
                SettingsManager.ActiveSettingsName = settingToSave;
                var settings = SettingsManager.GetSettings();
                settings.SearchText = txtSearch.Text;
                SettingsManager.SaveSettings(settings);

                lblToParse.Text = GVars.LabelsText.LabelLinksLoaded(0, 0);
                lblOrdersLoaded.Text = GVars.LabelsText.LabelOrdersLoaded(0, 0);
                SetDDSettings(ddSettings.Items.Count);
            }
        }

        private void ddSettings_DropDownClosed(object sender, EventArgs e)
        {
            btnStart.PerformClick();
        }

        private void ddSettings_DropDown(object sender, EventArgs e)
        {
            btnStop.PerformClick();
        }

        private void nmrStep_ValueChanged(object sender, EventArgs e)
        {
            tmrPageLoaded.Stop();
            var numeric = (NumericUpDown)sender;
            var selectedVaue = (int)numeric.Value;
            var settings = SettingsManager.GetSettings();
            settings.CurrentStep = (ProgressStep)selectedVaue;
            SettingsManager.SaveSettings(settings);
            Thread.Sleep(5000);
            tmrPageLoaded.Start();
        }
    }
}

using Gecko;
using Gecko.Events;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
        public mForm()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            _browser.DocumentCompleted += PageLoaded;
            var settings = SettingsManager.GetSettings();

            txtSearch.Text = settings.SearchText;
        }

        private void PageLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(pageData);
            foreach(HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
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

        private void LinkLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            try
            {
                var settings = SettingsManager.GetSettings();
                var url = e.Uri.AbsoluteUri.ToString();

                if (settings.HandledLinks.Contains(url))
                {
                    settings.HandledLinks.Add(url);

                    SettingsManager.SaveSettings(settings);
                    LoadNextPage();
                    return;
                }

                var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                htmlDoc.LoadHtml(pageData);
                foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (hrefValue.Contains(GVars.Urls.OrderUrl1) && hrefValue.Contains(GVars.Urls.OrderUrl2))
                    {
                        int index1 = hrefValue.IndexOf("#");
                        if (index1 > 0)
                            hrefValue = hrefValue.Substring(0, index1);

                        int index2 = hrefValue.IndexOf("?");
                        if (index2 > 0)
                            hrefValue = hrefValue.Substring(0, index2);

                        if (!settings.OrderLinks.Contains(hrefValue))
                            settings.OrderLinks.Add(hrefValue);
                    }
                }

                
                if (!url.Contains("?page="))
                    url = $"{url}?page=1";

                settings.HandledLinks.Add(url);
                SettingsManager.SaveSettings(settings);
                LoadNextPage();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void OrderLoaded(object sender, GeckoDocumentCompletedEventArgs e)
        {
            try
            {
                var settings = SettingsManager.GetSettings();
                var url = e.Uri.AbsoluteUri.ToString();

                if (settings.HandledOrderLinks.Contains(url))
                {
                    settings.HandledOrderLinks.Add(url);
                    SettingsManager.SaveSettings(settings);
                    LoadNextOrder();
                    return;
                }

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
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BuildAndSaveLinks()
        {
            var links = new List<string>();
            for (int i = 1; i <= LastPage; i++)
                links.Add($"{Url}/?page={i}");

            var settings = SettingsManager.GetSettings();
            settings.LastPage = LastPage;
            settings.Links = links;
            settings.CurrentStep = ProgressStep.ParseOrderLinks;

            SettingsManager.SaveSettings(settings);

            MessageBox.Show("Ready search step!");
        }

        private void LoadNextPage()
        {
            Thread.Sleep(200);
            var links = SettingsManager.GetSettings().GetNotHandledLinks();
            lblToParse.Text = links.Count().ToString();

            if (links.Count() >= 1)
                _browser.Navigate(links[0]);
            else
                MessageBox.Show("FetchLinksReady");
        }

        private void btnGetLinks_Click(object sender, System.EventArgs e)
        {
            _browser.DocumentCompleted -= LinkLoaded;
            _browser.DocumentCompleted -= PageLoaded;
            _browser.DocumentCompleted -= OrderLoaded;

            _browser.DocumentCompleted += LinkLoaded;
            LoadNextPage();
        }

        private void btnGetViewsCount_Click(object sender, System.EventArgs e)
        {
            _browser.DocumentCompleted -= OrderLoaded;
            _browser.DocumentCompleted -= LinkLoaded;
            _browser.DocumentCompleted -= PageLoaded;

            _browser.DocumentCompleted += OrderLoaded;
            LoadNextOrder();

        }

        private void LoadNextOrder()
        {
            Thread.Sleep(200);
            var links = SettingsManager.GetSettings().GetNotHandledOrderLinks();
            lblToParse.Text = links.Count().ToString();

            if (links.Count() >= 1)
                _browser.Navigate(links[0]);
            else
                MessageBox.Show("OrderParsingReady");
        }

        private void btnSearchOlx_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                MessageBox.Show("Please exter search text!");
            else
            {
                txtSearch.Text = txtSearch.Text.Replace(" ", "-");
                Uri uri = new Uri($"https://www.olx.ua/list/q-{txtSearch.Text}");
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
            var settings = SettingsManager.GetSettings();

            if (settings.LastSavedDate < DateTime.Now.AddSeconds(-30))
            {
                if (settings.CurrentStep == ProgressStep.GetOrderLinks)
                {
                    SettingsManager.SaveSettings(settings);
                    btnGetViewsCount.PerformClick();
                }
                else if (settings.CurrentStep == ProgressStep.ParseOrderLinks)
                {
                    SettingsManager.SaveSettings(settings);
                    btnGetLinks.PerformClick();
                }
                else {

                }
            }
        }

        private void tmrPageLoaded_Tick(object sender, System.EventArgs e)
        {

        }
    }
}

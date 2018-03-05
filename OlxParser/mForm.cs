using Gecko;
using Gecko.Events;
using HtmlAgilityPack;
using Newtonsoft.Json;
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
        private int _currentPage = 0;
        private List<string> Links { get; set; }
        private string Query { get; set; }

        private int LastPage { get; set; }
        public mForm()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            _browser.DocumentCompleted += PageLoaded;
            //timer1.Start();
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
                    return;

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
                Thread.Sleep(2000);
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
                    return;

                var pageData = e.Window.Document.GetElementsByTagName("body")[0].InnerHtml;
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                htmlDoc.LoadHtml(pageData);
                var div = htmlDoc.GetElementbyId("offerbottombar");
                var count = Convert.ToInt32(Regex.Match(div.InnerHtml, @"росмотры:<strong>(\d+)</strong>").Groups[1].Value);

                settings.UrlWithCounts.Add(new UrlCounter() { Count = count, Url = url });
                settings.UrlWithCounts = settings.UrlWithCounts.OrderByDescending(uc => uc.Count).ToList();

                settings.HandledOrderLinks.Add(url);
                SettingsManager.SaveSettings(settings);
                Thread.Sleep(600);
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
                links.Add($"{Query}/?page={i}");

            var settigns = new Settings();
            settigns.LastPage = LastPage;
            settigns.Links = links;

            SettingsManager.SaveSettings(settigns);
        }

        private void LoadNextPage()
        {
            var links = SettingsManager.GetSettings().GetNotHandledLinks();

            if (links.Count() >= 1)
                _browser.Navigate(links[0]);
            else
                MessageBox.Show("FetchLinksReady");
        }

        private void btnGetLinks_Click(object sender, System.EventArgs e)
        {
            _browser.DocumentCompleted -= PageLoaded;
            _browser.DocumentCompleted -= OrderLoaded;
            _browser.DocumentCompleted += LinkLoaded;
            LoadNextPage();
        }

        private void btnGetViewsCount_Click(object sender, System.EventArgs e)
        {
            _browser.DocumentCompleted -= LinkLoaded;
            _browser.DocumentCompleted -= PageLoaded;
            _browser.DocumentCompleted += OrderLoaded;
            LoadNextOrder();

        }

        private void LoadNextOrder()
        {
            var links = SettingsManager.GetSettings().GetNotHandledOrderLinks();
            lblToParse.Text = links.Count().ToString();

            if (links.Count() >= 1)
                _browser.Navigate(links[0]);
            else
                MessageBox.Show("OrderParsingReady");
        }

        private void btnSearchOlx_Click(object sender, System.EventArgs e)
        {
            Uri uri = new Uri("https://www.olx.ua/list/q-бизиборд");
            Query = uri.AbsoluteUri;
            _browser.Navigate(Query);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {

        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {

        }
    }
}

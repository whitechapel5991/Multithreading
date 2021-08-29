using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait2
{
    public partial class DownloadManagerForm : Form
    {
        private readonly string Site_Folder;
        private readonly HttpClient HttpClient;
        private CancellationTokenSource CancellationTokenSource;
        private CancellationToken Token;
        public DownloadManagerForm()
        {
            InitializeComponent();
            Site_Folder = Path.Combine(Environment.CurrentDirectory, @"Temp\");

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };
            HttpClient = new HttpClient(handler);
        }

        private void addUrlButton_Click(object sender, EventArgs e)
        {
            var urlValue = this.urlTextBox.Text;
            var url = new Uri(urlValue);
            this.urlListBox.Items.Add(url);
            this.urlTextBox.Clear();
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Site_Folder))
            {
                Directory.Delete(Site_Folder, true);
            }

            Directory.CreateDirectory(Site_Folder);

            CancellationTokenSource = new CancellationTokenSource();
            Token = CancellationTokenSource.Token;
            this.cancelButton.Enabled = true;

            try
            {
                foreach (var item in this.urlListBox.Items)
                {
                    await DownloadWebsite(new Uri(item.ToString()), Token);
                }
            }
            catch (OperationCanceledException)
            {
                var formPopup = new CancellationDialogWindow();
                formPopup.Show(this);
            }

            CancellationTokenSource.Dispose();
            this.urlListBox.Items.Clear();
            this.cancelButton.Enabled = false;
        }

        private async Task DownloadWebsite(Uri url, CancellationToken token)
        {
            var filePath = CreateFilePath(url);
            string html = await HttpClient.GetStringAsync(url, token);
            await File.WriteAllTextAsync(filePath, html, token);
        }

        private string CreateFilePath(Uri url)
        {
            var fileName = $"{url.Host.Replace("/", "_").Replace(":", string.Empty)}{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.html";
            return Path.Combine(Site_Folder, fileName);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (Token.CanBeCanceled)
            {
                CancellationTokenSource.Cancel();
                CancellationTokenSource.Dispose();
            }
        }
    }
}

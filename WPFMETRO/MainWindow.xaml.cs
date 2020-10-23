using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.Specialized;

namespace WPFMETRO
{
    public partial class MainWindow : MetroWindow
    {
        Dictionary<string, string> formats = new Dictionary<string, string>();
        List<string> videoInfo = new List<string>();
        Queue queue = new Queue();
        private BackgroundWorker DownloadVideoWorker = new BackgroundWorker();
        public MainWindow()
        {
            if (Properties.Settings.Default.UseCustomLocale)
            {
                var customCulture = new CultureInfo(Properties.Settings.Default.CustomLocale);
                Thread.CurrentThread.CurrentCulture = customCulture;
                Thread.CurrentThread.CurrentUICulture = customCulture;
            }

            CheckForUpdates();
            InitializeComponent();

            VideoGrid.ItemsSource = queue.Videos;
            queue.Videos.CollectionChanged += Videos_CollectionChanged;
            PathBox.Text = Properties.Settings.Default.DefaultPath == "" ? AppDomain.CurrentDomain.BaseDirectory : Properties.Settings.Default.DefaultPath;
            DownloadVideoWorker.DoWork += new DoWorkEventHandler(DownloadVideoWorker_DoWork);
        }

        private void CheckForUpdates()
        {
            string guiVersionStr = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            int guiVersion = int.Parse(guiVersionStr.Replace(".", string.Empty));
            string lastVersionYoutubeDL = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string lastVersionGUIStr = client.DownloadString("https://ntanck.github.io/Youtube-DL-GUI/update/LATEST_VERSION");
                    int lastVersionGUI = int.Parse(lastVersionGUIStr.Replace(".", string.Empty));
                    if (lastVersionGUI > guiVersion)
                    {
                        MessageBoxResult userDialogResult = MessageBox.Show(Localization.Strings.AskToUpdateGUI + "\n" +
                        Localization.Strings.CurrentVersion + guiVersionStr + "\n" + Localization.Strings.NewVersion + lastVersionGUIStr,
                        "", MessageBoxButton.YesNo, MessageBoxImage.Question); //Asks user if he wants to update the GUI component

                        if (userDialogResult == MessageBoxResult.Yes)
                        {
                            Process.Start("https://ntanck.github.io/Youtube-DL-GUI/");
                        }
                    }
                    else
                    {
                        lastVersionYoutubeDL = client.DownloadString("https://rg3.github.io/youtube-dl/update/LATEST_VERSION");
                        if (lastVersionYoutubeDL != Properties.Settings.Default.YoutubeDLVersion)
                        {
                            if (!IsRunningAsAdmin())
                            {
                                ProcessStartInfo ytDLGUI = new ProcessStartInfo //Executes application as admin
                                {
                                    UseShellExecute = true,
                                    WorkingDirectory = Environment.CurrentDirectory,
                                    FileName = Assembly.GetExecutingAssembly().Location,
                                    Verb = "runas"
                                }; 

                                MessageBoxResult userDialogResult = MessageBox.Show(Localization.Strings.AskToUpdateYoutubeDL + "\n" + Localization.Strings.CurrentVersion + 
                                    Properties.Settings.Default.YoutubeDLVersion + "\n" + Localization.Strings.NewVersion + lastVersionYoutubeDL, "", MessageBoxButton.YesNo,
                                    MessageBoxImage.Question); //Asks user if he wants to update the y-dl binary

                                if (userDialogResult == MessageBoxResult.Yes)
                                {
                                    try
                                    {
                                        Process.Start(ytDLGUI);
                                    }
                                    catch
                                    {
                                        MessageBox.Show(Localization.Strings.UnauthorizedAccess, Localization.Strings.Error);
                                    }

                                    Application.Current.Shutdown();
                                }
                            }
                            else
                            {
                                if (lastVersionYoutubeDL != Properties.Settings.Default.YoutubeDLVersion)
                                { 
                                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\youtube-dl.exe"))
                                    {
                                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\youtube-dl.exe");
                                    }
                                    client.DownloadFile("https://yt-dl.org/downloads/" + lastVersionYoutubeDL + @"\youtube-dl.exe", AppDomain.CurrentDomain.BaseDirectory + @"\youtube-dl.exe");

                                    Properties.Settings.Default.YoutubeDLVersion = lastVersionYoutubeDL;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException || ex is WebException)
                {
                    MessageBox.Show(Localization.Strings.UnauthorizedAccess, Localization.Strings.Error);
                }
            }
        }

        private bool IsRunningAsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            UrlBox.Background = Brushes.Transparent;
            formats.Clear();
            string Url = UrlBox.Text;
            if (Properties.Settings.Default.PrioritizePlaylists && UrlBox.Text.Contains(@"&list="))
            {
                formats.Add("default", "default (mp4)");
                formats.Add("mp3", "mp3");
                formats.Add("flac", "flac");
                FiletypeBox.ItemsSource = formats;
                FiletypeBox.IsEnabled = AddToQueueButton.IsEnabled = true;
                UrlBox.Background = Brushes.DarkGreen;
            }
            else
            {
                UrlBox.Text = UrlBox.Text.Contains(@"&list=") ? UrlBox.Text.Substring(0, UrlBox.Text.IndexOf(@"&list=")) : UrlBox.Text;
                DownloadStatus.Text = Localization.Strings.RetrevingFormats;

                await Task.Run(() => formats=queue.GetFormats(Url));
                await Task.Run(() => videoInfo = queue.GetInfo(Url));

                if (formats.Count > 0)
                {
                    FiletypeBox.IsEnabled = AddToQueueButton.IsEnabled = true;
                    UrlBox.Background = Brushes.DarkGreen;
                    FiletypeBox.SelectedIndex = 0;
                }
                else
                {
                    UrlBox.Clear();
                    MessageBox.Show(Localization.Strings.InvalidURL, Localization.Strings.Error);
                }
                
                DownloadStatus.Text = Localization.Strings.NoDownload;
            }

            FiletypeBox.ItemsSource = formats;
        }
        private void DownloadVideoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var video in queue.Videos)
            {
                video.DownloadVideo();
            }

            this.BeginInvoke((Action)(() =>
            {
                queue.Videos.Clear();
                if (Properties.Settings.Default.AlertOnFinish)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    MessageBox.Show(Localization.Strings.Finished);
                }
            }));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void ConverterButton_Click(object sender, RoutedEventArgs e)
        {
            Converter converter = new Converter();
            converter.Show();
        }

        private void SelectLocationButton_Click(object sender, RoutedEventArgs e)
        {
            var locationDialog = new System.Windows.Forms.FolderBrowserDialog();
            locationDialog.ShowDialog();
            if (Directory.Exists(locationDialog.SelectedPath))
            {
                PathBox.Text = locationDialog.SelectedPath;
            }
        }

        private void UrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UrlBox.Background = Brushes.Transparent;
            formats.Clear();

            AddToQueueButton.IsEnabled = false;
        }

        private void ExportQueueButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog exportDialog = new SaveFileDialog();
            string queueString = "";
            exportDialog.ShowDialog();


            foreach (Video video in queue.Videos)
            {
                queueString += video.ID + Environment.NewLine;
            }
            File.WriteAllText(exportDialog.FileName, queueString);
        }

        private async void AddFromTextButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openText = new OpenFileDialog();
            openText.ShowDialog();
            string[] videos = File.ReadAllLines(openText.FileName);
            string filename = "%(title)s.%(ext)s";
            string path = PathBox.Text + "/";
            string filetype = "default";
            string title = videoInfo[0] ?? "N.A.";
            string thumbURL = videoInfo[1];
            formats.Add("default", "default (mp4)");

            foreach (string video in videos)
            {
                await Task.Run(() => formats = queue.GetFormats(video));
                await Task.Run(() => videoInfo = queue.GetInfo(video));
                queue.ModifyQueue(title, thumbURL,video, filename, path, filetype, formats);
            }
        }
        public void UpdateCard()
        {
            if (queue.Videos.Count > 0)
            {
                if (VideoGrid.SelectedItem != null)
                {
                    dynamic video = VideoGrid.SelectedItem;
                    string filename = video.Name == "%(title)s.%(ext)s" ? video.Title : video.Name;

                    TitleLabel.Text = video.Title;
                    UrlLabel.Text = video.ID;
                    foreach(var kvp in video.AvailableFormats)
                    {
                        if(kvp.Key == video.SelectedFormat)
                        {
                            FiletypeLabel.Text = kvp.Value;
                            break;
                        }
                    }
                    PathLabel.Text = video.Path;
                    FilenameLabel.Text = filename;

                    var image = new BitmapImage();

                    if (video.ThumbURL != null)
                    {
                        var imageData = new WebClient().DownloadData(new Uri(video.ThumbURL + ".jpg"));
                        var bitmapImage = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(imageData);
                        bitmapImage.EndInit();

                        image = bitmapImage;
                    }
                    else
                    {
                        using (var memory = new MemoryStream())
                        {
                            Properties.Resources.NoThumb.Save(memory, ImageFormat.Png);
                            memory.Position = 0;

                            image.BeginInit();
                            image.StreamSource = memory;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.EndInit();
                            image.Freeze();
                        }
                    }

                    Thumbnail.Source = image;
                }
            }
        }

        public void ClearCard()
        {
            Thumbnail.Source = null;
            TitleLabel.Text = UrlLabel.Text = "";
            FiletypeLabel.Text = PathLabel.Text = FilenameLabel.Text = "";
        }

        private void AddToQueueButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = UrlBox.Text;
            string filename = UseVideoTitleBox.IsChecked == true ? "%(title)s.%(ext)s" : FilenameBox.Text + ".%(ext)s"; //Get around nullable bool state
            string path = PathBox.Text + @"\";
            string filetype = formats.FirstOrDefault(x => x.Value == FiletypeBox.Text).Key;
            string title = videoInfo[0];
            string thumbURL = videoInfo[1] == "N.A" ? null : videoInfo[1];

            DownloadStatus.Text = Localization.Strings.GettingTitle;

            queue.ModifyQueue(title, thumbURL, ID, filename, path, filetype, formats);

            UrlBox.Clear();
            formats.Clear();
            FiletypeBox.IsEnabled = false;
        }
        private void Videos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            DownloadStatus.Text = Localization.Strings.NoDownload;
            DownloadButton.IsEnabled = queue.Videos.Count > 0;
        }

        private void VideoGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCard();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.DefaultPath = PathBox.Text;
            Properties.Settings.Default.UseVideoTitle = UseVideoTitleBox.IsChecked == true;

            Properties.Settings.Default.Save();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadVideoWorker.RunWorkerAsync();
            DownloadButton.IsEnabled = false;
            AddToQueueButton.IsEnabled = false;
            AddFromTextButton.IsEnabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using MahApps.Metro.Controls;

namespace WPFMETRO
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : MetroWindow
    {
        public About()
        {
            InitializeComponent();
            GetRandomQuote();
        }

        private void GetRandomQuote()
        {
            string resource = Properties.Resources.Splash;
            List<string> lines = resource.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Random r = new Random();

            QuoteText.Content = lines[r.Next(lines.Count)];

            VersionLabel.Content = "v " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.ToString()));
            e.Handled = true;
        }

        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=taQvvuQD8DM"));
        }
    }
}

using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace WPFMETRO
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Dictionary<string, string> AvailableBackColors { get; set; } = new Dictionary<string, string>
        {
            { "#FFF",  Localization.Strings.Light},
            { "#252525", Localization.Strings.Dark }
        };

        public Dictionary<string, string> AvailableForeColors { get; set; } = new Dictionary<string, string>
        {
            { "#FFF",  Localization.Strings.Light},
            { "#000", Localization.Strings.Dark }
        };

        private Dictionary<string, string> AvailableLanguages { get; set; } = new Dictionary<string, string>
        {
            { "pt-BR",  "Português Brasileiro"},
            { "en", "English" },
            { "fi", "Suomi" }
        };

        private Dictionary<int, string> AvailableAccentColors { get; set; } = new Dictionary<int, string>
        {
            { 0, "#006bb3"},
            { 1, "#009999"},
            { 2, "#990000"},
            { 3, "#669900"},
            { 4, "#999900"},
            { 5, "#800080"},
            { 6, "#4d4d4d"}
        };

        public Settings()
        {
            InitializeComponent();
            AccentTextBox.ItemsSource = AvailableForeColors;
            StyleBox.ItemsSource = AvailableBackColors;
            LocaleNameBox.ItemsSource = AvailableLanguages;
        }

        private void StyleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.GeneralTextColor = StyleBox.SelectedValue.ToString() == "#FFF" ?  "#000" : "#FFF";
        }

        private void ColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorBox.SelectedIndex != 7)
            {
                Properties.Settings.Default.AccentColor = AvailableAccentColors[ColorBox.SelectedIndex];
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult userDialogResult = MessageBox.Show(Localization.Strings.AreYouSure, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (userDialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        File.Delete(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show(ex.Message);
                    }

                    Application.Current.Shutdown();
                }
        }
    }
}

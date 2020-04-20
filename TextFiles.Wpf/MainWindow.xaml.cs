using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TextFiles.Lib;


namespace TextFiles.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {



        const string bestandenMap = @"../../Assets/";
        readonly ReadService readService = new ReadService();
        readonly WriteService writeService = new WriteService();

        const int DEFAULT = 0;
        const int UTF8 = 1;
        const int ANSI = 2;

        List<Encoding> karakterSet = new List<Encoding> { Encoding.Default, Encoding.UTF8, Encoding.GetEncoding("iso-8859-1") };
        List<string> karakterSetNamen = new List<string> { "Default", "UTF-8", "ANSI" };
        Encoding huidigeKarakterset;

        const int INDEX_FOLDER = 0;
        const int INDEX_FILENAME = 1;
        string[] huidigBestand = new string[2];

        public MainWindow()
        {
            InitializeComponent();
        }

        void ToonMelding(string melding, bool isSucces = false)
        {
            tbkFeedback.Visibility = Visibility.Visible;
            tbkFeedback.Text = melding;
            tbkFeedback.Background = isSucces == true ?  
                new SolidColorBrush(Color.FromRgb(0, 200, 0)) : 
                new SolidColorBrush(Color.FromRgb(200, 0, 0));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tbkFeedback.Visibility = Visibility.Hidden;
                txtTekst.Text = readService.TextFileToString(bestandenMap, "Le Maquis.txt");
                huidigBestand = new string[] { bestandenMap, "Le Maquis.txt" };
                txtBestandsnaam.Text = "Le Maquis ANSI.txt";
                KoppelLijsten();
                cmbEncoding.SelectedIndex = UTF8;
            }
            catch (Exception ex)
            {
                ToonMelding(ex.Message);
            }
        }

        void KoppelLijsten()
        {
            cmbEncoding.ItemsSource = karakterSetNamen;
        }

        private void BtnLeesBestand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bestandsNaam = txtBestandsnaam.Text;
                txtTekst.Text = "";

                txtTekst.Text = readService.TextFileToString(bestandenMap, bestandsNaam);
                ToonMelding($"Bestand {bestandsNaam} werd succesvol gelezen", true);
            }
            catch (Exception ex)
            {
                ToonMelding(ex.Message);
            }
        }

        private void BtnKiesBestand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSchrijfBestand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbEncoding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            huidigeKarakterset = karakterSet[cmbEncoding.SelectedIndex];
        }

        private void btnOverschrijfBestand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tekst = txtTekst.Text;

                writeService.StringToTextFile(tekst, huidigBestand[INDEX_FOLDER], huidigBestand[INDEX_FILENAME]);
                ToonMelding($"Bestand werd succesvol overschreven in de map: {huidigBestand[INDEX_FOLDER]}", true);
            }
            catch (Exception ex)
            {
                ToonMelding(ex.Message);
            }
        }
    }
}

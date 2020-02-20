using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Threading;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for NalogSluzbenikaDialog.xaml
    /// </summary>
    public partial class KandidatiDialog : Window
    {

        private polaznik polaznik = null;
        private bool izmjena = false;
        private const string uredu = "Uredu";
        private const string otkazi = "Otkaži";
        private const string izmjeni = "Izmijeni";
        private const string obrisi = "Obriši";

        public KandidatiDialog(polaznik polaznik)
        {
            InitializeComponent();
            this.polaznik = polaznik;

            if (polaznik != null)
            {
                Button_Uredu.Content = izmjeni;
                Button_Otkazi.Content = obrisi;

                textBox_Ime.IsEnabled = false;
                textBox_Prezime.IsEnabled = false;
                textBox_Email.IsEnabled = false;
                textBox_BrojTelefona.IsEnabled = false;

                textBox_Ime.Text = polaznik.osoba.Ime;
                textBox_Prezime.Text = polaznik.osoba.Prezime;
                textBox_Email.Text = polaznik.osoba.Email;
                textBox_BrojTelefona.Text = polaznik.osoba.BrojTelefona;
            }
        }

        private void Button_Uredu_Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();
            if (!izmjena)
            {
                textBox_Ime.IsEnabled = true;
                textBox_Prezime.IsEnabled = true;
                textBox_Email.IsEnabled = true;
                textBox_BrojTelefona.IsEnabled = true;

                Button_Uredu.Content = uredu;
                Button_Otkazi.Content = otkazi;
                izmjena = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox_Ime.Text) &&
                    !string.IsNullOrEmpty(textBox_Prezime.Text) &&
                    !string.IsNullOrEmpty(textBox_Email.Text) &&
                    !string.IsNullOrEmpty(textBox_BrojTelefona.Text))
                {
                    if (polaznik != null)
                    {
                        try
                        {
                            using (var ersteModel = new ErsteModel())
                            {
                                polaznik = ersteModel.polaznici.Find(polaznik.Id) ?? new polaznik();
                                polaznik.osoba.Ime = textBox_Ime.Text;
                                polaznik.osoba.Prezime = textBox_Prezime.Text;
                                polaznik.osoba.Email = textBox_Email.Text;
                                polaznik.osoba.BrojTelefona = textBox_BrojTelefona.Text;

                                // dodati još šta ima polaznik
                                ersteModel.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("MySQL Exception: " + ex.ToString());
                        }
                    }
                    else
                    {
                        polaznik polaznik = new polaznik();
                        polaznik.osoba = new osoba();
                        polaznik.osoba.Ime = textBox_Ime.Text;
                        polaznik.osoba.Prezime = textBox_Prezime.Text;
                        polaznik.osoba.Email = textBox_Email.Text;
                        polaznik.osoba.BrojTelefona = textBox_BrojTelefona.Text;

                        try
                        {
                            using (var ersteModel = new ErsteModel())
                            {
                                ersteModel.polaznici.Add(polaznik);
                                ersteModel.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("MySQL Exception: " + ex.ToString());
                        }
                    }
                    Close();
                }
                else
                {
                    if (string.IsNullOrEmpty(textBox_Ime.Text) || string.IsNullOrEmpty(textBox_Prezime.Text)
                        || string.IsNullOrEmpty(textBox_Email.Text) || string.IsNullOrEmpty(textBox_BrojTelefona.Text))
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena.");
                        var textBoxes = grid.Children.OfType<TextBox>();
                        foreach (var t in textBoxes)
                            if (String.IsNullOrEmpty(t.Text))
                                t.BorderBrush = Brushes.Red;
                    }
                }
            }
        }

        private void ResetBorderColors()
        {
            var textBoxes = grid.Children.OfType<TextBox>();
            foreach (var t in textBoxes)
                if (!String.IsNullOrEmpty(t.Text))
                    t.ClearValue(Border.BorderBrushProperty);
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            if (polaznik != null && !izmjena)
            {
                var culture = new System.Globalization.CultureInfo("sr-Latn-RS");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati kandidata?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    ObrisiKandidata();
            }
            else
            {
                Close();
            }
        }

        private void ObrisiKandidata()
        {
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    polaznik polaznikRemove = ersteModel.polaznici.Find(polaznik.Id);
                    if (polaznikRemove?.osoba != null)
                    {
                        ersteModel.osobe.Remove(polaznikRemove.osoba);
                        ersteModel.SaveChanges();
                    }
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }
        }
    }
}

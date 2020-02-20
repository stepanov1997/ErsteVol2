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

namespace Erste.Administrator
{
    /// <summary>
    /// Interaction logic for NalogSluzbenikaDialog.xaml
    /// </summary>
    public partial class NalogSluzbenikaDialog : Window
    {

        private sluzbenik sluzbenik = null;
        private Boolean izmjena = false;
        private const string uredu = "Uredu";
        private const string otkazi = "Otkaži";
        private const string izmijeni = "Izmijeni";
        private const string obrisi = "Obriši";

        public NalogSluzbenikaDialog(sluzbenik sluzbenik)
        {
            InitializeComponent();
            this.sluzbenik = sluzbenik;
            if (sluzbenik != null)
            {
                Button1.Content = izmijeni;
                Button2.Content = obrisi;

                textBox_Ime.IsEnabled = false;
                textBox_Prezime.IsEnabled = false;
                textBox_Email.IsEnabled = false;
                textBox_BrojTelefona.IsEnabled = false;
                textBox_KorisnickoIme.IsEnabled = false;
                textBox_Lozinka.IsEnabled = false;
                textBox_LozinkaProvjera.IsEnabled = false;

                textBox_Ime.Text = sluzbenik.osoba.Ime;
                textBox_Prezime.Text = sluzbenik.osoba.Prezime;
                textBox_Email.Text = sluzbenik.osoba.Email;
                textBox_BrojTelefona.Text = sluzbenik.osoba.BrojTelefona;
                textBox_KorisnickoIme.Text = sluzbenik.KorisnickoIme;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();
            if (sluzbenik != null)
            {
                if (!izmjena)
                {
                    textBox_Ime.IsEnabled = true;
                    textBox_Prezime.IsEnabled = true;
                    textBox_Email.IsEnabled = true;
                    textBox_BrojTelefona.IsEnabled = true;
                    textBox_KorisnickoIme.IsEnabled = true;
                    textBox_Lozinka.IsEnabled = true;
                    textBox_LozinkaProvjera.IsEnabled = true;

                    Button1.Content = uredu;
                    Button2.Content = otkazi;
                    izmjena = true;
                }
                else
                {
                    if (!String.IsNullOrEmpty(textBox_Ime.Text) &&
                        !String.IsNullOrEmpty(textBox_Prezime.Text) &&
                        !String.IsNullOrEmpty(textBox_Email.Text) &&
                        !String.IsNullOrEmpty(textBox_BrojTelefona.Text) &&
                        !String.IsNullOrEmpty(textBox_KorisnickoIme.Text) &&
                        textBox_Lozinka.Password.Equals(textBox_LozinkaProvjera.Password))
                    {
                        try
                        {
                            using (var ersteModel = new ErsteModel())
                            {
                                sluzbenik = ersteModel.sluzbenici.Find(sluzbenik.Id);
                                sluzbenik.osoba.Ime = textBox_Ime.Text;
                                sluzbenik.osoba.Prezime = textBox_Prezime.Text;
                                sluzbenik.osoba.Email = textBox_Email.Text;
                                sluzbenik.osoba.BrojTelefona = textBox_BrojTelefona.Text;
                                sluzbenik.KorisnickoIme = textBox_KorisnickoIme.Text;
                                if (!String.IsNullOrEmpty(textBox_Lozinka.Password))
                                {
                                    HashGenerator hashGenerator = new HashGenerator();
                                    sluzbenik.LozinkaHash = hashGenerator.ComputeHash(textBox_Lozinka.Password);
                                }
                                ersteModel.SaveChanges();
                                MessageBox.Show("Korisnik je uspješno izmijenjen.");
                                Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("MySQL Exception: " + ex.ToString());
                        }
                    }
                    else
                    {
                        if (!textBox_Lozinka.Password.Equals(textBox_LozinkaProvjera.Password))
                            MessageBox.Show("Lozinke moraju biti iste.");
                        else
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
            else
            {
                if (!String.IsNullOrEmpty(textBox_Ime.Text) &&
                        !String.IsNullOrEmpty(textBox_Prezime.Text) &&
                        !String.IsNullOrEmpty(textBox_Email.Text) &&
                        !String.IsNullOrEmpty(textBox_BrojTelefona.Text) &&
                        !String.IsNullOrEmpty(textBox_KorisnickoIme.Text) &&
                        textBox_Lozinka.Password.Equals(textBox_LozinkaProvjera.Password))
                {
                    sluzbenik sluzbenik = new sluzbenik();
                    sluzbenik.osoba = new osoba();
                    sluzbenik.osoba.Ime = textBox_Ime.Text;
                    sluzbenik.osoba.Prezime = textBox_Prezime.Text;
                    sluzbenik.osoba.Email = textBox_Email.Text;
                    sluzbenik.osoba.BrojTelefona = textBox_BrojTelefona.Text;
                    sluzbenik.KorisnickoIme = textBox_KorisnickoIme.Text;

                    HashGenerator hashGenerator = new HashGenerator();
                    sluzbenik.LozinkaHash = hashGenerator.ComputeHash(textBox_Lozinka.Password);

                    try
                    {
                        using (var ersteModel = new ErsteModel())
                        {
                            ersteModel.sluzbenici.Add(sluzbenik);
                            ersteModel.SaveChanges();
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("MySQL Exception: " + ex.ToString());
                    }
                }
                else
                {
                    if (!textBox_Lozinka.Password.Equals(textBox_LozinkaProvjera.Password))
                        MessageBox.Show("Lozinke moraju biti iste.");
                    else
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

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (sluzbenik != null && !izmjena)
            {
                var culture = new System.Globalization.CultureInfo("sr-Latn-RS");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati službenika?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    ObrisiSluzbenika();
            }
            else
            {
                Close();
            }
        }

        private void ObrisiSluzbenika()
        {
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    sluzbenik sluzbenik_remove = ersteModel.sluzbenici.Find(sluzbenik.Id);
                    if (sluzbenik_remove.osoba != null)
                    {
                        ersteModel.osobe.Remove(sluzbenik_remove.osoba);
                        ersteModel.SaveChanges();
                    }
                }
                MessageBox.Show("Korisnik je uspješno obrisan.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }
        }
    }
}

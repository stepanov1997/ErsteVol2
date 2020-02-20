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
    /// Interaction logic for EvidencijaProfesoraDialog.xaml
    /// </summary>
    public partial class EvidencijaProfesoraDialog : Window
    {

        private profesor profesor = null;
        private Boolean izmjena = false;
        private const string uredu = "Uredu";
        private const string otkazi = "Otkaži";
        private const string izmjeni = "Izmijeni";
        private const string obrisi = "Obriši";

        public EvidencijaProfesoraDialog(profesor profesor)
        {
            InitializeComponent();
            this.profesor = profesor;

            if (profesor != null)
            {
                Button1.Content = izmjeni;
                Button2.Content = obrisi;

                textBox_Ime.IsEnabled = false;
                textBox_Prezime.IsEnabled = false;
                textBox_Email.IsEnabled = false;
                textBox_BrojTelefona.IsEnabled = false;

                textBox_Ime.Text = profesor.osoba.Ime;
                textBox_Prezime.Text = profesor.osoba.Prezime;
                textBox_Email.Text = profesor.osoba.Email;
                textBox_BrojTelefona.Text = profesor.osoba.BrojTelefona;
            }
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();

            if (profesor != null)
            {
                if (!izmjena)
                {
                    textBox_Ime.IsEnabled = true;
                    textBox_Prezime.IsEnabled = true;
                    textBox_Email.IsEnabled = true;
                    textBox_BrojTelefona.IsEnabled = true;

                    Button1.Content = uredu;
                    Button2.Content = otkazi;
                    izmjena = true;
                }
                else
                {
                    if (!String.IsNullOrEmpty(textBox_Ime.Text) &&
                        !String.IsNullOrEmpty(textBox_Prezime.Text) &&
                        !String.IsNullOrEmpty(textBox_Email.Text) &&
                        !String.IsNullOrEmpty(textBox_BrojTelefona.Text))
                    {
                        try
                        {
                            using (var ersteModel = new ErsteModel())
                            {
                                profesor = ersteModel.profesori.Find(profesor.Id);
                                profesor.osoba.Ime = textBox_Ime.Text;
                                profesor.osoba.Prezime = textBox_Prezime.Text;
                                profesor.osoba.Email = textBox_Email.Text;
                                profesor.osoba.BrojTelefona = textBox_BrojTelefona.Text;
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
                        MessageBox.Show("Sva polja moraju biti popunjena.");
                        var textBoxes = grid.Children.OfType<TextBox>();
                        foreach (var t in textBoxes)
                            if (String.IsNullOrEmpty(t.Text))
                                t.BorderBrush = Brushes.Red;
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(textBox_Ime.Text) &&
                        !String.IsNullOrEmpty(textBox_Prezime.Text) &&
                        !String.IsNullOrEmpty(textBox_Email.Text) &&
                        !String.IsNullOrEmpty(textBox_BrojTelefona.Text))
                {
                    profesor profesor = new profesor();
                    profesor.osoba = new osoba();
                    profesor.osoba.Ime = textBox_Ime.Text;
                    profesor.osoba.Prezime = textBox_Prezime.Text;
                    profesor.osoba.Email = textBox_Email.Text;
                    profesor.osoba.BrojTelefona = textBox_BrojTelefona.Text;

                    try
                    {
                        using (var ersteModel = new ErsteModel())
                        {
                            ersteModel.profesori.Add(profesor);
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
                    MessageBox.Show("Sva polja moraju biti popunjena.");
                    var textBoxes = grid.Children.OfType<TextBox>();
                    foreach (var t in textBoxes)
                        if (String.IsNullOrEmpty(t.Text))
                            t.BorderBrush = Brushes.Red;
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
            if (profesor != null && !izmjena)
            {
                var culture = new System.Globalization.CultureInfo("sr-Latn-RS");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati profesora?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    ObrisiProfesora();               
            }
            else
            {
                Close();
            }
        }

        private void ObrisiProfesora()
        {
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    profesor profesor_remove = ersteModel.profesori.Find(profesor.Id);
                    if (profesor_remove.osoba != null)
                    {
                        ersteModel.osobe.Remove(profesor_remove.osoba);
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

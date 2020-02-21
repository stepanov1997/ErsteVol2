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
        private const string uredu = "U redu";
        private const string otkazi = "Otkaži";
        private const string izmjeni = "Izmijeni";
        private const string obrisi = "Obriši";

        public EvidencijaProfesoraDialog(profesor profesor)
        {
            InitializeComponent();
            this.profesor = profesor;

            dataGridKurs.ItemsSource = null;
            dataGridKurs.Items.Clear();

            dodavanjeKursa.ItemsSource = null;
            dodavanjeKursa.Items.Clear();

            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    List<kurs> kursevi = (from k in ersteModel.kursevi
                                          where k.Vazeci == true
                                          select k).ToList();
                    foreach (kurs k in kursevi)
                    {
                        if (k.jezik != null)
                        {
                            dodavanjeKursa.Items.Add(k);
                        }
                    }


                    if (profesor != null)
                    {
                        Button1.Content = izmjeni;
                        Button2.Content = obrisi;

                        textBox_Ime.IsEnabled = false;
                        textBox_Prezime.IsEnabled = false;
                        textBox_Email.IsEnabled = false;
                        textBox_BrojTelefona.IsEnabled = false;
                        dodavanjeKursa.IsEnabled = false;
                        dataGridKurs.IsEnabled = false;

                        textBox_Ime.Text = profesor.osoba.Ime;
                        textBox_Prezime.Text = profesor.osoba.Prezime;
                        textBox_Email.Text = profesor.osoba.Email;
                        textBox_BrojTelefona.Text = profesor.osoba.BrojTelefona;

                        foreach (kurs k in profesor.kursevi)
                        {
                            dataGridKurs.Items.Add(k);
                            kurs kRemove = kursevi.Where(kurs => kurs.Id == k.Id).First();
                            if (kRemove != null)
                            {
                                dodavanjeKursa.Items.Remove(kRemove);
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Greška");
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
                    dodavanjeKursa.IsEnabled = true;
                    dataGridKurs.IsEnabled = true;

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

                                List<kurs> kurseviIzmjena = new List<kurs>();

                                foreach(kurs k in dataGridKurs.Items)
                                {
                                    kurseviIzmjena.Add(ersteModel.kursevi.Find(k.Id));
                                }

                                foreach(kurs k in kurseviIzmjena)
                                {
                                    if(profesor.kursevi.All(kurs => kurs.Id != k.Id))
                                    {
                                        profesor.kursevi.Add(k);
                                        k.profesori.Add(profesor);
                                    }
                                }

                                List<kurs> kurseviBrisanje = new List<kurs>();

                                foreach(kurs k in profesor.kursevi)
                                {
                                    if(kurseviIzmjena.All(kurs => kurs.Id != k.Id)) 
                                    {
                                        kurseviBrisanje.Add(k);
                                    }
                                }

                                foreach(kurs k in kurseviBrisanje)
                                {
                                    profesor.kursevi.Remove(k);
                                    k.profesori.Remove(profesor);
                                }

                                ersteModel.SaveChanges();

                                MessageBox.Show("Korisnik je uspješno izmijenjen.");
                                Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška");
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
                    profesor.osoba.Vazeci = true;

                    try
                    {
                        using (var ersteModel = new ErsteModel())
                        {
                            if (dataGridKurs.Items.Count > 0)
                            {
                                foreach (kurs k in dataGridKurs.Items)
                                {
                                    kurs kursDodaj = ersteModel.kursevi.Find(k.Id);
                                    profesor.kursevi.Add(kursDodaj);
                                    kursDodaj.profesori.Add(profesor);
                                }
                            }
                            ersteModel.profesori.Add(profesor);
                            ersteModel.SaveChanges();
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška");
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
                        profesor_remove.osoba.Vazeci = false;
                        ersteModel.SaveChanges();
                    }
                }
                MessageBox.Show("Korisnik je uspješno obrisan.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška");
            }
        }

        private void DodavanjeKursa_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if(e.AddedItems.Count > 0)
            {
                kurs k = e.AddedItems[0] as kurs;
                dataGridKurs.Items.Add(k);
                dodavanjeKursa.Items.Remove(k);
            }
        }

        private void DataGridKurs_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            kurs k = dataGridKurs.SelectedItem as kurs;
            dodavanjeKursa.Items.Add(k);
            dataGridKurs.Items.Remove(k);

            e.Cancel = true;
        }
    }
}

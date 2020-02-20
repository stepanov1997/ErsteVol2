using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Globalization;

namespace Erste.Administrator
{
    /// <summary>
    /// Interaction logic for EvidencijaKursaDialog.xaml
    /// </summary>
    public partial class EvidencijaKursaDialog : Window
    {
        ObservableCollection<jezik> comboBoxList = new ObservableCollection<jezik>();

        private kurs kurs = null;
        private Boolean izmjena = false;
        private const string uredu = "Uredu";
        private const string otkazi = "Otkaži";
        private const string izmjeni = "Izmijeni";
        private const string obrisi = "Obriši";

        public EvidencijaKursaDialog(kurs kurs)
        {
            InitializeComponent();

            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    var jezici = (from jezik in ersteModel.jezici
                                  select jezik).ToList();
                    foreach (var jezik in jezici)
                    {
                        comboBoxList.Add(jezik);
                    }
                    comboBox_Jezik.ItemsSource = comboBoxList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }

            this.kurs = kurs;
            if (kurs != null)
            {
                Button1.Content = izmjeni;
                Button2.Content = obrisi;

                comboBox_Jezik.IsEnabled = false;
                textBox_Nivo.IsEnabled = false;
                TimePickerOd.IsEnabled = false;
                TimePickerDo.IsEnabled = false;

                comboBox_Jezik.SelectedIndex = comboBoxList.IndexOf(kurs.jezik);
                textBox_Nivo.Text = kurs.Nivo;
                //TimePickerOd.SelectedDate = kurs.DatumOd;
                //TimePickerDo.SelectedDate = kurs.DatumDo;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();
            if (kurs != null)
            {
                if (!izmjena)
                {
                    comboBox_Jezik.IsEnabled = true;
                    textBox_Nivo.IsEnabled = true;
                    TimePickerOd.IsEnabled = true;
                    TimePickerDo.IsEnabled = true;

                    Button1.Content = uredu;
                    Button2.Content = otkazi;
                    izmjena = true;
                }
                else
                {
                    if (!String.IsNullOrEmpty(textBox_Nivo.Text) && comboBox_Jezik.SelectedIndex != -1 && TimePickerOd.SelectedDate != null && TimePickerDo.SelectedDate != null)
                    {
                        try
                        {
                            using (var ersteModel = new ErsteModel())
                            {
                                kurs = ersteModel.kursevi.Find(kurs.Id);
                                kurs.Nivo = textBox_Nivo.Text;
                                kurs.JezikId = (comboBox_Jezik.SelectedItem as jezik).Id;
                                //kurs.DatumOd = TimePickerOd.SelectedDate.Value;
                                //kurs.DatumDo = TimePickerDo.SelectedDate.Value;
                                ersteModel.SaveChanges();
                                MessageBox.Show("Kurs je uspješno izmijenjen.");
                                Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška. Pokušajte ponovo kasnije.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena.");
                        var textBoxes = grid.Children.OfType<TextBox>();
                        foreach (var t in textBoxes)
                            if (String.IsNullOrEmpty(t.Text))
                                t.BorderBrush = Brushes.Red;
                        if (TimePickerOd.SelectedDate == null)
                            TimePickerOd.BorderBrush = Brushes.Red;
                        if (TimePickerDo.SelectedDate == null)
                            TimePickerDo.BorderBrush = Brushes.Red;
                        if (comboBox_Jezik.SelectedIndex == -1)
                            comboBox_Jezik.BorderBrush = Brushes.Red;
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(textBox_Nivo.Text) && comboBox_Jezik.SelectedIndex != -1 && TimePickerOd.SelectedDate != null && TimePickerDo.SelectedDate != null)
                {
                    kurs kurs = new kurs();
                    kurs.Nivo = textBox_Nivo.Text;
                    kurs.JezikId = (comboBox_Jezik.SelectedItem as jezik).Id;
                    //kurs.DatumOd = TimePickerOd.SelectedDate.Value;
                    //kurs.DatumDo = TimePickerDo.SelectedDate.Value;

                    try
                    {
                        using (var ersteModel = new ErsteModel())
                        {
                            ersteModel.kursevi.Add(kurs);
                            ersteModel.SaveChanges();
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška. Pokušajte ponovo kasnije.");
                    }
                }
                else
                {
                    MessageBox.Show("Sva polja moraju biti popunjena.");
                    var textBoxes = grid.Children.OfType<TextBox>();
                    foreach (var t in textBoxes)
                        if (String.IsNullOrEmpty(t.Text))
                            t.BorderBrush = Brushes.Red;
                    if (TimePickerOd.SelectedDate == null)
                        TimePickerOd.BorderBrush = Brushes.Red;
                    if (TimePickerDo.SelectedDate == null)
                        TimePickerDo.BorderBrush = Brushes.Red;
                    if (comboBox_Jezik.SelectedIndex == -1)
                        comboBox_Jezik.BorderBrush = Brushes.Red;
                }
            }
        }

        private void ResetBorderColors()
        {
            var textBoxes = grid.Children.OfType<TextBox>();
            foreach (var t in textBoxes)
                if (!String.IsNullOrEmpty(t.Text))
                    t.ClearValue(Border.BorderBrushProperty);
            TimePickerOd.ClearValue(Border.BorderBrushProperty);
            TimePickerDo.ClearValue(Border.BorderBrushProperty);
            comboBox_Jezik.ClearValue(Border.BorderBrushProperty);
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            if (kurs != null && !izmjena)
            {
                var culture = new System.Globalization.CultureInfo("sr-Latn-RS");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati kurs?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    ObrisiKurs();               
            }
            else
            {
                Close();
            }
        }

        private void ObrisiKurs()
        {
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    kurs kurs_remove = ersteModel.kursevi.Find(kurs.Id);
                    ersteModel.kursevi.Remove(kurs_remove);
                    ersteModel.SaveChanges();
                }
                MessageBox.Show("Kurs je uspješno obrisan.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }
        }
    }
}

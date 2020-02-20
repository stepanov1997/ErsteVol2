using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for UpisTerminaGrupe.xaml
    /// </summary>
    public partial class UpisTerminaGrupe : Window
    {
        public grupa Grupa { set; get; }
        public string NazivGrupe { set; get; }
        public ErsteModel ErsteModel { set; get; }

        public UpisTerminaGrupe(grupa g,ErsteModel ersteModel)
        {
            InitializeComponent();
            Grupa = g;
            ErsteModel = ersteModel;
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();
            if (string.IsNullOrWhiteSpace(textBox_NazivGrupe.Text))
            {
                MessageBox.Show("Odaberite naziv grupe.");
                textBox_NazivGrupe.BorderBrush = Brushes.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(DanCombo.Text))
            {
                MessageBox.Show("Odaberite dan termina.");
                DanCombo.BorderBrush = Brushes.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(TimePickerOd.Value.ToString()) || string.IsNullOrWhiteSpace(TimePickerDo.Value.ToString()))
            {
                MessageBox.Show("Popunite termine.");
                if (string.IsNullOrWhiteSpace(TimePickerOd.Value.ToString()))
                    TimePickerOd.BorderBrush = Brushes.Red;
                else
                    TimePickerDo.BorderBrush = Brushes.Red;
                return;
            }
            Func<DateTime?, DateTime?, bool> compare = (a, b) => a?.TimeOfDay.CompareTo(b?.TimeOfDay) > 0;
            if (compare(TimePickerOd.Value, TimePickerDo.Value))
            {
                MessageBox.Show("Termin početka mora biti prije termina završetka.");
                TimePickerOd.BorderBrush = Brushes.Red;
                TimePickerDo.BorderBrush = Brushes.Red;
                return;
            }

            try
            { 
                TimeSpan @od = new TimeSpan();
                TimeSpan @do = new TimeSpan();
                string dan = "";
                if (TimePickerOd.Value == null || TimePickerDo.Value == null || DanCombo.SelectedValue == null) return;
                dan = DanCombo.Text;
                @od = TimePickerOd.Value.Value.TimeOfDay;
                @do = TimePickerDo.Value.Value.TimeOfDay;

                termin t = new termin()
                {
                    Dan = dan,
                    Od = @od,
                    Do = @do,
                    grupa = Grupa,
                    GrupaId = Grupa.Id
                };
                ErsteModel.termini.Add(t);
                Grupa.termini.Add(t);
                Grupa.Naziv = textBox_NazivGrupe.Text;
                ErsteModel.SaveChanges();
                MessageBox.Show("Uspješno ste dodali novi termin.");
            }
            catch (IOException ioException)
            {
                MessageBox.Show(ioException.StackTrace);
            }
            Close();
        }

        private void ResetBorderColors()
        {
          //  if(textBox_NazivGrupe)
            textBox_NazivGrupe.ClearValue(Border.BorderBrushProperty);
            DanCombo.ClearValue(Border.BorderBrushProperty);
            DanCombo.ClearValue(Border.BorderBrushProperty);
            DanCombo.ClearValue(Border.BorderBrushProperty);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for EvidencijaTerminaDialog.xaml
    /// </summary>
    public partial class KreiranjeTermina : Window
    {
        private readonly Action refresh;

        public KreiranjeTermina()
        {
            InitializeComponent();
        }

        public KreiranjeTermina(Action refresh) : this()
        {
            this.refresh = refresh;
        }

        private async void Button1Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DanCombo.Text))
            {
                MessageBox.Show("Odaberite dan termina.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TimePickerOd.Value.ToString()) || string.IsNullOrWhiteSpace(TimePickerDo.Value.ToString()))
            {
                MessageBox.Show("Popunite termine.");
                return;
            }
            Func<DateTime?, DateTime?, bool> compare = (a, b) => a?.TimeOfDay.CompareTo(b?.TimeOfDay) > 0;
            if (compare(TimePickerOd.Value, TimePickerDo.Value))
            {
                MessageBox.Show("Termin početka mora biti prije termina završetka.");
                return;
            }

            try
            {
                TimeSpan @od = new TimeSpan();
                TimeSpan @do = new TimeSpan();
                string dan = "";
                if (Dispatcher != null)
                    await Dispatcher.InvokeAsync(() =>
                    {
                        if (TimePickerOd.Value == null || TimePickerDo.Value == null || DanCombo.SelectedValue == null) return;
                        dan = DanCombo.Text;
                        @od = TimePickerOd.Value.Value.TimeOfDay;
                        @do = TimePickerDo.Value.Value.TimeOfDay;
                    });

                using (ErsteModel ersteModel = new ErsteModel())
                {
                    ersteModel.termini.Add(new termin()
                    {
                        Dan = dan,
                        Od = @od,
                        Do = @do,
                        grupa = null,
                        GrupaId = null
                    });
                    await ersteModel.SaveChangesAsync();
                }
                MessageBox.Show("Uspješno ste dodali novi termin.");
            }
            catch (IOException ioException)
            {
                MessageBox.Show(ioException.StackTrace);
            }

            refresh();
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(Close);
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

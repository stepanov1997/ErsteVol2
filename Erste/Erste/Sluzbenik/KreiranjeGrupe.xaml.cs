using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for EvidencijaTerminaDialog.xaml
    /// </summary>
    public partial class KreiranjeGrupe : Window
    {
        private readonly Action refresh;

        public KreiranjeGrupe()
        {
            InitializeComponent();
            InitCombos();
        }

        private void InitCombos()
        {
            using (ErsteModel ersteModel = new ErsteModel())
            {
                foreach (var naziv in ersteModel.jezici.Where(j => j.Vazeci).Select(e => e.Naziv).ToList())
                {
                    JezikCombo.Items.Add(naziv);
                }
                foreach (var nivo in ersteModel.kursevi.Where(n => n.Vazeci).Select(e => e.Nivo).Distinct().ToList())
                {
                    NivoKursaCombo.Items.Add(nivo);
                }
            }

        }

        private async void Button1Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NazivBox.Text))
            {
                MessageBox.Show("Odaberite naziv grupe.");
                return;
            }
            using (ErsteModel ersteModel = new ErsteModel())
            {
                if (await ersteModel.grupe.Where(g => g.Vazeca).AnyAsync(g => g.Naziv == NazivBox.Text))
                {
                    MessageBox.Show("Grupa sa unesenim nazivom već postoji.");
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(JezikCombo.Text))
            {
                MessageBox.Show("Odaberite jezik.");
                return;
            }
            if (string.IsNullOrWhiteSpace(NivoKursaCombo.Text))
            {
                MessageBox.Show("Odaberite nivo kursa.");
                return;
            }
            if (TimePickerOd.SelectedDate is null)
            {
                MessageBox.Show("Odaberite početak kursa.");
                return;
            }
            if (TimePickerDo.SelectedDate is null)
            {
                MessageBox.Show("Odaberite kraj kursa.");
                return;
            }
            Func<DateTime?, DateTime?, bool> compare = (a, b) => a?.CompareTo(b) > 0;
            if (compare(TimePickerOd.SelectedDate, TimePickerDo.SelectedDate))
            {
                MessageBox.Show("Datum početka mora biti prije datuma završetka.");
                return;
            }

            try
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    ersteModel.grupe.Add(new grupa()
                    {
                        Naziv = NazivBox.Text,
                        BrojClanova = 0,
                        DatumOd = TimePickerOd.SelectedDate.Value,
                        DatumDo = TimePickerDo.SelectedDate.Value,
                        kurs = new kurs()
                        {
                            jezik = await ersteModel.jezici.Where(j => j.Vazeci).FirstAsync(j => j.Naziv == JezikCombo.Text),
                            Nivo = NivoKursaCombo.Text,
                            Vazeci = true
                        },
                        Vazeca = true
                    });
                    await ersteModel.SaveChangesAsync();
                }
                MessageBox.Show("Uspješno ste dodali novu grupu.");
            }
            catch (IOException ioException)
            {
                MessageBox.Show("Greška.");
            }

            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(Close);
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

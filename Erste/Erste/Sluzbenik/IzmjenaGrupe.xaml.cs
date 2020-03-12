using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for IzmjenaGrupe.xaml
    /// </summary>
    public partial class IzmjenaGrupe : Window
    {
        private readonly int idGrupe;
        private readonly Action refresh;

        public IzmjenaGrupe(int idGrupe)
        {
            this.idGrupe = idGrupe;
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

                grupa grupa = ersteModel.grupe.Find(idGrupe);
                JezikCombo.SelectedValue = ersteModel.jezici.Find(ersteModel.kursevi.Find(grupa.KursId).JezikId).Naziv;

                foreach (var nivo in ersteModel.kursevi.Where(n => n.Vazeci).Select(e => e.Nivo).Distinct().ToList())
                {
                    NivoKursaCombo.Items.Add(nivo);
                }
                NivoKursaCombo.SelectedValue = ersteModel.kursevi.Find(grupa.KursId)?.Nivo;

                NazivBox.Text = grupa.Naziv;
                TimePickerOd.SelectedDate = grupa.DatumOd;
                TimePickerDo.SelectedDate = grupa.DatumDo;
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
                if (await ersteModel.grupe.Where(g => g.Vazeca).AnyAsync(g => g.Id != idGrupe && g.Naziv == NazivBox.Text))
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
                    grupa grupa = ersteModel.grupe.Find(idGrupe);
                    grupa.Naziv = NazivBox.Text;
                    grupa.DatumOd = TimePickerOd.SelectedDate.Value;
                    grupa.DatumDo = TimePickerDo.SelectedDate.Value;
                    grupa.kurs = await ersteModel.kursevi.FindAsync(grupa.KursId);
                    if (grupa.kurs != null)
                    {
                        grupa.kurs.jezik = await ersteModel.jezici.Where(j => j.Vazeci)
                                                    .FirstAsync(j => j.Naziv == JezikCombo.Text);
                        grupa.kurs.Nivo = NivoKursaCombo.Text;
                        grupa.kurs.Vazeci = true;
                    }

                    grupa.Vazeca = true;
                    await ersteModel.SaveChangesAsync();
                }
                MessageBox.Show("Uspješno ste izmjenili grupu.");
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

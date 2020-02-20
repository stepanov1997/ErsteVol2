using Erste.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Org.BouncyCastle.Crypto.Digests;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for PregledTermina.xaml
    /// </summary>
    public partial class PregledTermina : Window
    {
        private TimetableItem item;
        private readonly Action refresh;

        public PregledTermina()
        {
            InitializeComponent();
        }

        public PregledTermina(TimetableItem item, Action refresh) : this()
        {
            this.item = item;
            this.refresh = refresh;
            Init();
        }

        private void Init()
        {
            DanCombo.Text = item.dan;
            TimePickerOd.Value = new DateTime(2020, 01, 01, 0,0,0) + item.vrijemeOd;
            TimePickerDo.Value = new DateTime(2020, 01, 01,0,0,0) + item.vrijemeDo;
            try
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    GrupaCombo.Items.Clear();
                    foreach (var naziv in ersteModel.grupe.Select(e => e.Naziv).ToList())
                    {
                        GrupaCombo.Items.Add(naziv);
                    }

                    if (item.GrupaId.HasValue)
                    {
                        grupa find = ersteModel.grupe.Find(item.GrupaId);
                        if (find is null)
                        {
                            GrupaCombo.Items.Add("Nije dodijeljena grupa");
                            GrupaCombo.Text = "Nije dodijeljena grupa";
                        }
                        else
                        {
                            GrupaCombo.Text = find.Naziv;
                        }
                    }
                    else
                    {
                        GrupaCombo.Items.Add("Nije dodijeljena grupa");
                        GrupaCombo.Text = "Nije dodijeljena grupa";
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Greška");
            }
        }

        private async void Potvrdi_Click(object sender, RoutedEventArgs e)
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
            Func<DateTime?, DateTime?, bool> compare = (a, b) => a?.TimeOfDay.CompareTo(b?.TimeOfDay)>0;
            if (compare(TimePickerOd.Value, TimePickerDo.Value))
            {
                MessageBox.Show("Termin početka mora biti prije termina završetka.");
                return;
            }

            using (ErsteModel ersteModel = new ErsteModel())
            {
                termin termin = await ersteModel.termini.FindAsync(item.termin.Id);
                if (termin != null)
                {
                    termin.Dan = DanCombo.Text;
                    if (TimePickerOd.Value != null) 
                        termin.Od = TimePickerOd.Value.Value.TimeOfDay;
                    if (TimePickerDo.Value != null)
                        termin.Do = TimePickerDo.Value.Value.TimeOfDay;
                    if(GrupaCombo.Text!="Nije dodijeljena grupa" && !string.IsNullOrWhiteSpace(GrupaCombo.Text))
                        termin.GrupaId = (await ersteModel.grupe.FirstAsync(g => g.Naziv == GrupaCombo.Text)).Id;
                    await ersteModel.SaveChangesAsync();
                }
            }
            Close();
            refresh();
        }

        private void Button_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DodavanjeGrupe_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            KreiranjeGrupe kreiranjeGrupe = new KreiranjeGrupe();
            kreiranjeGrupe.ShowDialog();
            Init();
        }

        private async void PregledGrupe_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            string NazivGrupe = null;
            grupa grupa;
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(() =>
                {
                    NazivGrupe = GrupaCombo.SelectedItem.ToString();
                });

            if (NazivGrupe==null || NazivGrupe == "Nije dodijeljena grupa")
            {
                MessageBox.Show("Nije dodijeljena grupa.");
                return;
            }
            
            using (ErsteModel ersteModel = new ErsteModel())
            {
                //grupa = await ersteModel.grupe.FirstAsync(g => g.termini!=null && g.profesori!=null && g.polaznici!=null && g.Naziv == NazivGrupe);
                grupa = await ersteModel.grupe.FirstAsync(g => g.Naziv == NazivGrupe);
            }

            if (grupa != null && Dispatcher != null)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    PregledGrupe pregledGrupe = new PregledGrupe(grupa);
                    pregledGrupe.ShowDialog();
                });
            }
            else
            {
                if (Dispatcher != null)
                    await Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show("Odaberite grupu, pa je onda pregledajte.");
                    });
            }
        }
    }
}

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
    /// Interaction logic for UpisPolaznikaDialog.xaml
    /// </summary>
    public partial class UpisPolaznikaDialog : Window
    {
        public UpisPolaznikaDialog()
        {
            InitializeComponent();
            chb_Nivo.IsEnabled = false;
            loadChb();
        }

        private void loadChb()
        {

            using (var ersteModel = new ErsteModel())
            {
                var jezici = (from j in ersteModel.jezici select j).Where(j => j.Vazeci).Select(j => j.Naziv);

                foreach (string naziv in jezici)
                    chb_Jezik.Items.Add(naziv);

            }
        }

        private void btn_PrikaziGrupe_Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();
            int odabraniJezikInt = chb_Jezik.SelectedIndex;
            int odabraniNivoInt = chb_Nivo.SelectedIndex;

            if (odabraniJezikInt < 0 || odabraniNivoInt < 0)
            {
                MessageBox.Show("Odaberite jezik i nivo kursa za prikaz grupa.");
                chb_Jezik.BorderBrush = Brushes.Red;
                chb_Nivo.BorderBrush = Brushes.Red;

                return;
            }
            string odabraniJezik = (string)chb_Jezik.Items.GetItemAt(odabraniJezikInt);
            string odabraniNivo = (string)chb_Nivo.Items.GetItemAt(odabraniNivoInt);



            if (string.IsNullOrEmpty(odabraniJezik))
            {
                MessageBox.Show("Odaberite jezik za prikaz grupa.");
                chb_Jezik.BorderBrush = Brushes.Red;
                return;

            }
            else if (string.IsNullOrEmpty(odabraniNivo))
            {
                MessageBox.Show("Odaberite nivo kursa za prikaz grupa.");
                chb_Nivo.BorderBrush = Brushes.Red;
                return;
            }

            using (var ersteModel = new ErsteModel())
            {

                var kursGrupa = (from g in ersteModel.grupe
                                 join k in ersteModel.kursevi on g.KursId equals k.Id
                                 join j in ersteModel.jezici on k.JezikId equals j.Id
                                 where g.BrojClanova >= 1 && g.DatumDo.CompareTo(DateTime.Now) > 0
                                 && j.Naziv.Equals(odabraniJezik) && k.Nivo.Equals(odabraniNivo)
                                 && g.Vazeca && k.Vazeci && j.Vazeci
                                 select new GrupaKursZapis
                                 {
                                     Grupa = g,
                                     Kurs = k,
                                     Jezik = j
                                 }).ToList();
                if (kursGrupa.Count == 0)
                {
                    MessageBox.Show("Nema formiranih grupa.");
                    return;
                }
                foreach (var zapis in kursGrupa)
                {
                    if (!GrupeDataGrid.Items.Contains(zapis))
                        GrupeDataGrid.Items.Add(zapis);
                }



            }
        }

        private void ResetBorderColors()
        {
            var textBoxes = grid.Children.OfType<TextBox>();
            foreach (var t in textBoxes)
                if (!String.IsNullOrEmpty(t.Text))
                    t.ClearValue(Border.BorderBrushProperty);
            chb_Jezik.ClearValue(Border.BorderBrushProperty);
            chb_Nivo.ClearValue(Border.BorderBrushProperty);
        }

        private void Apply_Btn_Click(object sender, RoutedEventArgs e)
        {
            ResetBorderColors();

            string odabraniJezik = (string)chb_Jezik.SelectedItem;
            string odabraniNivo = (string)chb_Nivo.SelectedItem;

            if (string.IsNullOrEmpty(textBox_Ime.Text) || string.IsNullOrEmpty(textBox_Prezime.Text)
                || string.IsNullOrEmpty(textBox_Email.Text) || string.IsNullOrEmpty(textBox_BrojTelefona.Text)
                    || string.IsNullOrEmpty(odabraniJezik) || string.IsNullOrWhiteSpace(odabraniNivo))
            {
                MessageBox.Show("Sva polja za unos moraju biti popunjena.");
                var textBoxes = grid.Children.OfType<TextBox>();
                foreach (var t in textBoxes)
                    if (String.IsNullOrEmpty(t.Text))
                        t.BorderBrush = Brushes.Red;
                if (string.IsNullOrEmpty(odabraniJezik))
                    chb_Jezik.BorderBrush = Brushes.Red;
                else if (string.IsNullOrWhiteSpace(odabraniNivo))
                    chb_Nivo.BorderBrush = Brushes.Red;
                return;
            }

            if (!GrupeDataGrid.Items.IsEmpty && (GrupeDataGrid.SelectedItems == null || GrupeDataGrid.SelectedItems.Count == 0))
            {
                MessageBox.Show("Izaberite grupu iz tabele.");
                return;
            }

            var ersteModel = new ErsteModel();

            osoba o = new osoba();
            o.Ime = textBox_Ime.Text;
            o.Prezime = textBox_Prezime.Text;
            o.BrojTelefona = textBox_BrojTelefona.Text;
            o.Email = textBox_Email.Text;
            o.Vazeci = true;
            polaznik p = new polaznik();
            p.osoba = o;

            if (!GrupeDataGrid.Items.IsEmpty)
            {
                GrupaKursZapis zapis = (GrupaKursZapis)GrupeDataGrid.SelectedItem;
                grupa zapisGrupa = (from g in ersteModel.grupe where g.Id == zapis.Grupa.Id select g).First();
                p.grupe.Add(zapisGrupa);
                zapisGrupa.polaznici.Add(p);

                MessageBox.Show("Uspjesno dodan polaznik.");
                ersteModel.SaveChanges();
                ersteModel.Dispose();

            }
            else
            {
                polaznik_na_cekanju pnc = new polaznik_na_cekanju();
                pnc.polaznik = p;
                pnc.Id = p.Id;

                var kursLista = (from k in ersteModel.kursevi
                                 join j in ersteModel.jezici on k.JezikId equals j.Id
                                 where k.Nivo.Equals(odabraniNivo) && j.Naziv.Equals(odabraniJezik)
                                 && k.Vazeci && j.Vazeci
                                 select k).ToList();

                kurs kurs = kursLista.First();
                IEnumerable<polaznik_na_cekanju> polazniciNaCekanjuZaTrazeniKurs = kursLista.SelectMany(k => k.polaznici_na_cekanju)
                    .Where(pn => pn.polaznik.osoba.Vazeci).ToList();

                if (polazniciNaCekanjuZaTrazeniKurs.Count() >= 2)
                {
                    //nova grupa
                    grupa g = new grupa
                    {
                        KursId = kurs.Id,
                        BrojClanova = 0,
                        Vazeca = true
                    };

                    ersteModel.SaveChanges();

                    //unos podataka o novoj grupi
                    UpisTerminaGrupe upisTermina = new UpisTerminaGrupe(g, ersteModel);
                    upisTermina.ShowDialog();
                    if (g.Naziv == null)
                    {
                        Task.Run(() => MessageBox.Show("Unesite naziv grupe."));
                        upisTermina = new UpisTerminaGrupe(g, ersteModel);
                        upisTermina.ShowDialog();
                    }

                    g = (from gr in ersteModel.grupe where gr.Id == g.Id && g.Vazeca select gr).First();

                    //dobijanje ref na polaznike i polaznika na cekanju
                    List<polaznik> polazniciNoveGrupe = new List<polaznik>();
                    polazniciNoveGrupe.Add(p);
                    foreach (polaznik_na_cekanju p_na_c in polazniciNaCekanjuZaTrazeniKurs)
                    {
                        polazniciNoveGrupe.Add(p_na_c.polaznik);
                    }

                    //brisanje korisnika na cekanju i veza s kursevima
                    foreach (polaznik_na_cekanju p_na_c in polazniciNaCekanjuZaTrazeniKurs)
                    {
                        kurs kurs_za_p_na_c = p_na_c.kursevi.First(k => k.Nivo.Equals(odabraniNivo) &&
                            k.jezik.Naziv.Equals(odabraniJezik) && k.Vazeci && k.jezik.Vazeci);
                        kurs_za_p_na_c.polaznici_na_cekanju.Remove(p_na_c);
                        p_na_c.kursevi.Remove(kurs_za_p_na_c);
                        p_na_c.polaznik.polaznik_na_cekanju = null;
                    }

                    //dodavanje polaznika u grupu
                    foreach (polaznik p_u_g in polazniciNoveGrupe)
                    {
                        p_u_g.grupe.Add(g);
                        g.polaznici.Add(p_u_g);
                    }

                    //dodavanje grupe u tabelu
                    //ersteModel.grupe.Add(g);

                    MessageBox.Show("Polaznik uspjesno ubacen u grupu.Polaznici na cekanju za odabrani kurs i nivo su takodje uspjesno ubaceni u grupu.");
                    ersteModel.SaveChanges();
                    ersteModel.Dispose();
                }
                else
                {

                    kurs.polaznici_na_cekanju.Add(pnc);
                    pnc.kursevi.Add(kurs);

                    MessageBox.Show("Polaznik dodat na listu cekanja za odabrani kurs i jezik.");
                    ersteModel.SaveChanges();
                    ersteModel.Dispose();
                }
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }

        private void Chb_Jezik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string odabraniJezik = (string)chb_Jezik.SelectedItem;
            using (var ersteModel = new ErsteModel())
            {
                chb_Nivo.IsEnabled = true;
                chb_Nivo.Items.Clear();
                var nivoi = (from k in ersteModel.kursevi
                             join j in ersteModel.jezici on
                             k.JezikId equals j.Id
                             where j.Naziv.Equals(odabraniJezik) && k.Vazeci
                             select k.Nivo).ToList();
                var distNivoi = nivoi.Distinct();
                foreach (var nivo in distNivoi)
                    chb_Nivo.Items.Add(nivo);
            }
        }


    }

    public class GrupaKursZapis
    {

        public grupa Grupa { get; set; }
        public kurs Kurs { get; set; }
        public jezik Jezik { get; set; }


    }
}

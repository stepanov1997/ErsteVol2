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
            loadChb();
        }

        private void loadChb()
        {

            using (var ersteModel = new ErsteModel())
            {
                var jezici = (from j in ersteModel.jezici select j).Select(j => j.Naziv);
                var nivoi = (from k in ersteModel.kursevi select k).Select(k => k.Nivo);

                 nivoi = nivoi.Distinct();

               foreach(string naziv in jezici)
                    chb_Jezik.Items.Add(naziv);
               foreach( string nivo in nivoi)
                    chb_Nivo.Items.Add(nivo);
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

            }else if (string.IsNullOrEmpty(odabraniNivo))
            {
                MessageBox.Show("Odaberite nivo kursa za prikaz grupa.");
                chb_Nivo.BorderBrush = Brushes.Red;
                return;
            }

            using (var ersteModel = new ErsteModel())
            {

                var kursGrupa = (from g in ersteModel.grupe join k in ersteModel.kursevi on g.KursId equals k.Id
                             join j in ersteModel.jezici on k.JezikId equals j.Id
                             where g.polaznici.Count>=3 && g.DatumDo.CompareTo(DateTime.Now) > 0
                             && j.Naziv == odabraniJezik && k.Nivo == odabraniNivo
                             select new GrupaKursZapis {
                                 Grupa = g,
                                 Kurs = k,
                                 Jezik = j 
                             }).ToList();
                if (kursGrupa.Count == 0)
                {
                    MessageBox.Show("Nema formiranih grupa.");
                    return;
                }
                GrupeDataGrid.Items.Clear();
                foreach (var zapis in kursGrupa)
                {
                    zapis.Grupa.BrojClanova = zapis.Grupa.polaznici.Count;
                    if(!GrupeDataGrid.Items.Contains(zapis))
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

        private  void Apply_Btn_Click(object sender, RoutedEventArgs e)
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
            polaznik p = new polaznik();
            p.osoba = o;

            if (!GrupeDataGrid.Items.IsEmpty)
            {
                GrupaKursZapis zapis = (GrupaKursZapis)GrupeDataGrid.SelectedItem;
                grupa zapisGrupa = (from g in ersteModel.grupe where g.Id == zapis.Grupa.Id select g).First();
                p.grupe.Add(zapisGrupa);
                zapisGrupa.polaznici.Add(p);

                MessageBox.Show("Uspjeno dodan polaznik.");
                ersteModel.SaveChanges();
                ersteModel.Dispose();
                
            }
            else
            {
                polaznik_na_cekanju pnc = new polaznik_na_cekanju();
                pnc.polaznik = p;
                pnc.Id = p.Id;

                // RAZMISLI O OVOME, KAKO MAPIRATI POLAZNIKE PO KURSEVIMA RAZLICITIH DATUMA
                var kursLista = (from k in ersteModel.kursevi
                                 join j in ersteModel.jezici on k.JezikId equals j.Id
                                 where k.Nivo.Equals(odabraniNivo) && j.Naziv.Equals(odabraniJezik)
                                     //&& g.DatumDo.CompareTo(DateTime.Now) > 0
                                 //orderby k.DatumDo descending
                                 select k).ToList();

                kurs kurs = kursLista.First();
                IEnumerable<polaznik_na_cekanju> polazniciNaCekanjuZaTrazeniKurs = kursLista.SelectMany(k => k.polaznici_na_cekanju).ToList();
                //List<polaznik_na_cekanju> polazniciNaCekanjuZaTrazeniKurs = new List<polaznik_na_cekanju>();
                //foreach (kurs k in kursLista)
                //{
                //    polazniciNaCekanjuZaTrazeniKurs.AddRange(k.polaznici_na_cekanju);
                //}

                if (polazniciNaCekanjuZaTrazeniKurs.Count() >= 2)
                {
                    //nova grupa
                    grupa g = new grupa
                    {
                        KursId = kurs.Id,
                        BrojClanova = 0,
                    };

                    ersteModel.SaveChanges();

                    //unos podataka o novoj grupi
                    UpisTerminaGrupe upisTermina = new UpisTerminaGrupe(g,ersteModel);
                    upisTermina.ShowDialog();
                    if (g.Naziv == null)
                    {
                        Task.Run(() => MessageBox.Show("Unesite naziv grupe."));
                        upisTermina = new UpisTerminaGrupe(g,ersteModel);
                        upisTermina.ShowDialog();
                    }

                    g = (from gr in ersteModel.grupe where gr.Id == g.Id select gr).First();

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
                            k.jezik.Naziv.Equals(odabraniJezik));
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

        //private void Chb_Jezik_Selected(object sender, RoutedEventArgs e)
        //{
        //    string odabraniJezik = (string)chb_Jezik.SelectedItem;
        //    string odabraniNivo = (string)chb_Nivo.SelectedItem;

        //    if (string.IsNullOrEmpty(odabraniJezik) && string.IsNullOrWhiteSpace(odabraniNivo))
        //    {
        //        using (var ersteModel = new ErsteModel())
        //        {

        //            var kursGrupa = (from g in ersteModel.grupe
        //                             join k in ersteModel.kursevi on g.KursId equals k.Id
        //                             join j in ersteModel.jezici on k.JezikId equals j.Id
        //                             where g.BrojClanova >= 3 && k.DatumDo.CompareTo(DateTime.Now) > 0
        //                             && j.Naziv.Equals(odabraniJezik) && k.Nivo.Equals(odabraniNivo)
        //                             select new GrupaKursZapis
        //                             {
        //                                 Grupa = g,
        //                                 Kurs = k,
        //                                 Jezik = j
        //                             }).ToList();
        //            foreach (var zapis in kursGrupa)
        //            {
        //                if (!GrupeDataGrid.Items.Contains(zapis))
        //                    GrupeDataGrid.Items.Add(zapis);
        //            }

        //        }

        //    }
        //}
    }

    public class GrupaKursZapis {

        public grupa Grupa { get; set; }
        public kurs Kurs { get; set; }
        public jezik Jezik { get; set; }


    }
}

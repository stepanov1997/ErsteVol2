using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for PregledGrupe.xaml
    /// </summary>
    public partial class PregledGrupe : Window
    {
        private grupa grupa;
        private bool flag = false;

        public PregledGrupe()
        {
            InitializeComponent();
        }

        public PregledGrupe(grupa grupa) : this()
        {
            this.grupa = grupa;
            Init();
            flag = true;
        }

        private void Init()
        {
            if (grupa == null)
            {
                using (ErsteModel model = new ErsteModel())
                {
                    grupa = model.grupe.FirstOrDefault();
                }
            }
            if (grupa is null)
            {
                NazivGrupeCombo.IsEnabled = false;
                return;
            }
            using (ErsteModel ersteModel = new ErsteModel())
            {
                BrojClanovaBox.Text = $"{ersteModel.polaznici.Count(e => e.grupe.Any(g => g.Id == grupa.Id))}";
                kurs kurs = ersteModel.kursevi.First(e => e.grupe.Any(p => p.Id == grupa.Id));
                if (!(kurs is null))
                {
                    flag = false;
                    NazivGrupeCombo.Items.Clear();
                    NazivGrupeCombo.ItemsSource = null;
                    var grupe = ersteModel.grupe.Select(e => e.Naziv).ToList();
                    foreach (var naziv in grupe)
                    {
                        NazivGrupeCombo.Items.Add(naziv);
                    }
                    NazivGrupeCombo.SelectedIndex = grupe.IndexOf(grupa.Naziv);
                    flag = true;
                    NivoKursa.Text = $"{kurs.Nivo}";
                    jezik jezik = ersteModel.jezici.Find(kurs.JezikId);
                    if (!(jezik is null))
                        jezikKursa.Text = $"{jezik.Naziv}";
                    datumOd.Text = grupa.DatumOd.ToString("dd.MM.yyyy");
                    datumDo.Text = grupa.DatumDo.ToString("dd.MM.yyyy");
                }
                PopuniTermine(ersteModel);
                PopuniPolaznike(ersteModel);
                PopuniProfesore(ersteModel);
                PopuniPolaznikeCombo(ersteModel);
                PopuniProfesoreCombo(ersteModel);
                PopuniTermineCombo(ersteModel);
            }
        }

        private void PopuniTermineCombo(ErsteModel ersteModel)
        {
            var termini = ersteModel.termini.Where(e => e.grupa == null).ToList();
            dodavanjeTermina.Items.Clear();
            foreach (var termin in termini)
            {
                dodavanjeTermina.Items.Add($"{termin.Dan} ({termin.Od:hh\\:mm}-{termin.Do:hh\\:mm})");
            }
        }

        private void PopuniProfesoreCombo(ErsteModel ersteModel)
        {
            //var profesori = ersteModel.profesori.Where(e => e.kursevi.Any(k => k.jezik.Naziv == jezikKursa.Text)).ToList();
            var profesori = ersteModel.profesori.ToList();
            foreach (profesor profesor in ProfesoriTable.Items)
            {
                profesori.RemoveAll(e => e.Id == profesor.Id);
            }
            flag = false;
            dodavanjeProfesora.Items.Clear();
            flag = true;
            foreach (var profesor in profesori)
            {
                dodavanjeProfesora.Items.Add($"{profesor.osoba.Ime} {profesor.osoba.Prezime} ({profesor.osoba.Email})");
            }
        }

        private void PopuniPolaznikeCombo(ErsteModel ersteModel)
        {
            string odabraniNivo = NivoKursa.Text;
            string odabraniJezik = jezikKursa.Text;
            var polazniciNaCekanju = (from p_na_c in ersteModel.polaznici_na_cekanju
                                      where p_na_c.kursevi.Any(k =>
                                          k.Nivo.Equals(odabraniNivo) && k.jezik.Naziv.Equals(odabraniJezik))
                                      select p_na_c).ToList();

            dodavanjePolaznika.Items.Clear();
            foreach (var polaznikNaCekanju in polazniciNaCekanju)
            {
                osoba osoba = polaznikNaCekanju.polaznik.osoba;
                dodavanjePolaznika.Items.Add($"{osoba.Ime} {osoba.Prezime} ({osoba.Email})");
            }
        }

        private void PopuniProfesore(ErsteModel ersteModel)
        {
            var list = ersteModel.profesori
                .Where(e => e.grupe.Any(g => g.Id == grupa.Id))
                .ToList();
            list.Sort((a, b) =>
            {
                int res;
                if ((res = string.Compare(a.osoba.Ime, b.osoba.Ime, StringComparison.Ordinal)) != 0) return res;
                return string.Compare(a.osoba.Prezime, b.osoba.Prezime, StringComparison.Ordinal);
            });
            ProfesoriTable.Items.Clear();
            ProfesoriTable.ItemsSource = null;
            foreach (var profesor in list)
            {
                if (profesor.osoba != null)
                {
                    ProfesoriTable.Items.Add(profesor);
                }
            }
        }

        private void PopuniPolaznike(ErsteModel ersteModel)
        {
            var list = ersteModel.polaznici
                .Where(e => e.grupe.Any(g => g.Id == grupa.Id))
                .ToList();
            list.Sort((a, b) =>
            {
                int res;
                if ((res = string.Compare(a.osoba.Ime, b.osoba.Ime, StringComparison.Ordinal)) != 0) return res;
                return string.Compare(a.osoba.Prezime, b.osoba.Prezime, StringComparison.Ordinal);
            });

            PolazniciTable.Items.Clear();
            PolazniciTable.ItemsSource = null;
            foreach (var polaznik in list)
            {
                if (polaznik.osoba != null)
                {
                    PolazniciTable.Items.Add(polaznik);
                }
            }
        }

        private void PopuniTermine(ErsteModel ersteModel)
        {
            var list = ersteModel.termini
                .Where(e => e.GrupaId == grupa.Id)
                .ToList();
            var newList = list.Select(e => new PrikazVremena(e.Dan, e.Od, e.Do, e)).ToList();
            newList.Sort((a, b) =>
            {
                int first = GetRedniBroj(a.Dan);
                int second = GetRedniBroj(b.Dan);
                int res;

                if ((res = first.CompareTo(second)) != 0) return res;

                if (a.Od < b.Od)
                {
                    return -1;
                }

                return 1;
            });
            flag = false;
            TerminiTable.Items.Clear();
            TerminiTable.ItemsSource = null;
            flag = true;
            foreach (var termin in newList)
            {
                TerminiTable.Items.Add(termin);
            }
        }

        private int GetRedniBroj(string dan)
        {
            switch (dan)
            {
                case "Ponedjeljak":
                    return 1;
                case "Utorak":
                    return 2;
                case "Srijeda":
                    return 3;
                case "Cetvrtak":
                case "Četvrtak":
                    return 4;
                case "Petak":
                    return 5;
                case "Subota":
                    return 6;
                default:
                    return 7;
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PromjenaGrupe_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (flag)
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    if (e.AddedItems.Count > 0)
                    {
                        string text = e.AddedItems[0].ToString();
                        grupa = ersteModel.grupe.First(g => g.Naziv == text);
                    }
                }
                Init();
            }
        }

        private void DodavanjeProfesora_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Func<string, string[]> parser = s =>
            {
                string[] result = new string[3];
                var splittedString = s.Split(' ');
                if (splittedString.Length >= 3)
                {
                    result[2] = splittedString[splittedString.Length - 1].Substring(1, splittedString[splittedString.Length - 1].Length - 2);
                    result[1] = splittedString[splittedString.Length - 2];
                    result[0] = splittedString.Take(splittedString.Length - 2).Aggregate("", (acc, x) => acc + x); ;
                }
                return result;

            };
            if (flag)
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    if (e.AddedItems.Count > 0)
                    {
                        string text = e.AddedItems[0].ToString();
                        var p = parser(text);
                        string ime = p[0];
                        string prezime = p[1];
                        string email = p[2];
                        profesor profesor = ersteModel.profesori.First(g => g.osoba.Ime == ime && g.osoba.Prezime == prezime && g.osoba.Email == email);
                        grupa grupica = ersteModel.grupe.Where(gr => gr.Id == grupa.Id).ToList().First();
                        grupica.profesori.Add(profesor);
                        profesor.grupe.Add(grupica);
                        ersteModel.SaveChanges();
                    }
                }
                Init();
                
                dodavanjeProfesora.Text = "Dodjeli profesora";
            }
        }

        private void DodavanjeTermina_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Func<string, (string, int, int, int, int)> parser = s =>
                {
                    var splittedString = s.Split(' ');
                    string date = splittedString[1];
                    var niz = date.Substring(1, date.Length - 2).Split('-');
                    string[] ar1 = niz[0].Split(':');
                    string[] ar2 = niz[1].Split(':');
                    return (splittedString[0],
                            int.Parse(ar1[0]),
                            int.Parse(ar1[1]),
                            int.Parse(ar2[0]),
                            int.Parse(ar2[1]));

                };
            if (flag)
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    if (e.AddedItems.Count > 0)
                    {
                        string text = e.AddedItems[0].ToString();
                        var p = parser(text);
                        string dan = p.Item1;
                        int sat1 = p.Item2;
                        int min1 = p.Item3;
                        int sat2 = p.Item4;
                        int min2 = p.Item5;
                        termin termin = ersteModel.termini.First(t => t.Dan == dan && t.Od.Hours == sat1 && t.Od.Minutes == min1 && t.Do.Hours == sat2 && t.Do.Minutes == min2);
                        grupa grupica = ersteModel.grupe.Where(gr => gr.Id == grupa.Id).ToList().First();
                        grupica.termini.Add(termin);
                        termin.grupa = grupica;
                        ersteModel.SaveChanges();
                        
                    }
                }
                dodavanjeTermina.Text = "Dodjeli termin";
                Init();
            }

        }

        private void DodavanjePolaznika_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Func<string, string[]> parser = s =>
            {
                string[] result = new string[3];
                var splittedString = s.Split(' ');
                if (splittedString.Length >= 3)
                {
                    result[2] = splittedString[splittedString.Length - 1].Substring(1, splittedString[splittedString.Length - 1].Length - 2);
                    result[1] = splittedString[splittedString.Length - 2];
                    result[0] = splittedString.Take(splittedString.Length - 2).Aggregate("", (acc, x) => acc + x); ;
                }
                return result;

            };
            if (flag)
            {
                using (ErsteModel ersteModel = new ErsteModel())
                {
                    if (e.AddedItems.Count > 0)
                    {
                        string text = e.AddedItems[0].ToString();
                        var p = parser(text);
                        string ime = p[0];
                        string prezime = p[1];
                        string email = p[2];
                        polaznik polaznik = ersteModel.polaznici.First(g => g.osoba.Ime == ime && g.osoba.Prezime == prezime && g.osoba.Email == email);
                        grupa grupica = ersteModel.grupe.Where(gr => gr.Id == grupa.Id).ToList().First();
                        grupica.polaznici.Add(polaznik);
                        polaznik.grupe.Add(grupica);
                        string odabraniNivo = NivoKursa.Text;
                        string odabraniJezik = jezikKursa.Text;
                        polaznik_na_cekanju p_na_c = polaznik.polaznik_na_cekanju;
                        kurs kurs_za_p_na_c = p_na_c.kursevi.First(k => k.Nivo.Equals(odabraniNivo) && k.jezik.Naziv.Equals(odabraniJezik));
                        kurs_za_p_na_c.polaznici_na_cekanju.Remove(p_na_c);
                        p_na_c.kursevi.Remove(kurs_za_p_na_c);
                        p_na_c.polaznik.polaznik_na_cekanju = null;
                        ersteModel.SaveChanges();
                    }
                }
                dodavanjePolaznika.Text = "Dodjeli polaznika";
                Init();
            }
        }

        private void OslobadjanjeTerminaButton_OnClick(object sender, RoutedEventArgs e)
        {
            PrikazVremena prikazVremena = (PrikazVremena)((Button)e.Source).DataContext;
            using (ErsteModel ersteModel = new ErsteModel())
            {
                termin st = ersteModel.termini.First(t => prikazVremena.Dan == t.Dan &&
                                                         prikazVremena.Od == t.Od &&
                                                         prikazVremena.Do == t.Do);
                st.GrupaId = null;
                ersteModel.SaveChanges();
            }
            Init();
        }

        private void uklanjanjeProfesoraButton_OnClick(object sender, RoutedEventArgs e)
        {
            profesor profa = (profesor)((Button)e.Source).DataContext;
            using (ErsteModel ersteModel = new ErsteModel())
            {
                grupa st = ersteModel.grupe.First(g => g.Id == grupa.Id);
                profesor profesor = ersteModel.profesori.Find(profa.Id);
                st.profesori.Remove(profesor);
                ersteModel.SaveChanges();
            }
            Init();
        }

        private void uklanjanjePolaznikaButton_OnClick(object sender, RoutedEventArgs e)
        {
            polaznik polaznik = (polaznik)((Button)e.Source).DataContext;
            using (ErsteModel ersteModel = new ErsteModel())
            {
                string odabraniNivo = NivoKursa.Text;
                string odabraniJezik = jezikKursa.Text;
                polaznik p = ersteModel.polaznici.Find(polaznik.Id);
                polaznik_na_cekanju p_na_c = p.polaznik_na_cekanju = new polaznik_na_cekanju(){Id=p.Id, kursevi = new List<kurs>(), polaznik = p};
                kurs kurs_za_p_na_c = ersteModel.kursevi.ToList().First(k =>  k.Nivo.Equals(odabraniNivo) && k.jezik.Naziv.Equals(odabraniJezik));
                kurs_za_p_na_c.polaznici_na_cekanju.Add(p_na_c);
                p_na_c.kursevi.Add(kurs_za_p_na_c);
                grupa grupica = ersteModel.grupe.Find(grupa.Id);
                p.grupe.Remove(grupica);
                grupica.polaznici.Remove(p);
                ersteModel.SaveChanges();
            }
            Init();
        }
    }

    class PrikazVremena
    {
        public string Dan { get; set; }
        public string OdString { get; set; }
        public string DoString { get; set; }
        public TimeSpan Od { get; set; }
        public TimeSpan Do { get; set; }

        public termin Termin { get; set; }

        public PrikazVremena(string dan, TimeSpan od, TimeSpan @do, termin termin)
        {
            termin = Termin;
            Dan = dan;
            OdString = od.ToString(@"hh\:mm");
            DoString = @do.ToString(@"hh\:mm");
            Od = od;
            Do = @do;
        }
    }
}

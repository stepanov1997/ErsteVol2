using System;
using System.Collections.Generic;
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
                kurs kurs = ersteModel.kursevi.Find(grupa.Id);
                if (!(kurs is null))
                {
                    NazivGrupeCombo.Items.Clear();
                    NazivGrupeCombo.ItemsSource = null;
                    var grupe = ersteModel.grupe.Select(e => e.Naziv).ToList();
                    foreach (var naziv in grupe)
                    {
                        NazivGrupeCombo.Items.Add(naziv);
                    }
                    //NazivGrupeCombo.Text = $"{grupa.Naziv}";
                    flag = false;
                    NazivGrupeCombo.SelectedIndex = grupe.IndexOf(grupa.Naziv);
                    flag = true;
                    NivoKursa.Text = $"{kurs.Nivo}";
                    jezik jezik = ersteModel.jezici.Find(kurs.JezikId);
                    if (!(jezik is null))
                        jezikKursa.Text = $"{jezik.Naziv}";
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
            var termini = ersteModel.termini.Where(e => e.GrupaId != grupa.Id).ToList();
            dodavanjeTermina.Items.Clear();
            foreach (var termin in termini)
            {
                dodavanjeTermina.Items.Add($"{termin.Dan} ({termin.Od:hh\\:mm}-{termin.Do:hh\\:mm})");
            }
        }

        private void PopuniProfesoreCombo(ErsteModel ersteModel)
        {
            var profesori = ersteModel.profesori.ToList();
            foreach (profesor profesor in ProfesoriTable.Items)
            {
                profesori.RemoveAll(e => e.Id == profesor.Id);
            }
            dodavanjeProfesora.Items.Clear();
            foreach (var profesor in profesori)
            {
                dodavanjeProfesora.Items.Add($"{profesor.osoba.Ime} {profesor.osoba.Prezime} ({profesor.osoba.Email})");
            }
        }

        private void PopuniPolaznikeCombo(ErsteModel ersteModel)
        {
            var polaznici = ersteModel.polaznici.ToList();
            foreach (polaznik polaznik in PolazniciTable.Items)
            {
                polaznici.RemoveAll(e => e.Id == polaznik.Id);
            }
            dodavanjePolaznika.Items.Clear();
            foreach (var polaznik in polaznici)
            {
                dodavanjePolaznika.Items.Add($"{polaznik.osoba.Ime} {polaznik.osoba.Prezime} ({polaznik.osoba.Email})");
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
            var newList = list.Select(e => new
            {
                Dan = e.Dan,
                OdString = e.Od.ToString(@"hh\:mm"),
                DoString = e.Do.ToString(@"hh\:mm"),
                Od = e.Od,
                Do = e.Do
            }).ToList();
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
            TerminiTable.Items.Clear();
            TerminiTable.ItemsSource = null;
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
                        Init();
                        NazivGrupeCombo.Text = text;
                    }
                }

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
                        Init();
                        NazivGrupeCombo.Text = text;
                    }
                }

            }
        }

        private void DodavanjeTermina_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Func<string, (string,int,int,int,int)> parser = s =>
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
                        termin termin = ersteModel.termini.First(t => t.Dan==dan && t.Od.Hours==sat1 && t.Od.Minutes == min1 && t.Do.Hours == sat2 && t.Do.Minutes == min2);
                        grupa grupica = ersteModel.grupe.Where(gr => gr.Id == grupa.Id).ToList().First();
                        grupica.termini.Add(termin);
                        termin.grupa = grupica;
                        ersteModel.SaveChanges();
                        Init();
                        NazivGrupeCombo.Text = text;
                    }
                }

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
                        ersteModel.SaveChanges();
                        Init();
                        NazivGrupeCombo.Text = text;
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Erste.Administrator
{
    /// <summary>
    /// Interaction logic for NaloziSluzbenika.xaml
    /// </summary>
    public partial class NaloziSluzbenika : UserControl
    {
        public NaloziSluzbenika()
        {
            InitializeComponent();
        }
        /*
        public void AddButtonActions(params Button[] buttons)
        {
            buttons[0].Click += (sender, args) =>
            {
                NalogSluzbenikaDialog nalogSluzbenikaDialog = new NalogSluzbenikaDialog(null);
                nalogSluzbenikaDialog.ShowDialog();

                Load_Data();
            };
            buttons[1].Click += (sender, args) =>
            {
                var dataGridSelectedItems = DataGrid.SelectedItems;
                using (var ersteModel = new ErsteModel())
                {
                    foreach (var dataGridSelectedItem in dataGridSelectedItems)
                    {
                        var sluzbenikRemove = ersteModel.sluzbenici.Find(((sluzbenik)dataGridSelectedItem).Id);
                        if (sluzbenikRemove?.osoba != null)
                        {
                            ersteModel.osobe.Remove(sluzbenikRemove.osoba);
                            ersteModel.SaveChanges();
                        }
                    }
                }
                Load_Data();
            };
        }
        */

        public void Refresh() => Load_Data();

        private void Load_Data()
        {
            Search.Text = "";
            DataGrid.Items.Clear();
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    var sluzbenici = (from sluzbenik in ersteModel.sluzbenici
                        join osoba in ersteModel.osobe on sluzbenik.Id equals osoba.Id
                        where osoba.Vazeci == true
                        select sluzbenik).ToList();

                    foreach (var sluzbenik in sluzbenici)
                    {
                        if (sluzbenik.osoba != null)
                        {
                            DataGrid.Items.Add(sluzbenik);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška");
            }
        }

        private void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            sluzbenik sluzbenik = DataGrid.SelectedItem as sluzbenik;
            NalogSluzbenikaDialog nalogSluzbenikaDialog = new NalogSluzbenikaDialog(sluzbenik);
            nalogSluzbenikaDialog.ShowDialog();

            Load_Data();
            e.Cancel = true;
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Search.Text;
            if (Dispatcher != null)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    DataGrid.Items.Clear();
                    DataGrid.ItemsSource = null;
                    DataGrid.Items.Refresh();
                });
            }


            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    var sluzbenici = (from sluzbenik in ersteModel.sluzbenici
                            join osoba in ersteModel.osobe on sluzbenik.Id equals osoba.Id
                            where osoba.Vazeci == true
                            select sluzbenik)
                        .Where(s => (s.osoba.Ime + " " + s.osoba.Prezime + " " +
                                     s.osoba.BrojTelefona + " " +
                                     s.osoba.Email + " " + s.KorisnickoIme).ToLower().Contains(text.ToLower()))
                        .ToList();

                    foreach (var sluzbenik in sluzbenici)
                    {
                        if (sluzbenik.osoba != null)
                        {
                            DataGrid.Items.Add(sluzbenik);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Dispatcher != null)
                {
                    MessageBox.Show("Greska");
                }
            }
        }
    }
}

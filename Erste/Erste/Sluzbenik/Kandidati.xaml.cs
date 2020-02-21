using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Erste.Administrator;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for NaloziSluzbenika.xaml
    /// </summary>
    public partial class Kandidati : UserControl
    {
        private String mode;
        public Kandidati(String mode)
        {
            InitializeComponent();

            this.mode = mode;
        }

        /*public Kandidati(params Button[] buttons) : this()
        {
            buttons[0].Click += (sender, args) =>
            {
                var dataGridSelectedItems = DataGrid.SelectedItems;
                using (var ersteModel = new ErsteModel())
                {
                    foreach (var dataGridSelectedItem in dataGridSelectedItems)
                    {
                        var polaznikRemove = ersteModel.polaznici.Find(((polaznik)dataGridSelectedItem).Id);
                        if (polaznikRemove?.osoba != null)
                        {
                            ersteModel.osobe.Remove(polaznikRemove.osoba);
                            ersteModel.SaveChanges();
                        }
                    }
                }
                Load_Data();
            };
            buttons[1].Click += (sender, args) =>
            {
                MessageBox.Show("Cekanje");
            };
        }*/

        /*private void PregledKandidata_DoubleClick(object sender, RoutedEventArgs e)
        {
            polaznik polaznik = DataGrid.SelectedItem as polaznik;
            KandidatiDialog kandidatiDialog = new KandidatiDialog(polaznik);
            kandidatiDialog.ShowDialog();

            Load_Data();
        }*/

        public async Task Refresh() => await Load_Data();

        private async Task Load_Data()
        {
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
                    if ("svi".Equals(mode))
                    {
                        var polaznici = await (from polaznik in ersteModel.polaznici.Include("osoba")
                                               join osoba in ersteModel.osobe.Include("polaznik") on polaznik.Id equals osoba.Id
                                               where osoba.Vazeci
                                               select polaznik).ToListAsync();

                        foreach (var polaznik in polaznici)
                        {
                            if (polaznik.osoba != null && Dispatcher != null)
                            {
                                await Dispatcher.InvokeAsync(() => { DataGrid.Items.Add(polaznik); });
                            }
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

        private  void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            polaznik polaznik = DataGrid.SelectedItem as polaznik;
            KandidatiDialog kandidatiDialog = new KandidatiDialog(polaznik);
            kandidatiDialog.ShowDialog();
            Refresh();
            e.Cancel = true;
        }
    }
}

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
using System.Windows.Shapes;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for KandidatiNaCekanju.xaml
    /// </summary>
    public partial class KandidatiNaCekanju : UserControl
    {
        public KandidatiNaCekanju()
        {
            InitializeComponent();
        }

        public async Task Refresh() => await Load_Data();

        public async Task Load_Data()
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
                    var osobeKojeCekaju = (from o in ersteModel.osobe
                        join pnc in ersteModel.polaznici_na_cekanju on o.Id equals pnc.Id
                        select new
                        {
                            Id = o.Id,
                            Ime = o.Ime,
                            Prezime = o.Prezime,
                            BrojTelefon = o.BrojTelefona,
                            Email = o.Email

                        }).ToList();

                    foreach (var o in osobeKojeCekaju)
                    {
                        await Dispatcher.InvokeAsync(() => { DataGrid.Items.Add(o); });
                    }
                }
            }
            catch (Exception ex)
            {
                if (Dispatcher != null)
                {
                    MessageBox.Show("MySQL Exception: " + ex);
                }
}

        }

        private async void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //if (Dispatcher != null)
            //    await Dispatcher.InvokeAsync(() =>
            //    {
            //        polaznik polaznik = DataGrid.SelectedItem as polaznik;
            //        KandidatiDialog kandidatiDialog = new KandidatiDialog(polaznik);
            //        kandidatiDialog.ShowDialog();
            //    });
            //await Refresh();
            //e.Cancel = true;
        }
    }
}

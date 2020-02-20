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
    /// Interaction logic for EvidencijaKurseva.xaml
    /// </summary>
    public partial class EvidencijaKurseva : UserControl
    {
        public EvidencijaKurseva()
        {
            InitializeComponent();
        }     

        public void AddButtonActions(params Button[] buttons)
        {
            buttons[0].Click += (sender, args) =>
            {
                EvidencijaKursaDialog evidencijaKursaDialog = new EvidencijaKursaDialog(null);
                evidencijaKursaDialog.ShowDialog();

                Load_Data();
            };
            buttons[1].Click += (sender, args) =>
            {
                var dataGridSelectedItems = DataGrid.SelectedItems;
                using (var ersteModel = new ErsteModel())
                {
                    foreach (var dataGridSelectedItem in dataGridSelectedItems)
                    {
                        var kursRemove = ersteModel.kursevi.Find(((kurs)dataGridSelectedItem).Id);
                        ersteModel.kursevi.Remove(kursRemove);
                        ersteModel.SaveChanges();
                    }
                }
                Load_Data();
            };
        }

        public void Refresh() => Load_Data();

        private void Load_Data()
        {
            DataGrid.Items.Clear();
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
            try
            {
                using (var ersteModel = new ErsteModel())
                {
                    var kursevi = (from kurs in ersteModel.kursevi
                                   join jezik in ersteModel.jezici on kurs.JezikId equals jezik.Id
                                   select kurs).ToList();

                    foreach (var kurs in kursevi)
                    {
                        if (kurs.jezik != null)
                        {
                            DataGrid.Items.Add(kurs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }
        }

        private void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            kurs kurs = DataGrid.SelectedItem as kurs;
            EvidencijaKursaDialog evidencijaKursaDialog = new EvidencijaKursaDialog(kurs);
            evidencijaKursaDialog.ShowDialog();

            Load_Data();
            e.Cancel = true;
        }
    }
}

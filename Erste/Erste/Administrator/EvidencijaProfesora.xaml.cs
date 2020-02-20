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
    /// Interaction logic for EvidencijaProfesora.xaml
    /// </summary>
    public partial class EvidencijaProfesora : UserControl
    {
        public EvidencijaProfesora()
        {
            InitializeComponent();
        }

        public void AddButtonActions(params Button[] buttons)
        {
            buttons[0].Click += (sender, args) =>
            {
                EvidencijaProfesoraDialog evidencijaProfesoraDialog = new EvidencijaProfesoraDialog(null);
                evidencijaProfesoraDialog.ShowDialog();

                Load_Data();
            };
            buttons[1].Click += (sender, args) =>
            {
                var dataGridSelectedItems = DataGrid.SelectedItems;
                using (var ersteModel = new ErsteModel())
                {
                    foreach (var dataGridSelectedItem in dataGridSelectedItems)
                    {
                        var profesorRemove = ersteModel.profesori.Find(((profesor)dataGridSelectedItem).Id);
                        if (profesorRemove?.osoba != null)
                        {
                            ersteModel.osobe.Remove(profesorRemove.osoba);
                            ersteModel.SaveChanges();
                        }
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
                    var profesori = (from profesor in ersteModel.profesori
                                      join osoba in ersteModel.osobe on profesor.Id equals osoba.Id
                                      select profesor).ToList();

                    foreach (var profesor in profesori)
                    {
                        if (profesor.osoba != null)
                        {
                            DataGrid.Items.Add(profesor);
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
            profesor profesor = DataGrid.SelectedItem as profesor;
            EvidencijaProfesoraDialog evidencijaProfesoraDialog = new EvidencijaProfesoraDialog(profesor);
            evidencijaProfesoraDialog.ShowDialog();

            Load_Data();
            e.Cancel = true;
        }
    }
}

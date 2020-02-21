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
                    var polazniciNaCekanju = ersteModel.polaznici_na_cekanju.Where(pnc => pnc.polaznik.osoba.Vazeci).ToList();
                    foreach (var polaznikNaCekanju in polazniciNaCekanju)
                    {
                        var kursevi = polaznikNaCekanju.kursevi.Where(k => k.Vazeci && k.jezik.Vazeci).ToList();
                        foreach (var k in kursevi)
                        {
                            PolaznikNaCekanjuKurs pnck = new PolaznikNaCekanjuKurs
                            {
                                Osoba = polaznikNaCekanju.polaznik.osoba,
                                Kurs = k,
                                Jezik = k.jezik
                            };
                            await Dispatcher.InvokeAsync(() => { DataGrid.Items.Add(pnck); });
                        }
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

    }

    public class PolaznikNaCekanjuKurs
    {
        public osoba Osoba { set; get; }
        public kurs Kurs { set; get; }
        public jezik Jezik { set; get; }
    }

}

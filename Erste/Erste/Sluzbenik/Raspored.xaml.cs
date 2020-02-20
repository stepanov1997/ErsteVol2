using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Erste.Util;

namespace Erste.Sluzbenik
{
    /// <summary>
    /// Interaction logic for Raspored.xaml
    /// </summary>
    public partial class Raspored : UserControl
    {
        public Raspored()
        {
            InitializeComponent();
        }

        public async Task Refresh() => await Load_Data();

        private async Task Load_Data()
        {
            try
            {
                using (var ersteModel = new ErsteModel())
                {

                    List<ListView> kolone = new List<ListView>
                    {
                        listViewMonday,
                        listViewTuesday,
                        listViewWednesday,
                        listViewThursday,
                        listViewFriday,
                        listViewSaturday,
                        listViewSunday
                    };

                    await (from kurs in ersteModel.kursevi
                           join jezik in ersteModel.jezici on kurs.JezikId equals jezik.Id
                           select kurs).ToListAsync();

                    List<TimetableItem> items = await (from termin in ersteModel.termini
                                                       where termin.grupa != null
                                                       join grupa in ersteModel.grupe on termin.GrupaId equals grupa.Id
                                                       join kurs in ersteModel.kursevi on grupa.KursId equals kurs.Id
                                                       join jezik in ersteModel.jezici on kurs.JezikId equals jezik.Id
                                                       select new TimetableItem
                                                       {
                                                           vrijemeOd = termin.Od,
                                                           vrijemeDo = termin.Do,
                                                           jezik = jezik.Naziv,
                                                           nivo = kurs.Nivo,
                                                           dan = termin.Dan,
                                                           GrupaId = termin.GrupaId,
                                                           termin = termin
                                                       }).ToListAsync();
                    items.AddRange(
                        await (from termin in ersteModel.termini
                               where termin.grupa == null
                               select new TimetableItem
                               {
                                   vrijemeOd = termin.Od,
                                   vrijemeDo = termin.Do,
                                   jezik = null,
                                   nivo = null,
                                   dan = termin.Dan,
                                   GrupaId = termin.GrupaId,
                                   termin = termin
                               }).ToListAsync());



                    List<List<TimetableItem>> terminiPoDanima = new List<List<TimetableItem>>();
                    for (int i = 0; i < 7; ++i)
                        terminiPoDanima.Add(new List<TimetableItem>());

                    foreach (var item in items)
                    {
                        switch (item.dan)
                        {
                            case "Ponedjeljak":
                                terminiPoDanima.ElementAt(0).Add(item);
                                break;
                            case "Utorak":
                                terminiPoDanima.ElementAt(1).Add(item);
                                break;
                            case "Srijeda":
                                terminiPoDanima.ElementAt(2).Add(item);
                                break;
                            case "Cetvrtak":
                            case "Četvrtak":
                                terminiPoDanima.ElementAt(3).Add(item);
                                break;
                            case "Petak":
                                terminiPoDanima.ElementAt(4).Add(item);
                                break;
                            case "Subota":
                                terminiPoDanima.ElementAt(5).Add(item);
                                break;
                            default:
                                terminiPoDanima.ElementAt(6).Add(item);
                                break;
                        }
                    }

                    for (int i = 0; i < 7; ++i)
                    {
                        kolone[i].ItemsSource = terminiPoDanima.ElementAt(i).OrderBy(e => e.vrijemeOd);
                        kolone[i].SelectionMode = SelectionMode.Single;
                        kolone[i].MouseUp += (sender, args) =>
                        {
                            if (((FrameworkElement)args.OriginalSource).DataContext is TimetableItem item)
                            {
                                if (Dispatcher != null)
                                {
                                    Window pregledTermina = new PregledTermina(item, async () => await Refresh());
                                    pregledTermina.ShowDialog();
                                }
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Exception: " + ex.ToString());
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*Activity activity = ((FrameworkElement)e.OriginalSource).DataContext as Activity;
            ActivityDetails.OpenedActivity = activity;

            if (activity != null)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Window activityDetails = new ActivityDetails();
                    activityDetails.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                    activityDetails.Height = 343;
                    activityDetails.MinHeight = 343;
                    activityDetails.Width = 593;
                    activityDetails.MinWidth = 593;

                    (activityDetails as ActivityDetails).titleLabel.Content = activity.Title;
                    (activityDetails as ActivityDetails).startTimeLabel.Content += "    " + activity.TimeFrom.ToShortTimeString() + "h";
                    (activityDetails as ActivityDetails).endTimeLabel.Content += "    " + activity.TimeTo.ToShortTimeString() + "h";
                    (activityDetails as ActivityDetails).descriptionBox.Text = activity.Description;
                    (activityDetails as ActivityDetails).dateLabel.Content += "    " + activity.Date.ToShortDateString();
                    activityDetails.ShowDialog();
                });
            }*/
        }
    }
}

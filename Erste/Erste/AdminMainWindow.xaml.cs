using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Erste.Administrator;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Erste
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        //private object _locker = new object();
        //private static int[] _menuIndex = { 0, 0, 0 };
        private NaloziSluzbenika naloziSluzbenika = new NaloziSluzbenika();
        private EvidencijaProfesora evidencijaProfesora = new EvidencijaProfesora();
        private EvidencijaKurseva evidencijaKurseva = new EvidencijaKurseva();

        public AdminMainWindow()
        {
            InitializeComponent();

            Hide_All();

            GridZaPrikaz.Children.Add(naloziSluzbenika);
            GridZaPrikaz.Children.Add(evidencijaProfesora);
            GridZaPrikaz.Children.Add(evidencijaKurseva);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Environment.Exit(0);
        }

        private void Button_NaloziSluzbenika(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(prikaziSluzbenikeButton);
            //GridZaPrikaz.Children.Add(naloziSluzbenika = new NaloziSluzbenika());
            Hide_All();
            naloziSluzbenika.Visibility = Visibility.Visible;
            naloziSluzbenika.Refresh();
        }

        private void Button_DodajSluzbenika(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(dodajSluzbenikeButton);
            NalogSluzbenikaDialog nalogSluzbenikaDialog = new NalogSluzbenikaDialog(null);
            nalogSluzbenikaDialog.ShowDialog();
            naloziSluzbenika?.Refresh();

            if (evidencijaKurseva.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziKurseveButton);
            else if (evidencijaProfesora.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziProfesoreButton);
            else if (naloziSluzbenika?.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziSluzbenikeButton);
        }

        private void Button_EvidencijaProfesora(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(prikaziProfesoreButton);
            //GridZaPrikaz.Children.Add(evidencijaProfesora = new EvidencijaProfesora());
            Hide_All();
            evidencijaProfesora.Visibility = Visibility.Visible;
            evidencijaProfesora.Refresh();
        }

        private void Button_DodajProfesora(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(dodajProfesoreButton);
            EvidencijaProfesoraDialog evidencijaProfesoraDialog = new EvidencijaProfesoraDialog(null);
            evidencijaProfesoraDialog.ShowDialog();
            evidencijaProfesora.Refresh();
            if (evidencijaKurseva.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziKurseveButton);
            else if (evidencijaProfesora.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziProfesoreButton);
            else if (naloziSluzbenika?.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziSluzbenikeButton);
        }

        private void Button_EvidencijaKurseva(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(prikaziKurseveButton);
            //GridZaPrikaz.Children.Add(evidencijaKurseva = new EvidencijaKurseva());
            Hide_All();
            evidencijaKurseva.Visibility = Visibility.Visible;
            evidencijaKurseva.Refresh();
        }

        private void Button_DodajKurs(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(dodajKursButton);
            EvidencijaKursaDialog evidencijaKursaDialog = new EvidencijaKursaDialog(null);
            evidencijaKursaDialog.ShowDialog();
            evidencijaKurseva.Refresh();
            if (evidencijaKurseva.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziKurseveButton);
            else if (evidencijaProfesora.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziProfesoreButton);
            else if (naloziSluzbenika?.Visibility == Visibility.Visible)
                ClickOnFieldColor(prikaziSluzbenikeButton);
        }

        private void Hide_All()
        {
            naloziSluzbenika.Visibility = Visibility.Hidden;
            evidencijaProfesora.Visibility = Visibility.Hidden;
            evidencijaKurseva.Visibility = Visibility.Hidden;
        }

        private void ClickOnFieldColor(Button clickedButton)
        {
            ClearFieldsColor();
            clickedButton.Background = new SolidColorBrush(Colors.DarkRed);
        }

        private void ClearFieldsColor()
        {
            Button[] buttons =
            {
                prikaziSluzbenikeButton,
                prikaziKurseveButton,
                prikaziProfesoreButton,
                dodajSluzbenikeButton,
                dodajKursButton,
                dodajProfesoreButton,
            };
            foreach (var button in buttons)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
            }
        }

        /*private async Task NapraviAnimaciju(StackPanel stackPanel, int index, Button button, TimeSpan animationDurance)
        {
            lock (_locker)
            {
                button.IsEnabled = false;
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                if (_menuIndex[index] % 2 == 0)
                {
                    SolidColorBrush solidColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                    button.Background = solidColor;
                    RegisterName("SolidColor", solidColor);

                    AnimationTimeline expand = new DoubleAnimation(0, stackPanel.ActualHeight, animationDurance);

                    AnimationTimeline opacity = new DoubleAnimation(0, 1, animationDurance);
                    AnimationTimeline colorChange = new ColorAnimation(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A), Colors.DarkRed, animationDurance);

                    Storyboard.SetTarget(expand, stackPanel);
                    Storyboard.SetTargetProperty(expand, new PropertyPath(Rectangle.HeightProperty));

                    DataGrid dataGrid;
                    if (index == 0)
                        dataGrid = naloziSluzbenika.DataGrid;
                    else if (index == 1)
                        dataGrid = evidencijaProfesora.DataGrid;
                    else
                        dataGrid = evidencijaKurseva.DataGrid;
                    Storyboard.SetTarget(opacity, dataGrid);
                    Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));

                    Storyboard.SetTargetName(colorChange, "SolidColor");
                    Storyboard.SetTargetProperty(colorChange, new PropertyPath(SolidColorBrush.ColorProperty));

                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(expand);

                    if (index == 0)
                        naloziSluzbenika.Refresh();
                    else if (index == 1)
                        evidencijaProfesora.Refresh();
                    else
                        evidencijaKurseva.Refresh();
                    storyboard.Children.Add(opacity);
                    storyboard.Children.Add(colorChange);
                    storyboard.Begin(this);
                }
                else
                {
                    SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
                    button.Background = solidColor;
                    RegisterName("SolidColor", solidColor);

                    AnimationTimeline expand = new DoubleAnimation(stackPanel.ActualHeight, 0, animationDurance);
                    AnimationTimeline colorChange = new ColorAnimation(Colors.DarkRed, Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A), animationDurance);
                    AnimationTimeline opacity = new DoubleAnimation(1, 0, animationDurance);

                    Storyboard.SetTarget(expand, stackPanel);
                    Storyboard.SetTargetProperty(expand, new PropertyPath(Rectangle.HeightProperty));

                    Storyboard.SetTargetName(colorChange, "SolidColor");
                    Storyboard.SetTargetProperty(colorChange, new PropertyPath(SolidColorBrush.ColorProperty));

                    DataGrid dataGrid;
                    if (index == 0)
                        dataGrid = naloziSluzbenika.DataGrid;
                    else if (index == 1)
                        dataGrid = evidencijaProfesora.DataGrid;
                    else
                        dataGrid = evidencijaKurseva.DataGrid;
                    Storyboard.SetTarget(opacity, dataGrid);
                    Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));

                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(expand);

                    storyboard.Children.Add(opacity);
                    storyboard.Children.Add(colorChange);
                    storyboard.Begin(this);
                }

                UnregisterName("SolidColor");
                _menuIndex[index]++;

            }
            await Task.Run(async () =>
            {
                await Task.Delay(animationDurance);
                Dispatcher?.Invoke(() =>
                {
                    button.IsEnabled = true;
                    button1.IsEnabled = true;
                    button2.IsEnabled = true;
                });
            });
        }*/
        private async void LogOff_Click(object sender, RoutedEventArgs e)
        {
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(() =>
                {
                    Hide();
                    LoginWindow window = new LoginWindow
                        { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = null };
                    window.Show();
                });
        }
    }
}

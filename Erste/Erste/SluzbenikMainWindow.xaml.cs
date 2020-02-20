using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Erste.Sluzbenik;

namespace Erste
{
    /// <summary>
    /// Interaction logic for SluzbenikMainWindow.xaml
    /// </summary>
    public partial class SluzbenikMainWindow : Window
    {
        private Kandidati kandidatiSvi = new Kandidati("svi");
        private Kandidati kandidatiCekanje = new Kandidati("cekanje");
        private Raspored raspored = new Raspored();
        public SluzbenikMainWindow()
        {
            InitializeComponent();

            Hide_All();

            GridZaPrikaz.Children.Add(kandidatiSvi);
            GridZaPrikaz.Children.Add(kandidatiCekanje);
            GridZaPrikaz.Children.Add(raspored);
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

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

        /*private static int[] _menuIndex = { 0, 0, 0 };
        private object _locker = new object();*/

        private void Upis_Click(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(upisButton);

            UpisPolaznikaDialog upisPolaznikaDialog = new UpisPolaznikaDialog();
            upisPolaznikaDialog.ShowDialog();

            ShowLastView();
        }

        private async void Raspored_Click(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(rasporedButton);
            //GridZaPrikaz.Children.Add(raspored = new Raspored());
            Hide_All();
            raspored.Visibility = Visibility.Visible;
            await raspored.Refresh();
        }

        private async void Pregled_Click(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(pregledButton);
            //GridZaPrikaz.Children.Add(kandidati = new Kandidati("svi"));
            Hide_All();
            kandidatiSvi.Visibility = Visibility.Visible;
            await kandidatiSvi.Refresh();
        }

        private async void KandidatiNaCekanju_Click(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(pregledCekanjeButton);
            //GridZaPrikaz.Children.Add(kandidati = new Kandidati("cekanje"));
            Hide_All();
            kandidatiCekanje.Visibility = Visibility.Visible;
            await kandidatiCekanje.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Environment.Exit(0);
        }

        private async void DodajNoviTermin_Click(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(noviTerminButton);
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(() =>
                {
                    KreiranjeTermina kreiranjeKursa =
                        new KreiranjeTermina(async () => await raspored.Refresh());
                    kreiranjeKursa.ShowDialog();
                    ShowLastView();
                });
        }

        private void ShowLastView()
        {
            if (raspored.Visibility == Visibility.Visible)
                ClickOnFieldColor(rasporedButton);
            else if (kandidatiSvi.Visibility == Visibility.Visible)
                ClickOnFieldColor(pregledButton);
            else if (kandidatiCekanje.Visibility == Visibility.Visible)
                ClickOnFieldColor(pregledCekanjeButton);
            else
            {
                ClickOnFieldColor(null);
            }
        }

        private void Hide_All()
        {
            kandidatiSvi.Visibility = Visibility.Hidden;
            kandidatiCekanje.Visibility = Visibility.Hidden;
            raspored.Visibility = Visibility.Hidden;
        }

        private void ClickOnFieldColor(Button clickedButton)
        {
            Button[] buttons =
            {
                upisButton,
                pregledButton,
                pregledCekanjeButton,
                rasporedButton,
                noviTerminButton,
                pregledGrupa,
                kreiranjeGrupe
            };

            foreach (var button in buttons)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
            }

            if (clickedButton != null)
            {
                clickedButton.Background = new SolidColorBrush(Colors.DarkRed);
            }
        }

        private async void PregledGrupa_OnClick(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(pregledGrupa);
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(() =>
                {
                    PregledGrupe pregledGrupe = new PregledGrupe(null);
                    pregledGrupe.ShowDialog();
                    ShowLastView();
                });
        }

        private async void DodajNovuGrupu_OnClick(object sender, RoutedEventArgs e)
        {
            ClickOnFieldColor(kreiranjeGrupe);
            if (Dispatcher != null)
                await Dispatcher.InvokeAsync(() =>
                {
                    KreiranjeGrupe kreiranjeGrupe = new KreiranjeGrupe();
                    kreiranjeGrupe.ShowDialog();
                    ShowLastView();
                });
        }
        /*private async Task NapraviAnimaciju(StackPanel stackPanel, int index, Button button, TimeSpan animationDurance)
        {
            lock (_locker)
            {
                pregledButton.IsEnabled = false;
                upisButton.IsEnabled = false;
                rasporedButton.IsEnabled = false;
                if (_menuIndex[index] % 2 == 0)
                {
                    //_kandidati.Visibility = Visibility.Visible;
                    SolidColorBrush solidColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                    button.Background = solidColor;
                    RegisterName("SolidColor", solidColor);
                    AnimationTimeline expand = new DoubleAnimation(0, stackPanel.ActualHeight, animationDurance);
                    AnimationTimeline shrink = new DoubleAnimation(label.ActualHeight, label.ActualHeight - stackPanel.ActualHeight,
                        animationDurance);
                    //AnimationTimeline timeline = new DoubleAnimation(800, animationDurance);
                    AnimationTimeline opacity = new DoubleAnimation(0,1, animationDurance);
                    AnimationTimeline colorChange = new ColorAnimation(Color.FromArgb(0xFF,0xEF,0x3D,0x4A), Colors.DarkRed, animationDurance);
                    Storyboard.SetTarget(expand, stackPanel);
                    Storyboard.SetTargetProperty(expand, new PropertyPath(Rectangle.HeightProperty));
                    Storyboard.SetTarget(shrink, label);
                    Storyboard.SetTargetProperty(shrink, new PropertyPath(Rectangle.HeightProperty));
                    //Storyboard.SetTarget(timeline, this);
                    //Storyboard.SetTargetProperty(timeline, new PropertyPath(Window.WidthProperty));
                    Storyboard.SetTarget(opacity, _kandidati.DataGrid);
                    Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));
                    Storyboard.SetTargetName(colorChange, "SolidColor");
                    Storyboard.SetTargetProperty(colorChange, new PropertyPath(SolidColorBrush.ColorProperty));
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(expand);
                    storyboard.Children.Add(shrink);
                    //if(index==2) storyboard.Children.Add(timeline);
                    if (index == 2)
                    {
                        _kandidati.Refresh();
                        storyboard.Children.Add(opacity);
                    }
                    storyboard.Children.Add(colorChange);
                    storyboard.Begin(this);
                }
                else
                {
                    //_kandidati.Visibility = Visibility.Collapsed;
                    SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
                    button.Background = solidColor;
                    RegisterName("SolidColor", solidColor);
                    //AnimationTimeline timeline = new DoubleAnimation( 217, TimeSpan.FromMilliseconds(1000));
                    AnimationTimeline expand = new DoubleAnimation(stackPanel.ActualHeight, 0, animationDurance);
                    AnimationTimeline shrink = new DoubleAnimation(label.ActualHeight, label.ActualHeight + stackPanel.ActualHeight,
                        animationDurance);
                    AnimationTimeline colorChange = new ColorAnimation(Colors.DarkRed, Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A), animationDurance);
                    AnimationTimeline opacity = new DoubleAnimation(1, 0, animationDurance);
                    Storyboard.SetTarget(expand, stackPanel);
                    Storyboard.SetTargetProperty(expand, new PropertyPath(Rectangle.HeightProperty));
                    Storyboard.SetTarget(shrink, label);
                    Storyboard.SetTargetProperty(shrink, new PropertyPath(Rectangle.HeightProperty));
                    //Storyboard.SetTarget(timeline, this);
                    //Storyboard.SetTargetProperty(timeline, new PropertyPath(Window.WidthProperty));
                    Storyboard.SetTargetName(colorChange, "SolidColor");
                    Storyboard.SetTargetProperty(colorChange, new PropertyPath(SolidColorBrush.ColorProperty));
                    Storyboard.SetTarget(opacity, _kandidati.DataGrid);
                    Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(expand);
                    storyboard.Children.Add(shrink);
                    //if (index == 2) storyboard.Children.Add(timeline);
                    if (index == 2) storyboard.Children.Add(opacity);
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
                    pregledButton.IsEnabled = true;
                    upisButton.IsEnabled = true;
                    rasporedButton.IsEnabled = true;
                });
            });
        }*/
    }
}
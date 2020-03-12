using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Erste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : NavigationWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            usernameBox.Focus();

            //using (ErsteModel ersteModel = new ErsteModel())
            //{
            //    grupa grupa = new grupa
            //    {
            //        kurs = new kurs()
            //        {
            //            jezik = new jezik()
            //            {
            //                Naziv = "kurs1"
            //            },
            //            Nivo="A2"
            //        }
            //    };
            //    grupa.polaznici.Add(new polaznik()
            //    {
            //        osoba = new osoba
            //        {
            //            Ime="Mitar",
            //            Prezime = "Mirić",
            //            BrojTelefona = "065555333",
            //            Email = "mitar.miric@gmail.com"
            //        }
            //    });
            //    grupa.profesori.Add(new profesor()
            //    {
            //        osoba = new osoba
            //        {
            //            Ime = "Profesor",
            //            Prezime = "Profesorčić",
            //            BrojTelefona = "065555333",
            //            Email = "profa.profic@gmail.com"
            //        }
            //    });
            //    ersteModel.grupe.Add(grupa);
            //    ersteModel.SaveChanges();
            //}


            //za testiranje
            //using (ErsteModel context = new ErsteModel())
            //{
            //    List<Administrator> administrators = new List<Administrator>();
            //    List<sluzbenik> employees = new List<sluzbenik>();
            //    List<osoba> users = new List<osoba>();

            //    HashGenerator hashGenerator = new HashGenerator();
            //    osoba user = new osoba
            //    {
            //        Id = 1,
            //        Ime = "admin",
            //        Prezime = "admin",
            //        BrojTelefona = "2345324",
            //        Email = "admin@gmail.com"
            //    };
            //    users.Add(user);
            //    Administrator admin = new Administrator
            //    {
            //        Id = 1,
            //        KorisnickoIme = "admin",
            //        LozinkaHash = hashGenerator.ComputeHash("admin")
            //    };
            //    administrators.Add(admin);

            //    osoba user2 = new osoba
            //    {
            //        Id = 2,
            //        Ime = "sluzbenik",
            //        Prezime = "sluzbenik",
            //        BrojTelefona = "2332131232124",
            //        Email = "marko@gmail.com"
            //    };
            //    users.Add(user2);
            //    sluzbenik employee = new sluzbenik
            //    {
            //        Id = 2,
            //        KorisnickoIme = "sluzbenik",
            //        LozinkaHash = hashGenerator.ComputeHash("sluzbenik")
            //    };
            //    employees.Add(employee);

            //    context.osobe.AddOrUpdate(user);
            //    context.administratori.AddOrUpdate(admin);
            //    context.osobe.AddOrUpdate(user2);
            //    context.sluzbenici.AddOrUpdate(employee);
            //    context.SaveChanges();
            //}
        }

        private async void Prijava_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
                {
                    try
                    {
                        if (Dispatcher != null)
                            await Dispatcher?.InvokeAsync(() =>
                            {
                                usernameBox.IsEnabled = false;
                                passwordBox.IsEnabled = false;
                                NapomenaBox.Visibility = Visibility.Visible;
                                NapomenaBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0xCE, 0xF9));
                                NapomenaBox.Text = "Molimo sačekajte...";
                            });

                        string username = "", password = "";

                        if (Dispatcher != null)
                            await Dispatcher?.InvokeAsync(() =>
                            {
                                username = usernameBox.Text;
                                password = passwordBox.Password;
                            });

                        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                        {
                            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
                            {
                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        NapomenaBox.Visibility = Visibility.Visible;
                                        NapomenaBox.Background =
                                            new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                                        usernameBox.IsEnabled = true;
                                        passwordBox.IsEnabled = true;
                                        NapomenaBox.Text = "Unesite korisničko ime i lozinku.";
                                    });
                                return;
                            }

                            if (string.IsNullOrEmpty(username))
                            {
                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        NapomenaBox.Visibility = Visibility.Visible;
                                        NapomenaBox.Background =
                                            new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                                        usernameBox.IsEnabled = true;
                                        passwordBox.IsEnabled = true;
                                        NapomenaBox.Text = "Unesite korisničko ime.";
                                    });
                                return;
                            }
                            else
                            {
                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        NapomenaBox.Visibility = Visibility.Visible;
                                        NapomenaBox.Background =
                                            new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                                        usernameBox.IsEnabled = true;
                                        passwordBox.IsEnabled = true;
                                        NapomenaBox.Text = "Unesite lozinku.";
                                    });
                                return;
                            }

                        }

                        HashGenerator hashGenerator = new HashGenerator();
                        string hash = hashGenerator.ComputeHash(password);

                        //slanje podataka na server i login
                        using (ErsteModel context = new ErsteModel())
                        {
                            var administators =
                                await (from a in context.administratori
                                       where a.KorisnickoIme.Equals(username) && a.osoba.Vazeci == true
                                       select a)
                                    .ToListAsync();
                            if (administators.Count != 0 && hash.Equals(administators[0].LozinkaHash))
                            {
                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        NapomenaBox.Visibility = Visibility.Visible;
                                        NapomenaBox.Foreground = new SolidColorBrush(Colors.White);
                                        NapomenaBox.Background = new SolidColorBrush(Colors.LightGreen);
                                        usernameBox.IsEnabled = true;
                                        passwordBox.IsEnabled = true;
                                        NapomenaBox.Text =
                                            "Uspješno ste se prijavili. Sačekajte da se učita novi prozor.";
                                    });

                                await Task.Delay(1500);

                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        AdminMainWindow window = new AdminMainWindow();
                                        window.Show();
                                        Close();
                                    });

                                return;
                            }

                            var employees = await (from a in context.sluzbenici
                                                   where a.KorisnickoIme.Equals(username) && a.osoba.Vazeci == true
                                                   select a)
                                .ToListAsync();
                            if (employees.Count != 0 && hash.Equals(employees[0].LozinkaHash))
                            {
                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        NapomenaBox.Visibility = Visibility.Visible;
                                        NapomenaBox.Text =
                                            "Uspješno ste se prijavili. Sačekajte da se učita novi prozor.";
                                        usernameBox.IsEnabled = true;
                                        passwordBox.IsEnabled = true;
                                        NapomenaBox.Foreground = new SolidColorBrush(Colors.White);
                                        NapomenaBox.Background = new SolidColorBrush(Colors.LightGreen);
                                    });

                                await Task.Delay(1500);

                                if (Dispatcher != null)
                                    await Dispatcher?.InvokeAsync(() =>
                                    {
                                        SluzbenikMainWindow window = new SluzbenikMainWindow { Owner = null };
                                        window.Show();
                                        Hide();
                                    });
                                return;
                            }
                        }

                        if (Dispatcher != null)
                            await Dispatcher?.InvokeAsync(() =>
                            {
                                NapomenaBox.Visibility = Visibility.Visible;
                                NapomenaBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                                usernameBox.IsEnabled = true;
                                passwordBox.IsEnabled = true;
                                NapomenaBox.Text = "Nalog sa tim korisničkim imenom i lozinkom ne postoji.";
                            });

                        await Task.Delay(5000);

                    }
                    catch (Exception exception)
                    {
                        if (Dispatcher != null)
                            await Dispatcher?.InvokeAsync(() =>
                            {
                                NapomenaBox.Visibility = Visibility.Visible;
                                NapomenaBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEF, 0x3D, 0x4A));
                                usernameBox.IsEnabled = true;
                                passwordBox.IsEnabled = true;
                                NapomenaBox.Text = "Konekcija nije uspostavljena.";
                            });

                        await Task.Delay(5000);
                    }
                })
                .ContinueWith(async a =>
            {
                await Task.Delay(5000);

                if (Dispatcher != null)
                {
                    await Dispatcher?.InvokeAsync(() =>
                    {
                        NapomenaBox.Visibility = Visibility.Collapsed;
                    });
                }
            });
        }

        private async void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Prijava_Click(sender, e);
            }
        }

        private void RevealPassword_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Visibility = Visibility.Collapsed;
            passwordBoxReveal.Text = passwordBox.Password;
            passwordBoxReveal.Visibility = Visibility.Visible;
            PasswordRevealImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/password_eye_inverted.png")));
        }

        private void RevealPassword_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Visibility = Visibility.Visible;
            passwordBox.Password = passwordBoxReveal.Text;
            passwordBoxReveal.Visibility = Visibility.Collapsed;
            PasswordRevealImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/password_eye.png")));
        }
    }
}

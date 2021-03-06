﻿using System;
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

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //  Code achter het registratie scherm
    //
    //  Author: Stijn Stas

    public partial class RegistratieWindow : Window
    {
        //  Eigenschappen
        private string naam;
        private string voornaam;
        private string geboorteDatum;
        private string passwoord;
        private string klas;
        private AlleGebruikers allegebruikers;
        private MainWindow mainWindow;

        //  Constructor
        public RegistratieWindow(AlleGebruikers allegebruikers, MainWindow mainWindow)
        {
            InitializeComponent();
            datumDatePicker.SelectedDate = DateTime.Now;
            this.allegebruikers = allegebruikers;
            klasComboBox.ItemsSource = allegebruikers.Klassen;
            klasComboBox.SelectedValue = "testKlas";
            this.mainWindow = mainWindow;
        }

        //methoden

        //  gebruikers worden aangemaakt
        //  met behulp van de allegebruikersklasse
        //
        //  Author: Stijn Stas
        private void klasButton_Click(object sender, RoutedEventArgs e)
        {
            naam = naamTextBox.Text;
            voornaam = voornaamTextBox.Text;
            passwoord = passwoordPasswordBox.Password;
            geboorteDatum = datumDatePicker.Text;
            if (datumDatePicker.Text.Split('/')[0].Length < 2) //vul aan met 0
            {
                geboorteDatum = "0" + datumDatePicker.Text;
            }
            klas = klasComboBox.SelectedItem.ToString();
            if ((naam!="")&&(voornaam!="")&&(passwoord!="")&&(geboorteDatum!="")&&
                ((soortRegistratieComboBox.SelectedItem==leerlingItem)||(soortRegistratieComboBox.SelectedItem==leerkrachtItem)))
            {
                if (soortRegistratieComboBox.SelectedItem == leerlingItem)
                {
                    Leerling student = new Leerling(naam, voornaam, geboorteDatum, passwoord, klas, allegebruikers);
                    student.SlaOp();
                    MessageBox.Show(string.Format("Student:\t\t{0}\nWachtwoord:\t{1}", student.ID, student.Paswoord), "Opgeslagen");
                }
                else if (soortRegistratieComboBox.SelectedItem == leerkrachtItem)
                {
                    Leerkracht leerkracht = new Leerkracht(naam, voornaam, geboorteDatum, passwoord, allegebruikers);
                    leerkracht.SlaOp();
                    MessageBox.Show(string.Format("Leerkracht:\t{0}\nWachtwoord:\t{1}", leerkracht.ID, leerkracht.Paswoord), "Opgeslagen");
                } 
            }
            else
            {
                MessageBox.Show("Ongeldige registratie", "FOUT !!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //  Handler die elementen in het scherm toont
        //  afhankelijk van gebruiker
        //
        //  Author: Stijn Stas
        private void soortRegistratieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (soortRegistratieComboBox.SelectedItem == leerlingItem)
            {
                klasLabel.Visibility = Visibility.Visible;
                klasComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                klasLabel.Visibility = Visibility.Hidden;
                klasComboBox.Visibility = Visibility.Hidden;
            }
        }

        //  keer terug naar beginscherm
        //
        //  Author: Stijn Stas

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }
    }
}

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
using Microsoft.Win32;

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainVragenWindow : Window
    {
        private string bestandsNaam;
        private AlleGebruikers alleGebruikers;
        private MainWindow mainWindow;

        public MainVragenWindow()
        {
            InitializeComponent();
        }

        public MainVragenWindow(Leerling leerling, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        public MainVragenWindow(Leerkracht leerkracht, AlleGebruikers allegebruikers, MainWindow mainWindow)
        {
            InitializeComponent();
            this.alleGebruikers = allegebruikers;
            this.mainWindow = mainWindow;
        }

        private void opstellenButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            // initial directory 
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                Window w = new AanpassenWindow(bestandsNaam, true);
                w.Show();
            }
        }

        private void oplossenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            // initial directory 
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                Window w = new OplossenWindow(bestandsNaam);
                w.Show();
            }
        }
             
        private void aanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            // initial directory 
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam!="")
            {
                Window w = new AanpassenWindow(bestandsNaam, false);
                w.Show();
            }
        }

        private void scoreButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OverzichtScoresWindow();
            w.Show();
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.Show();
            this.Hide();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void LogUitButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        private void NieuweKlasButton_Click(object sender, RoutedEventArgs e)
        {
            Window nieuweKlasWindow = new login.maakKlasWindow(alleGebruikers, this);
            nieuweKlasWindow.Show();
            this.Hide();
        }

    }
}

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
using System.IO;

// deze window wordt niet meer gebruikt; nieuwe vragenlijsten worden ook in AanpassenWindow gemaakt.
// deze window moet uit het uiteindelijke project verwijderd worden

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for Opstellen.xaml
    /// </summary>
    public partial class OpstellenWindow : Window
    {
        private List<Vraag> vragenLijst;
       

        public OpstellenWindow()
        {
            InitializeComponent();
            vragenLijst=new List<Vraag>();
            
        }
       

        private void volgendeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegVraagToe();
        }

        private void opslaanButton_Click(object sender, RoutedEventArgs e)
        {
            VoegVraagToe();
            StreamWriter outputStream = File.CreateText("vragen.txt");
            foreach(Vraag vraag in vragenLijst){
                outputStream.WriteLine(vraag.ToString());

            }
            outputStream.Close();
        }

        private void VoegVraagToe()
        {
            BasisVraag vraag = new BasisVraag(opgaveTextBox.Text, antwoordTextBox.Text);
            vragenLijst.Add(vraag);
            opgaveTextBox.Text = "";
            antwoordTextBox.Text = "";
        }


    }
}

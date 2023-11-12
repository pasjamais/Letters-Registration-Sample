using DB_Interaction.Entities;
using DB_Interaction.Views;
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
using WPF_Client.Entities;

namespace WPF_Client.Pages
{
    /// <summary>
    /// Logique d'interaction pour Page_LettersList.xaml
    /// </summary>
    public partial class Page_LettersList : Page, IHas_field_Title
    {
        public Page_LettersList()
        {
            InitializeComponent();
            string control_message;
            DataGridLettersList.ItemsSource = Letter_Agent.Get_Letter_Captions(out control_message);
            MainWindow.Instance.StatusProperty.Message += control_message;
        }

        private void Button_OpenLetter_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridLettersList.SelectedIndex >= 0)
            {
                Letter_Caption letter_Caption = DataGridLettersList.SelectedItem as Letter_Caption;
                string control_message;
                Letter letter = Letter_Agent.Get_Letter_by_ID(letter_Caption.Id, out control_message);
                MainWindow.Instance.StatusProperty.Message += control_message;
                MainWindow.Instance.Navigate(letter);
            }
        }
    }
}

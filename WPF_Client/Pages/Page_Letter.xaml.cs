using DB_Interaction.Entities;
using DB_Interaction.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static Azure.Core.HttpHeader;

namespace WPF_Client.Pages
{
    /// <summary>
    /// Logique d'interaction pour Page_Letter.xaml
    /// </summary>
    public partial class Page_Letter : Page, IHas_field_Title
    {
        public Page_Letter()
        {
            InitializeComponent();
            Reload_ComboBox(CB_Sender);
            Reload_ComboBox(CB_Addressee);
        }

        public Page_Letter(Letter letter) : this()
        {
            Label_Date.Visibility = Visibility.Visible;
            Button_SendLetter.IsEnabled = false;
            Fill_Form_from_Item(letter);
        }
        private void Reload_ComboBox(ComboBox comboBox)
        {
            string message;
            comboBox.ItemsSource = Server_Agent.Get_Users(out message);
            comboBox.DisplayMemberPath = "Name";
            if (comboBox.Items.Count > 0)
                comboBox.SelectedItem = comboBox.Items[0];
        }
        private bool Fill_Letter_from_Form(Letter letter)
        {
            bool result = true;
            letter.Date = DateTime.Now;
            Notice notice = new Notice();

            if (TB_Subject.Text != "")
                letter.Subject = TB_Subject.Text;
            else
            {
                MainWindow.Instance.StatusProperty.Message += "\nThe mail subject cannot be empty";
                notice.Notice_by_Background_Color(TB_Subject.Background);
                result = false;
            }

            if (TB_Text.Text != "")
                letter.Content = TB_Text.Text;
            else
            {
                MainWindow.Instance.StatusProperty.Message += "\nThe mail text cannot be empty";
                notice.Notice_by_Background_Color(TB_Text.Background);
                result = false;
            }

            if (CB_Sender.SelectedValue != null)
            {
                letter.SenderId = ((User)CB_Sender.SelectedItem).Id;
                letter.Sender = ((User)CB_Sender.SelectedItem);
            }
            else
            {
                MainWindow.Instance.StatusProperty.Message += "\nSender not specified";
                notice.Notice_by_Background_Color(CB_Sender.Background);
                result = false;
            }

            if (CB_Addressee.SelectedValue != null)
            {
                letter.AddresseeId = ((User)CB_Addressee.SelectedItem).Id;
                letter.Addressee = ((User)CB_Addressee.SelectedItem);
            }
            else
            {
                MainWindow.Instance.StatusProperty.Message += "\nAddressee not specified";
                notice.Notice_by_Background_Color(CB_Sender.Background);
                result = false;
            }

            return result;
        }


        private void Button_Add_User_Click(object sender, RoutedEventArgs e)
        {
            if (TB_User_Name.Text != "" && TB_User_Email.Text != "")
            {
                string control_message;
                if (User_Agent.Create_User(TB_User_Name.Text, TB_User_Email.Text, out control_message))
                {
                    Reload_ComboBox(CB_Sender);
                    Reload_ComboBox(CB_Addressee);
                }
                MainWindow.Instance.StatusProperty.Message += control_message;
            };
        }

        private void Button_SendLetter_Click(object sender, RoutedEventArgs e)
        {
            Letter letter = new();
            if (Fill_Letter_from_Form(letter))
            {
                MainWindow.Instance.StatusProperty.Message += Letter_Agent.Create_Letter(letter);
                MainWindow.Instance.Navigate<Page_LettersList>();
            }
        }

        private void Fill_Form_from_Item(Letter letter)
        {
            Label_Date.Content = Label_Date.Content as string + letter.Date.ToString();
            TB_Text.Text = letter.Content;
            TB_Subject.Text = letter.Subject;

            if (letter.SenderId == null)
                CB_Sender.SelectedItem = null;
            else
            {
                CB_Sender.SelectedValue = letter.SenderId;
            }

            if (letter.AddresseeId == null)
                CB_Addressee.SelectedItem = null;
            else
            {
                CB_Addressee.SelectedValue = letter.AddresseeId;
            }
        }
    }
}

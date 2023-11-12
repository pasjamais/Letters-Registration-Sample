using DB_Interaction.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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
using static Azure.Core.HttpHeader;

namespace WPF_Client.Pages
{
    /// <summary>
    /// Logique d'interaction pour Page_UsersList.xaml
    /// </summary>
    public partial class Page_UsersList : Page, IHas_field_Title
    {
        public Page_UsersList()
        {
            InitializeComponent();
            Reload_DGV();
        }

        private void Reload_DGV()
        {
            string control_message;
            DataGridUsersList.ItemsSource = User_Agent.Get_User_Captions(out control_message);
            MainWindow.Instance.StatusProperty.Message += control_message;
        }

        private void Button_Add_User_Click(object sender, RoutedEventArgs e)
        {
            if (TB_User_Name.Text != "" && TB_User_Email.Text != "")
            {
                string control_message;
                if (User_Agent.Create_User(TB_User_Name.Text, TB_User_Email.Text, out control_message))
                {
                    Reload_DGV();
                }
                MainWindow.Instance.StatusProperty.Message += control_message;
            };
        }

        private void Button_EditUser_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsersList.SelectedIndex >= 0)
            {
                User_Caption user_Caption = DataGridUsersList.SelectedItem as User_Caption;
                MainWindow.Instance.StatusProperty.Message += User_Agent.Delete_User(user_Caption.Id);
                Reload_DGV();
            }

        }
    }
}

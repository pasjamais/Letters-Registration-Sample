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

namespace WPF_Client.Pages
{
    /// <summary>
    /// Logique d'interaction pour Page_System.xaml
    /// </summary>
    public partial class Page_System : Page, IHas_field_Title
    {
        public Page_System()
        {
            InitializeComponent();
            TB_ServerAddress.Text = Server_Agent.Get_Server_Address();
        }

        private void Button_RecreateDB_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.StatusProperty.Message += Server_Agent.Initialize_BD_with_Default_Values("competely_new");
        }
        
        private void Button_Initialise_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.StatusProperty.Message += Server_Agent.Initialize_BD_with_Default_Values("refill");
        }

        private void Button_NewServerAddress_Click(object sender, RoutedEventArgs e)
        {
            Server_Agent.Set_Server_Address(TB_ServerAddress.Text);
        }
    }
}

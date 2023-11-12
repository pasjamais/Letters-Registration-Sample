using DB_Interaction.Entities;
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
using WPF_Client.Pages;
using static Azure.Core.HttpHeader;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public StatusBarUpdate StatusProperty { get; set; } = new StatusBarUpdate();
        static MainWindow()
        {
            Instance = new MainWindow();
        }

        private MainWindow()
        {
            InitializeComponent();
        }

        private void Button_UsersList_Click(object sender, RoutedEventArgs e)
        {
            Navigate<Page_UsersList>();
        }

        private void Button_LettersList_Click(object sender, RoutedEventArgs e)
        {
            Navigate<Page_LettersList>();
        }

        private void Button_ComposeLetter_Click(object sender, RoutedEventArgs e)
        {
            Navigate<Page_Letter>();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Navigate<T>() where T: IHas_field_Title, new()
        {
            var page = new T();
            FrameMain.Navigate(new T());
            StatusProperty.Message += "\n" + page.Title; 
        }
        public void Navigate(Letter letter)
        {
            Page_Letter page = new Page_Letter(letter);
            FrameMain.Navigate(page);
            StatusProperty.Message += "\n" + page.Title;
        }

        private void TB_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            TB_Output.ScrollToEnd();
        }

        private void Button_System_Click(object sender, RoutedEventArgs e)
        {
            Navigate<Page_System>();
        }
    }
}

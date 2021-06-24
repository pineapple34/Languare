using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WpfApp1.Data;

namespace WpfApp1.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        public schoolEntities lang;

        public ClientForm()
        {
            InitializeComponent();
            lang = new schoolEntities();
            lang.Clients.Load();
            GridClient.ItemsSource = lang.Clients.Local;
        }

        private void ClientAdd_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new ClientUnevers().ShowDialog();
            lang.Clients.Load();
            GridClient.ItemsSource = lang.Clients.Local;
        }

        private void GridClient_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) //активация кнопок
        {
                ClientEdit.IsEnabled = true;
                ClientDelet.IsEnabled = true;
        }

        public static Client Red;

        private void ClientEdit_Click(object sender, RoutedEventArgs e)
        {
            Red = (Client)GridClient.SelectedItem;
            Hide();
            new ClientUnevers().ShowDialog();
            lang.Clients.Load();
            GridClient.ItemsSource = lang.Clients.Local;
        }

        private void ClientMount_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new ClientBith().ShowDialog();
        }

        private void ClientDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Red = (Client)GridClient.SelectedItem;
                lang.Clients.Remove(Red);
                lang.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите клиента");
            }
        }
    }
}

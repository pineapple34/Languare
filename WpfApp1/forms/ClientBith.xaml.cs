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
    /// Логика взаимодействия для ClientBith.xaml
    /// </summary>
    public partial class ClientBith : Window
    {
        public ClientBith()
        {
            schoolEntities lang = new schoolEntities();
            InitializeComponent();
            lang.Clients.Load();
            GridClient.ItemsSource = lang.Clients.Local.Where(a=>a.Birthday.Value.Month == Convert.ToInt32( DateTime.Today.Month));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new ClientForm().Show();
        }
    }
}

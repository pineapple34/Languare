using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Data;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1.Forms
{
    /// <summary>
    /// Логика взаимодействия для ClientUnevers.xaml
    /// </summary>
    public partial class ClientUnevers : Window
    {
        public bool e1 = false;//email неверен

        public void LoadCli()
        {
            try
            {
                IdClient.Content = ClientForm.Red.ID;
                NameClient.Text = ClientForm.Red.FirstName;
                FamlyClient.Text = ClientForm.Red.LastName;
                OtClient.Text = ClientForm.Red.Patronymic;
                EmailClient.Text = ClientForm.Red.Email;
                PhoneClient.Text = ClientForm.Red.Phone;
                DateClient.Text = ClientForm.Red.Birthday.ToString();
                SexClient.Text = ClientForm.Red.GenderCode;
            }
            catch (Exception)
            {
            }
        }

        public ClientUnevers( )
        {
            InitializeComponent();
            schoolEntities lang = new schoolEntities();
            lang.Clients.Load();

            if (ClientForm.Red != null) LoadCli();
        }

        public void Val_email() //Проверка емаила
        {
            //Символьная проверка
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(EmailClient.Text, pattern, RegexOptions.IgnoreCase))
            {
                e1 = true;
            }
            else
            {
                MessageBox.Show("Некорректный email");
            }
        }

        private void ClientOk_Click(object sender, RoutedEventArgs e) // Добаление и редактирование
        {
            schoolEntities lang = new schoolEntities();
            try
            {
                Val_email();

                if (e1 == true && IdClient.Content == null)
                {
                    Client cl = new Client { FirstName = NameClient.Text, LastName = FamlyClient.Text , Patronymic = OtClient.Text,Email = EmailClient.Text,Phone = PhoneClient.Text, Birthday = Convert.ToDateTime(DateClient.Text), GenderCode = SexClient.Text, PhotoPath = null, RegistrationDate = DateTime.Now};
                    lang.Clients.Add(cl);
                    lang.SaveChanges();
                    Hide();
                    new ClientForm().Show();
                }
                else
                {
                    ClientForm.Red = new Client() {ID = Convert.ToInt32(IdClient.Content.ToString()), FirstName = NameClient.Text, LastName = FamlyClient.Text, Patronymic = OtClient.Text, Email = EmailClient.Text, Phone = PhoneClient.Text, Birthday = Convert.ToDateTime(DateClient.Text), GenderCode = SexClient.Text, PhotoPath = ClientForm.Red.PhotoPath, RegistrationDate = ClientForm.Red.RegistrationDate };
                    lang.Clients.Attach(ClientForm.Red);
                    lang.Entry(ClientForm.Red).State = EntityState.Modified;
                    lang.SaveChanges();
                    Hide();
                    new ClientForm().Show();
                }
            }
            catch
            {
                MessageBox.Show("Неправильный ввод данных");
            }
        }

        private void ClientClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new ClientForm().Show();
        }
    }
}

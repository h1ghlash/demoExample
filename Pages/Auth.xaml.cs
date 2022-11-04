using demotest.AppDataFile;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace demotest.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            var Login = login.Text;
            var Password = HashPassword.hashPassword(Pass.Password);

            string query = $"SELECT Password, Login FROM [dbo].[User] WHERE Login = '{Login}' AND Password = '{Password}'";

            if (login.Text != null && Pass.Password != null)
            {
                db.openConnection();
                SqlCommand command = new SqlCommand(query, db.getConnection());
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    MainWindow mainPage = new MainWindow();
                    mainPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Данные не корректны");
                }
            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register nextPage = new Register();
            nextPage.Show();
            this.Close();
        }
    }
}

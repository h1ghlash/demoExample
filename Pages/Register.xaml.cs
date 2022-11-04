using System;
using demotest.AppDataFile;
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
using System.Configuration;
using System.Data.SqlClient;

namespace demotest.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB dB = new DB();
            var firstName = first.Text;
            var secondName = second.Text;
            var lastName = last.Text;
            var Login = login.Text;
            var IsDeleted = false;
            var password = HashPassword.hashPassword(pass.Password);
            string SqlExpression = $"INSERT INTO [dbo].[User] (Password, FirstName, MiddleName, LastName, Login, IsDeleted) VALUES('{password}', '{firstName}', '{secondName}', '{lastName}', '{Login}', '{IsDeleted}')";
            if (first.Text != null && second.Text != null && last.Text != null && login.Text != null && pass.Password != null)
            {
                  SqlCommand sqlCommand = new SqlCommand(SqlExpression, dB.getConnection());
                  dB.openConnection();
                  if(sqlCommand.ExecuteNonQuery() == 1)
                  {
                      first.Text = null;
                      second.Text = null;
                      last.Text = null;
                      login.Text = null;
                      pass.Password = null;
                      MessageBox.Show("Вы зарегистрировались!");
                      MainWindow main = new MainWindow();
                      main.Show();
                      this.Close();
                  }
                  else
                  {
                      MessageBox.Show("Ошибка");
                  }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
            dB.closeConnection();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}

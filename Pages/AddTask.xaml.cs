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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public AddTask()
        {
            InitializeComponent();
            StatusBox.Items.Add("Запланирована");
            StatusBox.Items.Add("Выполняется");
            StatusBox.Items.Add("Завершена");
            StatusBox.Items.Add("Отменена");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB dB = new DB();
            Random random = new Random();
            var title = Title.Text;
            var deadLine = Deadline.Text;
            var difficulty = Difficulty.Text;
            var time = Time.Text;
            var workType = WorkType.Text;
            var ExecutorId = random.Next(1, 7);
            var createDateTime = DateTime.Now;
            var status = StatusBox.SelectedItem;
            string SqlExpression = $"INSERT INTO [dbo].[Task] (ExecutorID, Title, Description, CreateDateTime, Deadline, Difficulty, Time, Status, WorkType, CompletedDateTime, IsDeleted) VALUES('{ExecutorId}', '{title}', '{null}', '{createDateTime}', '{deadLine}', '{difficulty}', '{time}', '{status}', '{workType}', '{null}', '{false}')";
            if (Title.Text != null && Deadline.Text != null && Difficulty.Text != null && WorkType.Text != null && Time.Text != null)
            { 
                SqlCommand sqlCommand = new SqlCommand(SqlExpression, dB.getConnection());
                dB.openConnection();
                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    Title.Text = null;
                    Deadline.Text = null;
                    Difficulty.Text = null;
                    Time.Text = null;
                    WorkType.Text = null;
                    MessageBox.Show("Успешно добавлено!");
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

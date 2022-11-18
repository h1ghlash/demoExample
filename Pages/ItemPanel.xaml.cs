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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demotest.Pages
{
    /// <summary>
    /// Логика взаимодействия для ItemPanel.xaml
    /// </summary>
    public partial class ItemPanel : UserControl
    {
        public ItemPanel()
        {
            InitializeComponent();
            this.DataContext = this;
            if(Status == "Завершена" || Status == "Отменена")
            {
                EditTask.Visibility = Visibility.Hidden;
            }
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string WorkType { get; set; }
        public string Grade { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditTask edit = new EditTask(ID, Title, Deadline, Status, WorkType);
            edit.Show();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            var sql = $"DELETE from Task WHERE Title = '{Title}'";
            var sqlCommand = new SqlCommand(sql, db.getConnection());
            if (sqlCommand.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные удалены");
            }
        }
    }
}

using demotest.AppDataFile;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace demotest.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public EditTask(int id,string title, DateTime deadline, string status, string workType)
        {
            InitializeComponent();
            TaskName.Text = title;
            TaskDateTime.Text = deadline.ToString();
            TaskStatus.Items.Add("Запланирована");
            TaskStatus.Items.Add("Выполняется");
            TaskStatus.Items.Add("Завершена");
            TaskStatus.Items.Add("Отменена");
            TaskWorkType.Text = workType;
            TaskId.Content = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            var sql = $"update Task set Title='{TaskName.Text}', Deadline='{TaskDateTime.Text}', Status='{TaskStatus.SelectedItem}', WorkType='{TaskWorkType.Text}' Where ID={TaskId.Content};";
            var sqlCommand = new SqlCommand(sql, db.getConnection());
            if(sqlCommand.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные успешно обновлены");
                this.Close();
            }
        }
    }
}

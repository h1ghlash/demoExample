using demotest.Pages;
using demotest.AppDataFile;
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
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace demotest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ItemPanel> ItemPanelCollection { get; set; }
        public string Sort = "Title";
        public string Search = string.Empty;
        public string Filter = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            ItemPanelCollection = new ObservableCollection<ItemPanel>();
            GetTasks(Sort, Filter, Search);
            GetStatus();
            CmbSort.Items.Add("Title");
            CmbSort.Items.Add("Deadline");
            CmbSort.SelectedIndex = 0;
        }

        public void GetStatus()
        {
            DB db = new DB();
            db.openConnection();
            var sql = "select Task.Status from Task group by Task.Status;";
            var sqlCommand = new SqlCommand(sql, db.getConnection());
            var rd = sqlCommand.ExecuteReader();
            CmbFilter.Items.Add("");
            while (rd.Read())
            {
                CmbFilter.Items.Add(rd.GetString(0));
            }
        }

        public void GetTasks(string sort, string filter, string search)
        {
            DB db = new DB();
            db.openConnection();
            string fil = "";
            if (filter != String.Empty)
            {
                fil = "and Task.Status = '" + filter + "' ";
            }

            string src = "";
            if (search != String.Empty)
            {
                src = $"and Task.Title LIKE '%{search}%' ";
            }
            string query = $"Select Task.ID, Title, Deadline, Status, WorkType, Grade from [dbo].[Task], [dbo].[Executor] where Task.ExecutorID = Executor.ID {fil}{src} group by Task.ID, Task.Title, Task.Status, Task.Deadline, Task.WorkType, Executor.Grade order by {sort}";
            SqlCommand command = new SqlCommand(query, db.getConnection());
            SqlDataReader dataReader = command.ExecuteReader();
            ItemPanelCollection = new ObservableCollection<ItemPanel>();
            ListTasks.ItemsSource = ItemPanelCollection;
            while (dataReader.Read())
            {
                var record = (IDataRecord)dataReader;
                ItemPanelCollection.Add(new ItemPanel()
                {
                    ID = record.GetInt32(0),
                    Title = record.GetString(1),
                    Deadline = record.GetDateTime(2),
                    Status = record.GetString(3),
                    WorkType = record.GetString(4),
                    Grade = record.GetString(5)
                });
            }
            db.closeConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask add = new AddTask();
            add.Show();
            this.Close();
        }
        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search = TextSearch.Text;
            GetTasks(Sort, Filter, Search);
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter = (string)CmbFilter.SelectedItem;
            GetTasks(Sort, Filter, Search);
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort = (string)CmbSort.SelectedItem;
            GetTasks(Sort, Filter, Search);
        }
    }
}


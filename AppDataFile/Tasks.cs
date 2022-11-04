using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demotest.AppDataFile
{
        public class MyTask
        {
            public MyTask(string Title, DateTime Deadline, string Status, string WorkType, string Executor)
            {
               title = Title;
               deadline = Deadline;
               status = Status;
               workType = WorkType; 
               executor = Executor;
            }

            public static string title { get; set; }
            public static DateTime deadline { get; set; }
            public static string status { get; set; }
            public static string workType { get; set; }
            public static string executor { get; set; } 
        }
}

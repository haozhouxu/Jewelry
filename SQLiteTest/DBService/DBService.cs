using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student;
using System.Data.SQLite;

namespace SQLiteTest.DBService
{
    public class DBService
    {
        private static string connectionUrl = @"Data Source=DB/test.db;";

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="student"></param>
        public static void UpdateStudentByGuid(Student student)
        {
            try
            {
                //打开连接
                SQLiteConnection conn = new SQLiteConnection(connectionUrl);
                conn.Open();

                //更新数据
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

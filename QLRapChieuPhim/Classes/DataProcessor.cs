using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace QLRapChieuPhim.Classes
{
    internal class DataProcessor
    {
        string strConnect = "";
        
        SQLiteConnection sqliteConn = null;

        public DataProcessor(string cinemaID)
        {
            strConnect = "Data Source = "+ Login.cinemaID + "QLRap.db";
        }

        void OpenConnect()
        {
            sqliteConn = new SQLiteConnection(strConnect);
            if (sqliteConn.State != ConnectionState.Open)
                sqliteConn.Open();
        }

        void CloseConnect()
        {
            if (sqliteConn.State != ConnectionState.Closed)
            {
                sqliteConn.Close();
            }
            sqliteConn.Dispose();
        }

        public DataTable ReadData(string sqlSelect)
        {
            DataTable dt = new DataTable();
            OpenConnect();
            SQLiteDataAdapter data = new SQLiteDataAdapter(sqlSelect, sqliteConn);
            data.Fill(dt);
            CloseConnect();
            data.Dispose();
            return dt;
        }

        public void ChangeData(string sql)
        {
            OpenConnect();
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = sql;
            command.Connection = sqliteConn;
            command.ExecuteNonQuery();
            CloseConnect();
            command.Dispose();
        }

        public int CountRecords(string tableName)
        {
            int count = 0;
            OpenConnect();
            using (SQLiteCommand command = new SQLiteCommand($"SELECT COUNT(*) FROM {tableName}", sqliteConn))
            {
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            CloseConnect();
            return count;
        }
    }
}

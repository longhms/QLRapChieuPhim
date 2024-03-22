using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace QLRapChieuPhim.Classes
{
    internal class DataProcessor
    {
        string strConnect = "Data Source = QLRappp.db";
        SQLiteConnection sqliteConn = null;

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
    }
}

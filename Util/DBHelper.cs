using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace creditcard.Util
{
    public static class DBHelper
    {

        private static MySQLDriverCS.MySQLConnection connection;
        public static MySQLDriverCS.MySQLConnection Connection
        {
            get
            {
                string connectionString = System.Configuration.ConfigurationSettings.AppSettings["connstr"].ToString();
                if (connection == null)
                {
                    connection = new MySQLDriverCS.MySQLConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        /// <summary>
        /// ִ��һ����ɾ�Ĵ洢����(�в�)
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <param name="values">�����б�</param>
        /// <returns>Ӱ������</returns>
        public static int ExecuteProc(string procName, params MySQLDriverCS.MySQLParameter[] values)
        {
             
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// ִ��һ����ѯ�޲δ洢���̣�Ҫ�ر�
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <returns>MySQLDriverCS.MySQLDataReader</returns>
        public static MySQLDriverCS.MySQLDataReader ExecuteProcSelect(string procName)
        {
             
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteReaderEx();
        }
        /// <summary>
        /// ִ��һ�����β�ѯ�洢����,ע��Ҫ�ر� 
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <param name="values">�����б�</param>
        /// <returns>MySQLDriverCS.MySQLDataReader</returns>
        public static MySQLDriverCS.MySQLDataReader ExecuteProcSelect(string procName, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteReaderEx();
        }
        /// <summary>
        /// ִ��һ���޲���ɾ�Ĵ洢����
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <returns>Ӱ������</returns>
        public static int ExecuteProc(string procName)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// ִ��һ��(�޲�)��ɾ�����
        /// </summary>
        /// <param name="safeSql">���</param>
        /// <returns>Ӱ������</returns>
        public static int ExecuteCommand(string safeSql)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(safeSql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// ִ��һ���в���ɾ�Ĳ���
        /// </summary>
        /// <param name="sql">���</param>
        /// <param name="values">����</param>
        /// <returns>Ӱ������ </returns>
        public static int ExecuteCommand(string sql, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// ��ѯ��һ�е�һ�����ݣ��޲Σ������ص���ʲô���;�ת����ʲô���ͣ�
        /// </summary>
        /// <param name="safeSql">���</param>
        /// <returns>object</returns>
        public static object GetScalar(string safeSql)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(safeSql, Connection);
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// ��ѯ��һ�е�һ�����ݣ��вΣ������ص���ʲô���;�ת����ʲô���ͣ�
        /// </summary>
        /// <param name="values">����</param>
        /// <returns>object</returns>
        public static object GetScalar(string safeSql, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(safeSql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// ����һ��MySQLDriverCS.MySQLDataReader��ע��Ҫ�رգ�
        /// </summary>
        /// <param name="safeSql">���</param>
        /// <returns>MySQLDriverCS.MySQLDataReader</returns>
        public static MySQLDriverCS.MySQLDataReader GetReader(string safeSql)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(safeSql, Connection);
            MySQLDriverCS.MySQLDataReader reader = cmd.ExecuteReaderEx();
            return reader;
        }
        /// <summary>
        /// ����int
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int GetScalarInt(string sql, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// ����string
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetScalarString(string sql, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return Convert.ToString(cmd.ExecuteScalar());
        }

        /// <summary>
        /// ����һ���в�MySQLDriverCS.MySQLDataReader��ע��Ҫ�رգ�
        /// </summary>
        /// <param name="sql">���</param>
        /// <param name="values">����</param>
        /// <returns>MySQLDriverCS.MySQLDataReader</returns>
        public static MySQLDriverCS.MySQLDataReader GetReader(string sql, params MySQLDriverCS.MySQLParameter[] values)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            MySQLDriverCS.MySQLDataReader reader = cmd.ExecuteReaderEx();
            return reader;
        }
        /// <summary>
        /// ����һ��Datatable���޲Σ�
        /// </summary>
        /// <param name="safeSql">���</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
             
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand("set names gbk", Connection);
            cmd.ExecuteNonQuery();
            cmd = new MySQLDriverCS.MySQLCommand(safeSql, Connection);
            MySQLDriverCS.MySQLDataAdapter da = new MySQLDriverCS.MySQLDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// ����һ��Datatable���вΣ�
        /// </summary>
        /// <param name="sql">���</param>
        /// <param name="values">����</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataSet(string sql, params MySQLDriverCS.MySQLParameter[] values)
        {
            DataSet ds = new DataSet();
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            MySQLDriverCS.MySQLDataAdapter da = new MySQLDriverCS.MySQLDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">����SQL���</param>  
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            MySQLDriverCS.MySQLCommand cmd = new MySQLDriverCS.MySQLCommand();
            cmd.Connection = Connection;

            MySQLDriverCS.MySQLTransaction tx = (MySQLDriverCS.MySQLTransaction)Connection.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
        }
    }
}

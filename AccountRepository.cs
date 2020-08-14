using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Board.Models
{
    public class AccountRepository
    {
        //private IDbConnection db;
        private SqlConnection con;

        public AccountRepository()
        {
            //db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public AccountData Add(AccountData data)
        {
            var sql = @"INSERT INTO Accounts (
                    [Id],
                    [Password],
                    [Name],
                    [Email]
                )
                VALUES (
                    @Id,
                    @Password,
                    @Name,
                    @Email
                ); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var no = con.Query<int>(sql, data).Single();

            data.No = no;
            return data;
        }

        public void Delete(AccountData data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"DELETE FROM Accounts WHERE WHERE [Id] = @Id AND [Password] = @Password";
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
            cmd.Parameters["@Id"].Value = data.Id;
            cmd.Parameters["@Password"].Value = data.Password;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Modify(AccountData data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"UPDATE Accounts SET [Name] = @Name, [Email] = @Email, [Password] = @Password WHERE [Id] = @Id";
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
            cmd.Parameters["@Id"].Value = data.Id;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
            cmd.Parameters["@Password"].Value = data.Password;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = data.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = data.Email;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public AccountData Login(AccountData data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"SELECT COUNT(*) FROM Accounts WHERE [Id] = @Id AND [Password] = @Password";           
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
            cmd.Parameters["@Id"].Value = data.Id;
            cmd.Parameters["@Password"].Value = data.Password;

            int result = (int)cmd.ExecuteScalar();
            if (result == 1)
            {
                cmd.CommandText = @"SELECT * FROM Accounts WHERE [Id] = @Id AND [Password] = @Password";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    data.No = reader["No"].GetHashCode();
                    data.Id = reader["Id"].ToString();
                    data.Password = reader["Password"].ToString();
                    data.Name = reader["Name"].ToString();
                    data.Email = reader["Email"].ToString();
                }
                else
                {
                    data = null;
                }
                reader.Close();
            }
            else
            {
                data = null;
            }
            con.Close();

            return data;
        }

        public AccountData GetAccount(string Name)
        {
            SqlCommand cmd = new SqlCommand();
            AccountData data = new AccountData();
            cmd.Connection = con;

            cmd.CommandText = @"SELECT * FROM Accounts WHERE [Name] = @Name";
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = Name;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                data.No = reader["No"].GetHashCode();
                data.Id = reader["Id"].ToString();
                data.Password = reader["Password"].ToString();
                data.Name = reader["Name"].ToString();
                data.Email = reader["Email"].ToString();
            }
            else
            {
                data = null;
            }
            reader.Close();
            con.Close();

            return data;
        }

        public bool CheckAccount(AccountData data, bool OnlyName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            if (OnlyName)
            {
                cmd.CommandText = @"SELECT COUNT(*) FROM Accounts WHERE [Name] = @Name";
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = data.Name;
            }
            else
            {
                cmd.CommandText = @"SELECT COUNT(*) FROM Accounts WHERE [Id] = @Id OR [Name] = @Name";
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
                cmd.Parameters["@Id"].Value = data.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = data.Name;
            }

            int result = (int)cmd.ExecuteScalar();
            con.Close();

            if (result == 0) return true;
            else return false;
        }

        public void InitAccountDB()
        {
            var sql = "DELETE FROM Accounts";
            con.Query(sql);
            sql = "DBCC CHECKIDENT(Accounts, RESEED, 0)";
            con.Query(sql);
        }

        /// <summary>
        /// 출력
        /// </summary>
        /// <returns></returns>
        public List<AccountData> GetAll()
        {
            string sql = "SELECT * FROM Accounts ODER BY No";
            return con.Query<AccountData>(sql).ToList();
        }
    }
}

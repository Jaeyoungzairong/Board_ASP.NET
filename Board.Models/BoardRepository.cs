using Dapper;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Models
{
    public class BoardRepository
    {
        //private IDbConnection db;
        private SqlConnection con;

        public BoardRepository()
        {
            //db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public BoardData Add(BoardData data)
        {
            //[Password],
            //[PostIp],
            //[Encoding],
            //[Homepage],
            //[ModifyDate],
            //[ModifyIp]
            //@Password,
            //@PostIp,
            //@Encoding,
            //@Homepage,
            //@ModifyDate,
            //@ModifyIp

            var sql = @"
                INSERT INTO Boards (
                    [Name],
                    [Email],
                    [Title],
                    [PostDate],
                    [Content],
                    [ReadCount],
                    [ModifyDate]
                )
                Values (
                    @Name,
                    @Email,
                    @Title,
                    @PostDate,
                    @Content,
                    0,
                    @ModifyDate
                ); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var id = con.Query<int>(sql, data).Single();

            data.Id = id;
            return data;
        }

        public void Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"DELETE FROM Boards WHERE [Id] = @Id";
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Modify(BoardData data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"UPDATE Boards SET [Title] = @Title, [Content] = @Content, [ModifyDate] = @ModifyDate WHERE [Id] = @Id";
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = data.Id;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters["@Title"].Value = data.Title;
            cmd.Parameters.Add("@Content", SqlDbType.NVarChar);
            cmd.Parameters["@Content"].Value = data.Content;
            cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime);
            cmd.Parameters["@ModifyDate"].Value = data.ModifyDate;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ModifyName(string OldName, string NewName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = @"UPDATE Boards SET [Name] = @NewName WHERE [Name] = @OldName";
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar);
            cmd.Parameters["@NewName"].Value = NewName;
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar);
            cmd.Parameters["@OldName"].Value = OldName;

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public BoardData View(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            BoardData data = new BoardData();
            cmd.Connection = con;

            cmd.CommandText = @"SELECT * FROM Boards WHERE [Id] = @Id";

            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                data.Id = reader["Id"].GetHashCode();
                data.Name = reader["Name"].ToString();
                data.Email = reader["Email"].ToString();
                data.Title = reader["Title"].ToString();
                data.PostDate = Convert.ToDateTime(reader["PostDate"]);
                data.Content = reader["Content"].ToString();
                data.ReadCount = Convert.ToInt32(reader["ReadCount"]);
                data.ModifyDate = Convert.ToDateTime(reader["ModifyDate"]);
            }
            else
            {
                data = null;
            }

            reader.Close();
            con.Close();

            return data;
        }

        public void UpdateReadCount(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "UPDATE Boards SET [ReadCount] = [ReadCount] + 1 WHERE [Id] = @Id";
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;


            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<BoardData> GetAll()
        {
            string sql = "Select * From Boards Order By Id Desc";
            return con.Query<BoardData>(sql).ToList();
        }

        public List<BoardData> SearchTitle(string Value)
        {
            var sql = "SELECT * FROM Boards WHERE Title LIKE N'%" + Value + "%' ORDER BY Id DESC";
            return con.Query<BoardData>(sql).ToList();
        }

        public List<BoardData> SearchName(string Value)
        {
            var sql = "SELECT * FROM Boards WHERE Name LIKE N'%" + Value + "%' ORDER BY Id DESC";
            return con.Query<BoardData>(sql).ToList();
        }

        public List<BoardData> SearchTitleAndContent(string Value)
        {
            var sql = "SELECT * FROM Boards WHERE Title LIKE N'%" + Value + "%' OR Content LIKE N'%" + Value + "%' ORDER BY Id DESC";
            return con.Query<BoardData>(sql).ToList();
        }

        public void InitBoarddDB()
        {
            var sql = "DELETE FROM Boards";
            con.Query(sql);
            sql = "DBCC CHECKIDENT(Boards, RESEED, 0)";
            con.Query(sql);
        }
    }
}

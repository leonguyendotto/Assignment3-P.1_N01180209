using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_N01180209.Models;
using MySql.Data.MySqlClient;

namespace Assignment3_N01180209.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<string> TeacherList()
        {

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<String> TeacherNames = new List<string> { };

            while (ResultSet.Read())
            {
                string TeacherName = ResultSet["teacherfname" ] + " " + ResultSet[ "teacherlname"];
                TeacherNames.Add(TeacherName);
            }

            Conn.Close();

            return TeacherNames;
        }
    }
}

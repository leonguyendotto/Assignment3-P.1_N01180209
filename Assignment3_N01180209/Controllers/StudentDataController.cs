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
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route ("api/ClassData/CLassLists/{SearchKey?}")]
        public IEnumerable<Student> ListStudents (string Searchkey = null)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query 
            cmd.CommandText = "Select * from Students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) " +
                 "or lower(concat(studentfname, ' ' ,studentlname)) like lower(@key) or lower(studentnumber) like (@key) or enroldate like (@key) or studentid like (@key)";

            //Prevent SQL Injection Attack
            cmd.Parameters.AddWithValue("@key", "%" + Searchkey + "%");
            cmd.Prepare();


            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Student> Students = new List<Student> { };
            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                /*int StudentId = (int)ResultSet["studentid"]; This one broke the webpage when I tried to load it */
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];

                Student NewStudent = new Student();
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.EnrolDate = EnrolDate;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;

                Students.Add(NewStudent); 
            }

            Conn.Close();
            return Students;
        }

        /// <summary>
        /// Return a detail Students information
        /// </summary>
        /// <example>GET api/StudentData/FindStudent/{id}</example>
        /// <param name="id">An interger</param>
        /// <returns>The information of a student based on the student id</returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query - In  reality this line will be changed if needed.
            cmd.CommandText = "Select * from students where studentid =" + id;

            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];


                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.EnrolDate = EnrolDate;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
            }

            return NewStudent;
        }
    }
    
}

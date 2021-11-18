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
        //This is the API controller that listen to the Database request and provide information from School SQL DB
        //The database context class which allows us to access our MySQL Database
        private SchoolDbContext School = new SchoolDbContext();
        
        //This Controller will access the teachers table of our school database
        /// <summary>
        /// Returns a list of Teachers in the systeam
        /// </summary>
        /// <example>GET api/TeacherData/TeacherList</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> TeacherList()
        {
            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Select * from Teachers";

            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers 
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row the result set
            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];


                //Create a new Teacher object 
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Teacher Name to the list
                Teachers.Add(NewTeacher);
            }
            
            //CLose the connection between the MySQL Database and the Web server
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query - In  reality this line will be changed if needed.
            cmd.CommandText = "Select * from Teachers where teacherid =" + id;

            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;
        }

    }
}

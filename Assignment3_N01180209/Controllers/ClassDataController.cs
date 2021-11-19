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
    public class ClassDataController : ApiController
    {
        //The database context class which allows us to access our MySQL Database
        private SchoolDbContext School = new SchoolDbContext();



        /// <summary>
        /// Returns a list of data within Classes table
        /// </summary>
        /// <example>GET aip/ClassData/Classlists</example>
        /// <returns>A list of Classes information: code, name, start date, fnish date
        /// </returns>
        [HttpGet]
        [Route("api/ClassData/CLassLists/{SearchKey?}")]
        public IEnumerable<Class> ClassLists(string SearchKey = null)
        {
            //Instance of connection 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between database and web server
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Classes where lower(classname) like lower(@key) or lower(classcode) like lower(@key) " +
                "or lower(concat(classname, ' ' ,classcode)) like lower(@key) or startdate like (@key) or finishdate like (@key)";

            //Prevent SQL Injection Attack
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Class> Classes = new List<Class> { };

            //Loop through each row the result set
            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime EndDate = (DateTime)ResultSet["finishdate"];
                string ClassCode = (string)ResultSet["classcode"];
                string ClassName = (string)ResultSet["classname"];



                //Create a new Class object 
                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.EndDate = EndDate;


                //Add the Class Name to the list
                Classes.Add(NewClass);
            }

            //CLose the connection between the MySQL Database and the Web server
            Conn.Close();

            //Return the final list of teacher names
            return Classes;
        }

        /// <summary>
        /// Return a detail Classes information
        /// </summary>
        /// <example>GET api/ClassData/FindClass/{id}</example>
        /// <param name="id">An interger</param>
        /// <returns>The information of a teacher based on the teacher id</returns>
        [HttpGet]
        public Class FindClass (int id)
        {
            Class NewClass = new Class();

            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query - In  reality this line will be changed if needed.
            cmd.CommandText = "Select * from Classes where classid =" + id;

            //Gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access the column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime EndDate = (DateTime)ResultSet["finishdate"];
                string ClassCode = (string)ResultSet["classcode"];
                string ClassName = (string)ResultSet["classname"];

                
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.EndDate = EndDate;
            }

            return NewClass;
        }
    }
}

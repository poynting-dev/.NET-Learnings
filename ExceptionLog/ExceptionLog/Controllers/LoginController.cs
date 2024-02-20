using ExceptionLog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExceptionLog.Controllers
{
    public class LoginController : Controller
    {
        string connectionStr = ConfigurationManager.ConnectionStrings["loginDB"].ConnectionString;

        // GET: Login
        public ActionResult Index()
        {
            List<loginCred> usersId = new List<loginCred>();
            string query = "SELECT * FROM loginCred";
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //str.Add(dr.GetValue(0).ToString());
                    usersId.Add(new loginCred
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                    });
                }
            }
            return View(usersId);
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            


            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(loginCred newCredentials)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = new SqlCommand("usercrudops", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@username", newCredentials.username);
                        command.Parameters.AddWithValue("@password", newCredentials.password);
                        command.Parameters.AddWithValue("@status", "INSERT");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            loginCred user = new loginCred();
            string query = "SELEC * FROM loginCred WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //str.Add(dr.GetValue(0).ToString());
                    user.Id = Int32.Parse(reader["Id"].ToString());
                    user.username = reader["username"].ToString();
                    user.password = reader["password"].ToString();
                }
            }



            return View(user);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, loginCred newCredentials)
        {
            //try
            //{
                string query = "UPDATE loginCred SET username=@username, password=@password WHERE Id=@id";
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@username", newCredentials.username);
                    command.Parameters.AddWithValue("@password", newCredentials.password);

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

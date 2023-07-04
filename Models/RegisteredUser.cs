using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MiniProject.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MiniProject.Models
{
    public class RegisteredUser
    {
        public bool isActive { get; set; }
        public int CityId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Login Name")]
        public string LoginName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Please enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

       
        public static List<RegisteredUser> DisplayAllUsers()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                List<RegisteredUser> lst = new List<RegisteredUser>();

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectUser";


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RegisteredUser u = new RegisteredUser();
                    u.CityId = (int)dr["CityId"];
                    u.LoginName = dr["LoginName"].ToString();
                    u.FullName = dr["FullName"].ToString();
                    u.Password = dr["Password"].ToString();
                    u.Gender = dr["Gender"].ToString();
                    u.EmailId = dr["EmailId"].ToString();
                    u.PhoneNumber = dr["PhoneNumber"].ToString();

                    lst.Add(u);
                }
                return lst;
                dr.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
           
        }
        public static RegisteredUser GetSingleUser(string LoginName)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
                RegisteredUser c = new RegisteredUser();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from RegisteredUser where LoginName=@LoginName";
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    c.CityId = (int)dr["CityId"];
                    c.LoginName = dr["LoginName"].ToString();
                    c.FullName = dr["FullName"].ToString();
                    c.Password = dr["Password"].ToString();
                    c.Gender = dr["Gender"].ToString();
                    c.EmailId = dr["EmailId"].ToString();
                    c.PhoneNumber = dr["PhoneNumber"].ToString();
                    return c;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }
        public static void Insert(RegisteredUser e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertUser";
                cmd.Parameters.AddWithValue("@CityId", e.CityId);
                cmd.Parameters.AddWithValue("@LoginName", e.LoginName);
                cmd.Parameters.AddWithValue("@FullName", e.FullName);
                cmd.Parameters.AddWithValue("@Password", e.Password);               
                cmd.Parameters.AddWithValue("@Gender", e.Gender);
                cmd.Parameters.AddWithValue("@EmailId", e.EmailId);
                cmd.Parameters.AddWithValue("@PhoneNumber", e.PhoneNumber);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

        }
        public static void Update(RegisteredUser e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateUser";
                cmd.Parameters.AddWithValue("@CityId", e.CityId);
                cmd.Parameters.AddWithValue("@LoginName", e.LoginName);
                cmd.Parameters.AddWithValue("@FullName", e.FullName);
                cmd.Parameters.AddWithValue("@Password", e.Password);
                cmd.Parameters.AddWithValue("@Gender", e.Gender);
                cmd.Parameters.AddWithValue("@EmailId", e.EmailId);
                cmd.Parameters.AddWithValue("@PhoneNumber", e.PhoneNumber);
                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }
        public static void Delete(string LoginName)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUser";
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                cn.Close();
            }

        }

    }
}

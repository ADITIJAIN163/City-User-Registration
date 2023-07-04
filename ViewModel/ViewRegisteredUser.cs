using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MiniProject.Models;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MiniProject.ViewModel
{
    public class ViewRegisteredUser
    {
       

       

        public IEnumerable<SelectListItem> Citydropdown { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

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

        public bool isActive { get; set; }

                  


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
       
        public static ViewRegisteredUser GetSingleUserV(string LoginName)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                
                
                ViewRegisteredUser c = new ViewRegisteredUser();
                cn.Open();
               
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from RegisteredUser where LoginName=@LoginName";
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                List<SelectListItem> lst = DisplayAllCitiesdd();
                SqlDataReader dr = cmd.ExecuteReader();
                c.Citydropdown = lst;

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
        public static void UpdateV(string LoginName,ViewRegisteredUser v1)
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
                cmd.Parameters.AddWithValue("@CityId", v1.CityId);
                cmd.Parameters.AddWithValue("@LoginName",LoginName);
                cmd.Parameters.AddWithValue("@FullName", v1.FullName);
               
                cmd.Parameters.AddWithValue("@Gender", v1.Gender);
                cmd.Parameters.AddWithValue("@EmailId", v1.EmailId);
                cmd.Parameters.AddWithValue("@PhoneNumber", v1.PhoneNumber);             

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
        public static bool IsValidUser(RegisteredUser u)
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
                cmd.CommandText = "select * from RegisteredUser where LoginName=@LoginName and Password=@Password";
                cmd.Parameters.AddWithValue("@LoginName", u.LoginName);
                cmd.Parameters.AddWithValue("@Password", u.Password);              
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()==true)
                {
                    u.FullName = dr["FullName"].ToString();
                    return true;
                }
                else
                    return false;
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
        public static RegisteredUser IsValidUserV(string ln,string p)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                RegisteredUser u = null;
                cn.Open();
                RegisteredUser c = new RegisteredUser();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from RegisteredUser where LoginName=@LoginName and Password=@Password";
                cmd.Parameters.AddWithValue("@LoginName", ln);
                cmd.Parameters.AddWithValue("@Password", p);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    u = new RegisteredUser();
                    u.FullName = dr["FullName"].ToString();
                    return u;
                }
                else
                    return u;
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
        public static List<SelectListItem> DisplayAllCitiesdd()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            try
            {
                List<SelectListItem> lst = new List<SelectListItem>();                
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Cities";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(new SelectListItem(dr.GetString("CityName"),dr.GetInt32("CityId").ToString()));
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

       public static RegisteredUser IsValidName(string ln)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                RegisteredUser u = null;
                cn.Open();
                RegisteredUser c = new RegisteredUser();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from RegisteredUser where LoginName=@LoginName";
                cmd.Parameters.AddWithValue("@LoginName", ln);               
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    u = new RegisteredUser();
                    u.FullName = dr["FullName"].ToString();
                    return u;
                }
                else
                    return u;
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
        public static Dictionary<string, List<ViewRegisteredUser>> ViewAll1()
        {
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True";
                try
                {                   
                    Dictionary<string,List<ViewRegisteredUser>> lst = new Dictionary<string, List<ViewRegisteredUser>>();
                    
                    cn.Open();
                    SqlCommand cmdc = new SqlCommand();
                    cmdc.Connection = cn;
                    cmdc.CommandType = CommandType.StoredProcedure;
                    cmdc.CommandText = "SelectCity";
                    SqlCommand cmdu = new SqlCommand();
                    cmdu.Connection = cn;
                    cmdu.CommandType = CommandType.Text;
                    SqlDataReader drc = cmdc.ExecuteReader();

                    while (drc.Read())
                    {
                        ViewRegisteredUser u = new ViewRegisteredUser();
                        u.CityName = drc["CityName"].ToString();
                        List<ViewRegisteredUser> lstv = new List<ViewRegisteredUser>();
                        cmdu.CommandText = "Select FullName,Gender,EmailId,PhoneNumber from RegisteredUser where CityId=" + drc["CityId"];
                        SqlDataReader dru = cmdu.ExecuteReader();

                        while (dru.Read())
                        {
                            ViewRegisteredUser u1 = new ViewRegisteredUser();
                            u1.FullName = dru["FullName"].ToString();
                            u1.Gender = dru["Gender"].ToString();
                            u1.EmailId = dru["EmailId"].ToString();
                            u1.PhoneNumber = dru["PhoneNumber"].ToString();
                            lstv.Add(u1);
                        }
                        lst.Add(u.CityName, lstv);
                        dru.Close();
                    }

                    drc.Close();
                  
                    return lst;

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
}


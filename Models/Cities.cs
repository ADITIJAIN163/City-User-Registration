using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniProject.Models
{
    public class Cities
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public static List<Cities> DisplayAllCities()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                List<Cities> lst = new List<Cities>();
               
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectCity";
                

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cities c = new Cities();
                    c.CityId = (int)dr["CityId"];
                    c.CityName = dr["CityName"].ToString();
                   lst.Add(c);
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
        public static Cities GetSingleCity(int CityId)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
                Cities c = new Cities();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Cities where CityId=@CityId";
                cmd.Parameters.AddWithValue("@CityId", CityId);
                SqlDataReader dr = cmd.ExecuteReader();
       

                if (dr.Read())
                {
                    c.CityId = (int)dr["CityId"];
                    c.CityName = dr["CityName"].ToString();
                    return c;
                }
                else
                    return c;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }
        public static void Insert(Cities e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertCity";
                cmd.Parameters.AddWithValue("@CityId", e.CityId);
                cmd.Parameters.AddWithValue("@CityName", e.CityName);               
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
        public static void Update( Cities e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateCity";
                cmd.Parameters.AddWithValue("@CityId", e.CityId);
                cmd.Parameters.AddWithValue("@CityName", e.CityName);           

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
        public static void Delete(int CityId)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                cn.Open();
 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteCity";
                cmd.Parameters.AddWithValue("@CityId", CityId);
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

        public static List<SelectListItem> DisplayAllCitiesdd()
        {
            try
            {
                List<SelectListItem> lst = new List<SelectListItem>();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yash1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Cities";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(new SelectListItem(dr.GetString("CityName"), dr.GetInt32("CityId").ToString()));
                }
                return lst;
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

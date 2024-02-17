using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace FoodLibrary
{
    public class FoodDAL
    {
        public string str = ConfigurationManager.ConnectionStrings["FoodManagementDB"].ConnectionString;
        public bool AddFoodItem(FoodMaster item)
        {
            bool status = false;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertFoodItem", con);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_FID", item.FId);
                cmd.Parameters.AddWithValue("@p_FName", item.FName);
                cmd.Parameters.AddWithValue("@p_FPrice", item.FPrice);
                con.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            return status;
        }

        public bool EditFoodItem(FoodMaster item, int FId)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateFoodItems]", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_FID", item.FId);
                cmd.Parameters.AddWithValue("@p_FName", item.FName);
                cmd.Parameters.AddWithValue("@p_FPrice", item.FPrice);
                cn.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return status;
        }
        public bool RemoveFoodItem(int FId)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("DELETE FROM FoodItems WHERE FId = @FId", cn);
            cmd.Parameters.AddWithValue("@FId", FId);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return status;
        }


        public List<FoodMaster> GetFoodItemList()
        {
            List<FoodMaster> itemlist = new List<FoodMaster>();
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from FoodItems", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                FoodMaster item = new FoodMaster();
                item.FId = Convert.ToInt32(dr["FId"]);
                item.FName = dr["FName"].ToString();
                item.FPrice = Convert.ToSingle(dr["FPrice"]);
                itemlist.Add(item);
            }
            con.Close();
            con.Dispose();
            return itemlist;
        }
        public FoodMaster FindFoodItem(int FId)
        {
            FoodMaster item = new FoodMaster();
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from FoodItems where FId = " + FId, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                item.FId = Convert.ToInt32(dr["FId"]);
                item.FName = dr["FName"].ToString();
                item.FPrice = Convert.ToSingle(dr["FPrice"]);
            }
            cn.Close();
            cn.Dispose();
            return item;
        }
    }
}

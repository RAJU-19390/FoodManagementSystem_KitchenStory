using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace AdminLibrary
{
    public class AdminDAL
    {
        public string connectionString;
        public AdminDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["FoodManagementDB"].ConnectionString;
        }
        public bool AddAdmin(AdminMaster admin)
        {
            bool status = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].sp_InsertAdmin", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_AdminName", admin.AdminName);
                        command.Parameters.AddWithValue("@p_Email", admin.Email);
                        command.Parameters.AddWithValue("@p_Password", admin.Password);

                        connection.Open();
                        command.ExecuteNonQuery();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return status;
        }
        public List<AdminMaster> GetAdminList()
        {
            List<AdminMaster> adminList = new List<AdminMaster>();
            string connectionString = ConfigurationManager.ConnectionStrings["FoodManagementDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Admin", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminMaster admin = new AdminMaster
                            {
                                AdminId = Convert.ToInt32(reader["AdminId"]),
                                AdminName = reader["AdminName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };

                            adminList.Add(admin);
                        }
                    }
                }
            }

            return adminList;
        }
        public bool ValidateAdminLogin(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public bool ValidateAdminMail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public bool UpdateAdminPassword(string email, string newPassword)
        {
            bool status = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].sp_UpdateAdminPassword", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_Email", email);
                        command.Parameters.AddWithValue("@p_NewPassword", newPassword);

                        connection.Open();
                        command.ExecuteNonQuery();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return status;
        }

    }
}

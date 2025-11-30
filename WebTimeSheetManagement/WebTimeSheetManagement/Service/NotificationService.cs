using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Service
{
    public class NotificationService
    {
        static readonly string connString = ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString();
        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <returns></returns>
        public static string GetNotification()
        {
            try
            {
                var messages = new List<NotificationsTB>();
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (command = new SqlCommand(@"  
    SELECT [NotificationsID],[Status],[Message] FROM [TimesheetDB].[dbo].[NotificationsTB]  
  where [TimesheetDB].[dbo].[NotificationsTB].Status ='A' 
  and GETDATE() between FromDate and ToDate", connection))
                    {
                        command.Notification = null;
                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            messages.Add(new NotificationsTB
                            {
                                NotificationsID = (int)reader["NotificationsID"],
                                Status = reader["Status"] != DBNull.Value ? (string)reader["Status"] : string.Empty,
                                Message = reader["Message"] != DBNull.Value ? (string)reader["Message"] : string.Empty
                            });
                        }
                    }
                }
                return JsonSerializer.Serialize(messages);
            }
            catch (Exception)
            {
                return null;
            }


        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;
            }
            // SignalR migration pending: hook into ASP.NET Core Hub here.
        }
    }
}
using Dashboard.Main.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Main.Controllers
{
    public class CardsController : Controller
    {
        // GET: Cards
        public ActionResult Index()
        {
            List<Cards> cardList = new List<Cards>();
            string CS = ConfigurationManager.ConnectionStrings["crudconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS)) {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [crud].[Cards]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    var card = new Cards();

                    card.CardID = Convert.ToInt32(rdr["card_id"]);
                    card.CardDescription = rdr["card_description"].ToString();
                    card.DateCreated = Convert.ToDateTime(rdr["date_created"]);
                    card.DateUpdated = Convert.ToDateTime(rdr["date_updated"]);
                    card.Status = rdr["status"].ToString();
                    cardList.Add(card);
                }
            }
            return View(cardList);
        }
    }
}
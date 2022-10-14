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

                    card.CardID = Convert.ToInt32(rdr["CardID"]);
                    card.CardDescription = rdr["CardDescription"].ToString();
                    card.DateCreated = Convert.ToDateTime(rdr["DateCreated"]);
                    card.DateUpdated = Convert.ToDateTime(rdr["DateUpdated"]);
                    card.Status = rdr["Status"].ToString();
                    cardList.Add(card);
                }
            }
            return View(cardList);
        }
    }
}
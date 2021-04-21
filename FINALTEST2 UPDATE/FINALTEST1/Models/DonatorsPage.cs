using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINALTEST1.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace FINALTEST1.Models
{
    public class DonatorsPage
    {
        public int MonetaryID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Boolean Validate { get; set; }

        private DonatorsDataAccessLayer dal = new DonatorsDataAccessLayer();

        //List codes

        //[List codes]Default
        public List<DonatorsPage> GetAll()
        {
            List<DonatorsPage> list = new List<DonatorsPage>();
            dal.Open();
            dal.SetSql("select Users.UserID, Monetaries.MonetaryID, Users.FirstName, Users.LastName, Monetaries.Amount, Monetaries.Date, Monetaries.Validate from Users inner join Monetaries on Users.UserID=Monetaries.UserID");
            SqlDataReader dr = dal.GetReader();
            while (dr.Read() == true)
            {
                DonatorsPage dn = new DonatorsPage();
                dn.UserID = (int)dr["UserID"];
                dn.MonetaryID = (int)dr["MonetaryID"];
                dn.FirstName = dr["FirstName"].ToString();
                dn.LastName = dr["LastName"].ToString();
                dn.Amount = (decimal)dr["Amount"];
                dn.Date = (DateTime)dr["Date"];
                dn.Validate = (bool)dr["Validate"];

                list.Add(dn);
            }
            dr.Close();
            dal.Close();

            return list;
        }

        //[List codes]Top
        public List<DonatorsPage> GetAllTop()
        {
            List<DonatorsPage> list = new List<DonatorsPage>();
            dal.Open();
            dal.SetSql("select Users.UserID, Monetaries.MonetaryID, Users.FirstName, Users.LastName, Monetaries.Amount, Monetaries.Date, Monetaries.Validate from Users inner join Monetaries on Users.UserID=Monetaries.UserID order by Monetaries.Amount desc");
            SqlDataReader dr = dal.GetReader();
            while (dr.Read() == true)
            {
                DonatorsPage dn = new DonatorsPage();
                dn.UserID = (int)dr["UserID"];
                dn.MonetaryID = (int)dr["MonetaryID"];
                dn.FirstName = dr["FirstName"].ToString();
                dn.LastName = dr["LastName"].ToString();
                dn.Amount = (decimal)dr["Amount"];
                dn.Date = (DateTime)dr["Date"];
                dn.Validate = (bool)dr["Validate"];

                list.Add(dn);
            }
            dr.Close();
            dal.Close();

            return list;
        }

        //[List codes]Recent
        public List<DonatorsPage> GetAllRecent()
        {
            List<DonatorsPage> list = new List<DonatorsPage>();
            dal.Open();
            dal.SetSql("select Users.UserID, Monetaries.MonetaryID, Users.FirstName, Users.LastName, Monetaries.Amount, Monetaries.Date, Monetaries.Validate from Users inner join Monetaries on Users.UserID=Monetaries.UserID order by Monetaries.Date desc");
            SqlDataReader dr = dal.GetReader();
            while (dr.Read() == true)
            {
                DonatorsPage dn = new DonatorsPage();
                dn.UserID = (int)dr["UserID"];
                dn.MonetaryID = (int)dr["MonetaryID"];
                dn.FirstName = dr["FirstName"].ToString();
                dn.LastName = dr["LastName"].ToString();
                dn.Amount = (decimal)dr["Amount"];
                dn.Date = (DateTime)dr["Date"];
                dn.Validate = (bool)dr["Validate"];

                list.Add(dn);
            }
            dr.Close();
            dal.Close();

            return list;
        }

        //Edit codes

        //[Edit codes]Edit First and Last Names
        public DonatorsPage Get(int uID)
        {
            DonatorsPage dn = new DonatorsPage();

            dal.Open();
            dal.SetSql("SELECT Users.UserID, Users.FirstName, Users.LastName FROM Users WHERE Users.UserID = @uID");
            dal.AddParameter("@uID", uID);
            SqlDataReader dr = dal.GetReader();
            if (dr.Read() == true)
            {
                dn.UserID = (int)dr["UserID"];
                dn.FirstName = dr["FirstName"].ToString();
                dn.LastName = dr["LastName"].ToString();
            }
            dr.Close();
            dal.Close();

            return dn;
        }

        public void Edit()
        {
            dal.Open();
            dal.SetSql("UPDATE Users SET Users.FirstName = @fn, Users.LastName = @ln WHERE Users.UserID = @uID");
            dal.AddParameter("@fn", FirstName);
            dal.AddParameter("@ln", LastName);
            dal.AddParameter("@uID", UserID);
            dal.Execute();
            dal.Close();
        }

        //[Edit codes]Edit Amount and Validation
        public DonatorsPage GetMonetary(int mID)
        {
            DonatorsPage dn = new DonatorsPage();

            dal.Open();
            dal.SetSql("SELECT Monetaries.MonetaryID, Monetaries.Amount, Monetaries.Validate FROM Monetaries WHERE Monetaries.MonetaryID = @mID");
            dal.AddParameter("@mID", mID);
            SqlDataReader dr = dal.GetReader();
            if (dr.Read() == true)
            {
                dn.MonetaryID = (int)dr["MonetaryID"];
                dn.Amount = (decimal)dr["Amount"];
                dn.Validate = (bool)dr["Validate"];
            }
            dr.Close();
            dal.Close();

            return dn;
        }

        public void EditMonetary()
        {
            dal.Open();
            dal.SetSql("UPDATE Monetaries SET Monetaries.Amount = @a, Monetaries.Validate = @v WHERE Monetaries.MonetaryID = @mID");
            dal.AddParameter("@a", Amount);
            dal.AddParameter("@v", Validate);
            dal.AddParameter("@mID", MonetaryID);
            dal.Execute();
            dal.Close();
        }

        //Delete
        public void Delete()
        {
            dal.Open();
            dal.SetSql("DELETE Users WHERE UserID = @uID");
            dal.AddParameter("@uID", UserID);
            dal.Execute();
            dal.Close();
        }
    }
}

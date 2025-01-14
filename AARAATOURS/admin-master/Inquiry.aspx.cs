﻿using AARAATOURS.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AARAATOURS.Common;

namespace AARAATOURS.admin_master
{
    public partial class Inquiry : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DD\\SQLEXPRESS;Initial Catalog=tours;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                display_data();
            }

        }

        protected void dispalytable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRecord")
            {
                int recordId = Convert.ToInt32(e.CommandArgument);
                DeleteRecord(recordId);
            }

        }




        private void DeleteRecord(int recordId)
        {
            try
            {

                string sql = "DELETE FROM [inquiry] WHERE id = " + recordId;
                int response = Services.execute(sql, con);
                if (response == 1)
                {
                    Response.Redirect("inquiry.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Something goes Wrong in deleting !')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }


        }


        protected void display_data()
        {
            try
            {
                string sql = "SELECT inquiry.id, inquiry.package_id, inquiry.name, inquiry.mobile_number, inquiry.email, package.name AS package_name, inquiry.gender, inquiry.no_of_days, inquiry.child, inquiry.adults, inquiry.message FROM inquiry INNER JOIN package ON inquiry.package_id = package.id";

                DataTable dt = Services.select(sql, con);
                if (dt.Rows.Count > 0)
                {
                    dispalytable.DataSource = dt;
                    dispalytable.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}
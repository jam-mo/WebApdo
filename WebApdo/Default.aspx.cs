using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace WebApdo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
                Response.Write("<br/>");
                string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
                SqlConnection con = new SqlConnection(s);
                string sqlString = "select * from customers";
                SqlCommand cmd = new SqlCommand(sqlString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                GridView1.DataSource = dr;
                GridView1.DataBind();
                dr.Close();
                string sqlStringDropDownList = "select Country from customers";
                SqlCommand cmd2 = new SqlCommand(sqlStringDropDownList, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read() == true)
                {
                    DropDownList1.Items.Add(new ListItem(dr2["Country"].ToString(), dr2["Country"].ToString()));
                }
                dr2.Close();
                con.Close();

            }
        }

     

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            Response.Write("<br/>");
            String txtvalue = TextBox1.Text;
            string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            SqlConnection con = new SqlConnection(s);
            string sqlString = "select * from customers where Country=@Country";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            // to prevent sql injection
            cmd.Parameters.AddWithValue("@Country", txtvalue);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            dr.Close();
            con.Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Write("<br/>");
                String txtValue = DropDownList1.SelectedValue.ToString();
                string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
                SqlConnection con = new SqlConnection(s);
                string sqlString = "select * from customers where Country=@Country";
                SqlCommand cmd = new SqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@Country", txtValue);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                GridView1.DataSource = dr;
                GridView1.DataBind();
                dr.Close();
                con.Close();
            }

        }
    }
}
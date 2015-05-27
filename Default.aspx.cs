using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=GANESHA-PC\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridView();
        }
    }

    private void FillGridView()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("spSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        GridView1.DataSource = cmd.ExecuteReader();
        GridView1.DataBind();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("spInsert", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@name", TextBox1.Text);
        cmd.Parameters.AddWithValue("@city", TextBox2.Text);
        cmd.Parameters.AddWithValue("@gender", RadioButtonList1.SelectedItem.Text);
        cmd.ExecuteNonQuery();
        con.Close();
        FillGridView();
    }
    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("spDelete", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
        cmd.ExecuteNonQuery();
        con.Close();
        FillGridView();
    }

    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        FillGridView();
    }

    protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("spUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        TextBox name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox3");
        TextBox city = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox4");
        TextBox gender = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox5");
        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
        cmd.Parameters.AddWithValue("@name", name.Text);
        cmd.Parameters.AddWithValue("@city", city.Text);
        cmd.Parameters.AddWithValue("@gender", gender.Text);
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        FillGridView();
    }

    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridView();
    }
}
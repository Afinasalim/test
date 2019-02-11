using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(@"data source=(local);Initial Catalog=db_remainder;Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
        {
            cn.Open();
            load();
            cn.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        cn.Open();
        SqlCommand cd = new SqlCommand("insert into tbl_remainder(reminder ) values('" + TextBox1.Text + "')", cn);
        cd.ExecuteNonQuery();
        load();
        cn.Close();
    }
    public void load()
    {
        SqlCommand cmd = new SqlCommand("select * from tbl_remainder",cn);
        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sdr.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        load();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex =-1;
        load();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        TextBox ed =GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
        Label lid = GridView1.Rows[e.RowIndex].FindControl("Label2") as Label;
        SqlCommand cad = new SqlCommand("delete from tbl_remainder where id='"+lid.Text+"'",cn);
        cn.Open();
        cad.ExecuteNonQuery();
        cn.Close();
        load();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox ed = GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
        Label lid = GridView1.Rows[e.RowIndex].FindControl("Label2") as Label;
        SqlCommand cad = new SqlCommand("update tbl_remainder set reminder='"+ed.Text+ "' where id='" + lid.Text + "'",cn);
        GridView1.EditIndex = -1;
        cn.Open();
        cad.ExecuteNonQuery();
        cn.Close();
        load();

    }
}
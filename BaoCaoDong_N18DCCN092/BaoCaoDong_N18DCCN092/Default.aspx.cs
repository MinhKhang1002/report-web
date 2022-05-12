using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace BaoCaoDong_N18DCCN092
{


    public partial class _Default : Page
    {
        public static List<String> listTable = new List<string>();
        public static List<String> listColunmTemp = new List<string>();
        public static List<String> listTableTemp = new List<string>();

  
        HashSet<string> tableName = new HashSet<string>();
        public static string Connect = "Data Source=MSI;Integrated Security=True";
        public static string database;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getDatabase();
               
            }
                

        }
        private void getDatabase()
        {
           
            using (SqlConnection conn = new SqlConnection())
            {   
                conn.ConnectionString = "Data Source=MSI;Initial Catalog='test';Integrated Security=True";

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb','ReportServer','ReportServerTempDB')";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                   DropDownList1.Items.Add("Chon database");
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = sdr["name"].ToString();
                            DropDownList1.Items.Add(item);
                            DropDownList1.AutoPostBack = true;
                        }
                    }
                    conn.Close();
                    
                }
            }
        }



        private void GetTableName(String database1)
        {
          
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=MSI;Initial Catalog='" + database1 + "';Integrated Security=True";

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG ='" + database1 + "' AND TABLE_NAME NOT LIKE 'sys%'";
                    //'" + database + "'
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = sdr["TABLE_NAME"].ToString();
                            CheckBoxTable.Items.Add(item);
                            CheckBoxTable.AutoPostBack = true;
                        }
                    }
                    conn.Close();
                }
            }
        }

        private void GetColumnName(String tableName)
        {


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=MSI;Initial Catalog='" + database + "';Integrated Security=True";
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["COLUMN_NAME"].ToString();
                            item.Value = tableName.ToString();
                            CheckBoxListColumn.Items.Add(item);
                            CheckBoxListColumn.AutoPostBack = true;
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void cbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            
             CheckBoxTable.Items.Clear();
            CauTruyVan.Text = "";

            database = DropDownList1.Items[DropDownList1.SelectedIndex].Value.ToString();
            Console.WriteLine(database);
            GetTableName(database);
        }

        protected void CheckBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxListColumn.Items.Clear();
            listTable.Clear();

            foreach (ListItem item in CheckBoxTable.Items)
            {
                if (item.Selected)
                {
                    listTable.Add(item.Value);

                }
            }
            for (int i = 0; i < listTable.Count; i++)
            {
                GetColumnName(listTable[i].ToString());
            }


        }

        protected void CheckBoxListColumn_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (ListItem item in CheckBoxListColumn.Items)
            {

                if (item.Selected)
                {
                    listTableTemp.Add(item.Text.ToString());
                    listColunmTemp.Add(item.Value.ToString());

                }
            }


            DataTable dt = new DataTable();

            dt.Columns.Add("TenCot", Type.GetType("System.String"));
            dt.Columns.Add("TenBang", Type.GetType("System.String"));

            string[] arr1 = listColunmTemp.ToArray();
            string[] arr2 = listTableTemp.ToArray();

            for (int i = 0; i < arr1.Length; i++)
            {
                dt.Rows.Add();
               
                dt.Rows[i]["TenBang"] = arr1[i];
                dt.Rows[i]["TenCot"] = arr2[i];

            }
            listColunmTemp.Clear();
            listTableTemp.Clear();

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



     

        protected void btnTaoTruyVan_Click(object sender, EventArgs e)
        {
            tableName.Clear();
         //   string mess1 = "";
            CauTruyVan.Text = "";
            string SELECT = "SELECT ";
            Boolean isSelect = false;
            Boolean isChoose = false;
            string FROM = " FROM ";
            //  int demfrom = 0;
            string WHERE = " WHERE ";
            Boolean isWhere = false;
            string GROUPBY = " GROUP BY ";
            Boolean isGroupby = false;
            string HAVING = " HAVING ";
            Boolean isHaving = false;
            
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                tableName.Add(GridView1.Rows[i].Cells[4].Text);
                DropDownList dr = new DropDownList();
                dr = (DropDownList)GridView1.Rows[i].Cells[1].FindControl("Cb_DieuKien");
                if (isSelect ==false)
                {

                    if (dr.SelectedValue.ToString() != "0" && dr.SelectedValue.ToString().Equals("GROUP BY") == false)
                    {
                        SELECT = SELECT + dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ")";

                        TextBox dk = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextInputDieuKienHaving");
                        if (dk.Text.Equals("")!=true && dk.Text != null)
                        {
                            if (isHaving == false)
                            {
                                HAVING = HAVING + dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ") " + dk.Text;
                                isHaving = true;
                            }
                            else
                            {
                                HAVING = HAVING + " and "+ dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ") " +dk.Text;
                                isHaving = true; ;
                            }
                        }
                    

                    }
                    else
                    {

                        SELECT = SELECT + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text;

                    }

                    isSelect = true;



                }



                else
                {
                    if (dr.SelectedValue.ToString() != "0" && dr.SelectedValue.ToString().Equals("GROUP BY") == false)
                    {

                        SELECT = SELECT + " , " + dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ")";
                        TextBox dk = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextInputDieuKienHaving");
                        if (dk.Text.Equals("") != true && dk.Text != null)
                        {
                            if (isHaving==false)
                            {
                                HAVING = HAVING + dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ") " + dk.Text;
                                isHaving = true;
                            }
                            else
                            {
                                HAVING = HAVING + " and " + dr.SelectedValue.ToString() + "(" + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + ") " + dk.Text;
                                isHaving = true;
                            }
                        }
                        
                    }
                    else
                    {

                        SELECT = SELECT + " , " + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text;


                    }

                }



            }

            //for 1

            // +       FROM
            for (int i = 0; i < tableName.Count; i++)
            {

                if (i != 0)
                {
                    FROM = FROM + " , " + tableName.ToArray()[i];
                }
                else
                {
                    FROM = FROM + tableName.ToArray()[i];
                }
            }


           
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList dr = new DropDownList();
                dr = (DropDownList)GridView1.Rows[i].Cells[1].FindControl("Cb_DieuKien");
                if (dr.SelectedValue.ToString() != "0")
                    isChoose = true;
            }


            if (isChoose)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    DropDownList dr = new DropDownList();
                    dr = (DropDownList)GridView1.Rows[i].Cells[1].FindControl("Cb_DieuKien");
                    if (dr.SelectedValue.ToString() == "0" || dr.SelectedValue.ToString().Equals("GROUP BY"))
                    {
                        if (isGroupby==true)
                        {
                            GROUPBY = GROUPBY + " , " + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text;
                            isGroupby = true;
                        }
                        else
                        {
                            GROUPBY = GROUPBY + "  " + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text;
                            isGroupby=true;
                        }
                    }
                }
            }
            WHERE = foreign_Key(WHERE, isWhere);
            if(WHERE.Length > 7)
            {
                isWhere = true;
            }
            
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox dk = (TextBox)GridView1.Rows[i].Cells[0].FindControl("TextInputDieuKien");
                if (dk.Text.ToString() != null && dk.Text.ToString() != "")
                {
                    if (isWhere==true)
                    {

                        WHERE = WHERE + " AND " + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + " " + dk.Text.ToString();
                    }
                    else
                    {
                        WHERE = WHERE + GridView1.Rows[i].Cells[4].Text + "." + GridView1.Rows[i].Cells[3].Text + " " + dk.Text.ToString();
                        isWhere=true;
                    }

                }
            }



            string QUERY = SELECT + FROM;
            if(WHERE.Length>7)
                QUERY = QUERY + WHERE;
            if (isGroupby == true)
                QUERY = QUERY + GROUPBY;
            if (isHaving == true)
                QUERY = QUERY + HAVING;
            CauTruyVan.Text = QUERY;
        }



        string foreign_Key(string where, Boolean isWhere)
        {
            using (SqlConnection cnn = new SqlConnection())
            {
                //qld.ConnectionString = "Data Source=MSI;Initial Catalog='" + database + "';Integrated Security=True";
               cnn.ConnectionString = "Data Source=MSI;Initial Catalog='" + database + "';Integrated Security=True";
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT  tab1.name AS[table]," +
                        " col1.name AS[column]," +
                        " tab2.name AS[referenced_table]," +
                        " col2.name AS[referenced_column]" +
                        " FROM sys.foreign_key_columns fkc" +
                        " INNER JOIN sys.tables tab1 " +
                        " ON tab1.object_id = fkc.parent_object_id " +
                        " INNER JOIN sys.columns col1 " +
                        " ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id" +
                        " INNER JOIN sys.tables tab2" +
                        " ON tab2.object_id = fkc.referenced_object_id" +
                        " INNER JOIN sys.columns col2" +
                        " ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id";
                    cmd.CommandText = query;
                    cmd.Connection = cnn;
                    cnn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            string table= sdr[0].ToString();
                            string colunm= sdr[1].ToString();
                            string ref_table = sdr[2].ToString();
                            string ref_colunm = sdr[3].ToString();
                            for (int i = 0; i < tableName.Count; i++)
                            {
                                for (int j = 0; j < tableName.Count; j++)
                                {

                                    string table_1 = tableName.ToArray()[i];
                                    string table_2 = tableName.ToArray()[j];
                                  

                                    if (table_1.Equals(table) && table_2.Equals(ref_table))
                                    {
                                        if (isWhere==false)
                                        {
                                            where = where + table + "." + colunm + " = " + ref_table + "." + ref_colunm;
                                            isWhere=true;
                                        }
                                        else
                                        {
                                            where = where + " AND " + table + "." + colunm + " = " + ref_table + "." + ref_colunm;
                                            isWhere = true;
                                        }
                                    }

                                }

                            }

                        }
                    }
                    cnn.Close();
                }
            }
            return where;
        }
        protected void ButtonReport_Click(object sender, EventArgs e)
        {
            String query = CauTruyVan.Text;
            Session["database"] = database;
            Session["query"] = query;
            Response.Redirect("WebForm1.aspx");
            Server.Execute("WebForm1.aspx");
        }
    }


}
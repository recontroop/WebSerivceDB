using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public String connstring = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=C:\\Users\\rusbe\\Desktop\\project.accdb;";
    public OleDbConnection conn=null;

    public Service()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Connect()
    {//activates a connection between the program and the current access file
        conn = new OleDbConnection(connstring);
        conn.Open();
    }

    [WebMethod]
    public int rowCount()
    {//returns the rowCount of the table
        OleDbConnection conn = new OleDbConnection(connstring);
        conn.Open();

        int rowCount = 0;
        String q = "Select COUNT(*) from Table1";
        OleDbCommand cmd = new OleDbCommand(q, conn);
        OleDbDataReader DB_Reader = cmd.ExecuteReader();

        DB_Reader.Read();
        rowCount = DB_Reader.GetInt32(0);
        DB_Reader.Close();

        conn.Close();
        return rowCount;
    }

    [WebMethod]
    public DataTable loopResult(int index, string query)
    {
        string loopstr = "";

        OleDbConnection conn = new OleDbConnection(connstring);
        conn.Open();
        OleDbCommand cmd = new OleDbCommand(query, conn);
        OleDbDataReader DB_Reader = cmd.ExecuteReader();
        OleDbDataAdapter myCmd = new OleDbDataAdapter(query, conn);
        DataSet dtSet = new DataSet();
        myCmd.Fill(dtSet, "Tbl");
        DataTable dTable = dtSet.Tables[0];

        //going through each row and adding matching data
        foreach (DataRow dtRow in dTable.Rows)
        {
            loopstr += (dtRow["Category_ID"] + "       " + dtRow["Project_Name"].ToString() + "-");
        }

        conn.Close();
        return dTable;
    }
}
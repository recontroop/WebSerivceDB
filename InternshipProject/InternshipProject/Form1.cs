using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InternshipProject
{
    public partial class Form1 : Form
    {
        public OleDbConnection conn = null;

        public Form1()
        {
            InitializeComponent();
        }

        //Web service object
        Operations.Service obj = new Operations.Service();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e) //All at runtime
        {
            //Obtaining Rowcount
            int rowCount = obj.rowCount();
            txtRows.Text = rowCount.ToString();
            //Creating array of objects, for storage of dynamic listboxes using rowCount of Categories 
            MessageBox.Show("You have # of Categories: " + rowCount.ToString());
            ListBox[] boxList = new ListBox[rowCount+1];
            //Cordinates for listbox Location
            int x = 15;
            int y = 53;
            string query = "";
            for (int i = 1; i <= rowCount; i++)
            {
                //Creates a new listbox entity on the form based on position of index
                boxList[i] = new ListBox();
                boxList[i].Location = new Point(x, y);
                //Changing dimensions of listbox(s)
                boxList[i].Width = 150;
                boxList[i].Height = 100;
                //Adds to form and moves the x Cordinate further right for placement                    
                this.Controls.Add(boxList[i]);
                x = x + 150;
                
                query = "Select * from Table2 WHERE Category_ID =" + (i);
                DataTable dCopy = obj.loopResult(i, query);
                foreach (DataRow dtRow in dCopy.Rows)
                {
                    boxList[i].Items.Add(dtRow["Category_ID"] + "       " + dtRow["Project_Name"].ToString());
                }
            }
        }
    }
}


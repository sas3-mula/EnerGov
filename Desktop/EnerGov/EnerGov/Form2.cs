using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnerGov
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                EmployeeDataClassDataContext db = new EmployeeDataClassDataContext();
                var emp = (from employee in db.GetTable<Employee>() select employee.Id).ToList();
                emp.Insert(0,"");
                manager.DataSource = emp;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void manager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(manager.SelectedIndex != 0)
            {
                empTable.Visible = true;
                try
                {
                    EmployeeDataClassDataContext db = new EmployeeDataClassDataContext();
                    empTable.DataSource = (from employee in db.GetTable<Employee>()
                                           where employee.ManagerId == manager.SelectedValue.ToString()
                                           select new { id = employee.Id, firstName = employee.firstName, lastName = employee.lastName });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 
        }

        private void search_Click(object sender, EventArgs e)
        {

            empManager add_new = new empManager();
            add_new.Show();
            this.Hide();
        }
    }
}

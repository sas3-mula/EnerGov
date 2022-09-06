using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EnerGov
{
    public partial class empManager : Form
    {
        public empManager()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            empFirstName.Text = "";
            empId.Text = "";
            empLastName.Text = "";
            roles.Text = "";
            manager.Text = "";

            ;
            this.Close();
            Form2 f2 = new Form2();
            f2.Show();




        }

        private void save_Click(object sender, EventArgs e)
        {
            if (empFirstName.Text == "" || empId.Text == "" || empLastName.Text == "" || manager.Text == "" || roles.Text == "")
            {
                MessageBox.Show("Please enter all the details");
            }
            else
            {
                Employee employee = new Employee();
                employee.Id = empId.Text;
                employee.firstName = empFirstName.Text;
                employee.lastName = empLastName.Text;
                employee.ManagerId = manager.Text;
                employee.Roles = roles.Text;

                try 
                {
                    EmployeeDataClassDataContext db = new EmployeeDataClassDataContext();
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    MessageBox.Show("Employee Added Successfully!..");
                    empFirstName.Text = "";
                    empId.Text = "";
                    empLastName.Text = "";
                    roles.Text = "";
                    manager.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error adding to the database. The employee Id is already taken." +
                        "please select a diffent employee id");
                }
                
            }
        }

        private void manager_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void empManager_Load(object sender, EventArgs e)
        {
            
            try
            {
                EmployeeDataClassDataContext db = new EmployeeDataClassDataContext();
                var emp = (from employee in db.GetTable<Employee>() select employee.Id).ToList();
                emp.Insert(0, "");
                manager.DataSource = emp;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

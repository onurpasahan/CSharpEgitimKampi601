using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=541900";

        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource =dataTable;
            connection.Close();
        }
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DataSource = dataTable;
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "departmentid";
            connection.Close();
        }
        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Insert Into employees (employeename,employeesurname,departmentid,employeesalary) Values (@p1,@p2,@p3,@p4)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", txtEmployeeName.Text);
            command.Parameters.AddWithValue("@p2", txtEmployeeSurname.Text);
            command.Parameters.AddWithValue("@p3", cmbEmployeeDepartment.SelectedValue);
            command.Parameters.AddWithValue("@p4", decimal.Parse(txtEmployeeSalary.Text));
            command.ExecuteNonQuery();
            connection.Close();
            EmployeeList();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            int id = int.Parse(txtEmployeeId.Text);
            string query = "Delete From employees Where employeeid=@p1";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            connection.Close();
            EmployeeList();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            int id = int.Parse(txtEmployeeId.Text);
            string query = "Update employees Set employeename=@p1,employeesurname=@p2,departmentid=@p3,employeesalary=@p4 Where employeeid=@p5";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", txtEmployeeName.Text);
            command.Parameters.AddWithValue("@p2", txtEmployeeSurname.Text);
            command.Parameters.AddWithValue("@p3", cmbEmployeeDepartment.SelectedValue);
            command.Parameters.AddWithValue("@p4", decimal.Parse(txtEmployeeSalary.Text));
            command.Parameters.AddWithValue("@p5", id);
            command.ExecuteNonQuery();
            connection.Close();
            EmployeeList();
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            int id = int.Parse(txtEmployeeId.Text);
            string query = "Select * From employees Where employeeid=@p1";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", id);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}

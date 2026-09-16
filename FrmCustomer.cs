using System;
using System.CodeDom.Compiler;
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
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=541900";
        void GetAllCustomers()
        {
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close(); 
        }
        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Customers (CustomerName, CustomerSurname, CustomerCity) VALUES (@name, @surname, @city)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", customerName);
            command.Parameters.AddWithValue("@surname", customerSurname);
            command.Parameters.AddWithValue("@city", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri başarılı bir şekilde eklendi");
            GetAllCustomers();
            connection.Close();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString); 
            connection.Open();
            string query = "DELETE FROM Customers WHERE CustomerId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri başarılı bir şekilde silindi");
            GetAllCustomers();
            connection.Close();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            int id = int.Parse(txtCustomerId.Text);
            string query = "UPDATE Customers SET CustomerName = @name, CustomerSurname = @surname, CustomerCity = @city WHERE CustomerId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", customerName);
            command.Parameters.AddWithValue("@surname", customerSurname);
            command.Parameters.AddWithValue("@city", customerCity);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri başarılı bir şekilde güncellendi");
            GetAllCustomers();
            connection.Close();
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            int id = int.Parse(txtCustomerId.Text);
            string query = "SELECT * FROM Customers WHERE CustomerId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            var adapter = new NpgsqlDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}

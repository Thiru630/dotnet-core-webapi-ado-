using System;
using System.Collections.Generic;
using EmployeeService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeService.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        IEnumerable<Employee> IEmployeeRepository.GetEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, Age, Email FROM emp", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return employees;
        }
        public int CreateEmployee(Employee e)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO emp (Name, Age, Email) VALUES (@Name, @Age, @Email)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", e.Name);
                    cmd.Parameters.AddWithValue("@Age", e.Age);
                    cmd.Parameters.AddWithValue("@Email", e.Email);

                    var x = cmd.ExecuteNonQuery();
                    return x;
                }
            }
        }
        public Employee searchEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from emp where Id=@Id",conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())  
                    {
                        return new Employee()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Email = reader["Email"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
           
        }
        public int DeleteEmployee(int id)
        {
            using(SqlConnection conn= new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("delete from emp where Id=@Id",conn))
                {
                    cmd.Parameters.AddWithValue("@Id",id);
                    var x=cmd.ExecuteNonQuery();
                    return x;
                }
            }
        }
        public int UpdateEmployee(Employee e)
        {
             using(SqlConnection conn= new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("update emp set Name=@name,Age=@Age,Email=@Email where Id=@Id",conn))
                {
                    cmd.Parameters.AddWithValue("@Id",e.Id);
                    cmd.Parameters.AddWithValue("@Name",e.Name);
                    cmd.Parameters.AddWithValue("@Age",e.Age);
                    cmd.Parameters.AddWithValue("@Email",e.Email);
                    var x=cmd.ExecuteNonQuery();
                    return x;
                }
            }
        }
    }
}
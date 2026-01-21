using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Models;

namespace EmployeeService.Repository
{
  public interface IEmployeeRepository
  {
    IEnumerable<Employee> GetEmployees();
    int CreateEmployee(Employee e);
    int UpdateEmployee(Employee e);
    int DeleteEmployee(int id);
     Employee searchEmployee(int id);
  }
}
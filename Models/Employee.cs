using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models
{
  public class Employee
  {
    [Key]
    public int Id {get;set;}
    [Required]
    public string Name {get;set;}
    public int Age {get;set;}
   [Required]
    public string Email {get;set;}
  }
}
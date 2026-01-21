using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Repository;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
 [ApiController]
  [Route("api/[controller]/[action]")]
  public class EmployeeController : ControllerBase
  {
    public readonly IEmployeeRepository _repo;
     public EmployeeController(IEmployeeRepository repo)
     {
        _repo =  repo;
     }
     [HttpGet]
     public IActionResult GetAll()
     {
        var emp=_repo.GetEmployees();
        return Ok(emp);
     }
     [HttpPost]
     public IActionResult Create(Employee e)
     {
       var res = _repo.CreateEmployee(e);
         if(res>0)
         return Ok(e);
         else
         {
            return BadRequest();
         }
     }
     [HttpGet]
     public IActionResult Search(int id)
     {
         var res = _repo.searchEmployee(id);
         return Ok(res);
     }
     [HttpDelete]
     public IActionResult Delete(int id)
     {
         var res = _repo.DeleteEmployee(id);
         if(res>0)
         return Ok("deleted successfully");
         else
         return BadRequest();
     }
      [HttpPut]
      public IActionResult Update(Employee e)
      {
         var res = _repo.UpdateEmployee(e);
         if(res>0)
         return Ok("updated successfully");
         else
         return BadRequest();
      }
  }
}
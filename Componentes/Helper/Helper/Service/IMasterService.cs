using Helper.Classes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Service
{
    public interface IMasterService
    {
        [Get("/api/master/employee/")]
        Task<Employee> EmployeeGetList();

        [Get("/api/master/employee/{id}")]
        Task<Employee> EmployeeGet(string id);

        [Post("/api/master/employee/")]
        Task<Employee> EmployeeCreate([Body] Employee employee, [Header("Authorization")] string token);
    }
}

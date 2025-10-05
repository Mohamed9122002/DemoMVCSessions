using Demo.DataAccess.Repositories.DepartmentRepo;
using Demo.DataAccess.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository  employeeRepository { get;} 
        public IDepartmentRepository departmentRepository { get; }
        int SaveChange();
       
    }
}

using Demo.DataAccess.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {

    }
}

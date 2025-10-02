using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.DepartmentRepo
{
    // Primary Constructor 
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext) , IDepartmentRepository
    {

    }
}

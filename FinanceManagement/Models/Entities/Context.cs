using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagement.Models.Entities
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }
    }
}

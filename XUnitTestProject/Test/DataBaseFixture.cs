using DLA.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject.Test
{
    public class DataBaseFixture : IDisposable
    {
       private ApplicationDbContext _context;
        public DataBaseFixture()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>();
                
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

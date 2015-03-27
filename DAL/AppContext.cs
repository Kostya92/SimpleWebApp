using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class AppContext : DbContext
    {
        public AppContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            
        }

        public DbSet<Profile> Profiles { get; set; }
    }
}

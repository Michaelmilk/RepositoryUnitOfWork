using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace unitWork.Models
{
    public class StoreContext: DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Book> Books { get; set; }

        public StoreContext(): base("Store")
        {

        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("data source= name;initial catalog=API;Integrated Security=True;") {}

        public DbSet<APIDataModel> APIData { get; set; }
    }
}

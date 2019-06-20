using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERP_testTask.Models
{
    public class BookContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
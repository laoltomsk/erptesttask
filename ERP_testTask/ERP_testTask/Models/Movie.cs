using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_testTask.Models
{
    public class Movie
    {
        public ApplicationUser User { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public string PosterURL { get; set; }
    }
}
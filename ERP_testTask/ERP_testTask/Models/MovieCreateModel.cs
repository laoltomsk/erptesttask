using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_testTask.Models
{
    public class MovieCreateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public HttpPostedFileBase Poster { get; set; }
    }
}
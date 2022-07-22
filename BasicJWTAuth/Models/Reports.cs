using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Models
{
    public class Reports
    {
        public List<Report> Report { get; set; }
    }

    public class Report
    {
        public int reportid { get; set; }

        public string reportname { get; set; }

        public string language { get; set; }
    }
}

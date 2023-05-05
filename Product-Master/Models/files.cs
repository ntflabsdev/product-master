using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_Master.Models
{
    public class files
    {
        public int Id { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Grade { get; set; }
        public string Unit_Of_Measurment { get; set; }
        public int? HS_Code { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public string TDS { get; set; }
        public string MSDS { get; set; }
        public string  Free_Trade_Agreement { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImagePath { get; set; }

        public HttpPostedFileBase TDS_File { get; set; }
        public HttpPostedFileBase MSDS_File { get; set; }
        public HttpPostedFileBase FTA_File { get; set; }
    }
}
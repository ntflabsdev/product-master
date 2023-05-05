using Product_Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace Product_Master.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

            [HttpPost]
            public ActionResult Index(files file)
            {
                string FileName = System.IO.Path.GetFileNameWithoutExtension(file.ImageFile.FileName);
                string FileExtension = System.IO.Path.GetExtension(file.ImageFile.FileName);
                FileName = DateTime.Now.ToString("yyyyMMDD") + "-" + FileName.Trim() + FileExtension;
                string UploadPath = Server.MapPath("~/Uploading Files/");
                file.ImagePath = UploadPath + FileName;
                file.ImageFile.SaveAs(file.ImagePath);
                byte[] bytes = System.IO.File.ReadAllBytes(file.ImagePath);

                ProductMasterEntities dbentities = new ProductMasterEntities();

                string Product_Code = file.Product_Name.Substring(0, 2);
                var PreCode = dbentities.Product.OrderByDescending( x=> x.Id).Select(x=>x.Product_Code).FirstOrDefault();


                string ProductCode = "";
                if (PreCode != null && PreCode != "")
                {
                ProductCode = Convert.ToInt32(Convert.ToInt32(PreCode.Substring(PreCode.Length - 3)) + 1).ToString();
                int length = ProductCode.Length;
                switch (length)
                {
                  case 1:
                        ProductCode = Product_Code + "00" + ProductCode;
                  break;

                  case 2:
                        ProductCode = Product_Code + "0" + ProductCode;
                  break;

                  case 3:
                        ProductCode = Product_Code + ProductCode;
                   break;
                   default:
                   break;
                      }
                }
                 else
                    ProductCode = Product_Code + "001";

            //Save Files to subfolders for Product
            //Save TDS File
            string TDS_FileName = System.IO.Path.GetFileNameWithoutExtension(file.TDS_File.FileName);
            string TDS_FileExtension = System.IO.Path.GetExtension(file.TDS_File.FileName);
            TDS_FileName = UploadPath + ProductCode + "/TDS_" + TDS_FileName.Trim() + TDS_FileExtension;
            string TDS_FilePath = "/Uploading Files/" + ProductCode + "/TDS_" + file.TDS_File.FileName ;
            if (!System.IO.Directory.Exists(UploadPath+ ProductCode))
            {
                System.IO.Directory.CreateDirectory(UploadPath+ ProductCode);
            }
            file.TDS_File.SaveAs(TDS_FileName);

            //Savew MSDS File
            string MSDS_FileName = System.IO.Path.GetFileNameWithoutExtension(file.MSDS_File.FileName);
            string MSDS_FileExtension = System.IO.Path.GetExtension(file.TDS_File.FileName);
            MSDS_FileName = UploadPath + ProductCode + "/MSDS_" + MSDS_FileName.Trim() + MSDS_FileExtension;
            string MSDS_FilePath = "/Uploading Files/" + ProductCode + "/MSDS_" + file.MSDS_File.FileName ;
            if (!System.IO.Directory.Exists(UploadPath + ProductCode))
            {
                System.IO.Directory.CreateDirectory(UploadPath + ProductCode);
            }
            file.MSDS_File.SaveAs(MSDS_FileName);

            //SAVE FTA File
            string FTA_FileName = System.IO.Path.GetFileNameWithoutExtension(file.FTA_File.FileName);
            string FTA_FileExtension = System.IO.Path.GetExtension(file.FTA_File.FileName);
            FTA_FileName = UploadPath + ProductCode + "/FTA_" + FTA_FileName.Trim() + FTA_FileExtension;
            string FTA_FilePath = "/Uploading Files/" + ProductCode + "/FTA_" + file.FTA_File.FileName ;
            if (!System.IO.Directory.Exists(UploadPath + ProductCode))
            {
                System.IO.Directory.CreateDirectory(UploadPath + ProductCode);
            }
            file.FTA_File.SaveAs(FTA_FileName);



                Product pdt = new Product();
                pdt.Product_Code = ProductCode;    
                pdt.Product_Name = file.Product_Name;
                pdt.Grade = file.Grade;
                pdt.Unit_Of_Measurement = file.Unit_Of_Measurment;
                pdt.HS_Code = file.HS_Code;
                pdt.Manufacturer = file.Manufacturer;                
                pdt.Country = file.Country;
                pdt.TDS = TDS_FilePath;
                pdt.MSDS = MSDS_FilePath;
                pdt.Free_Trade_Agreement = FTA_FilePath;
                pdt.Image = bytes;

            dbentities.Product.Add(pdt);
                dbentities.SaveChanges();

                System.IO.File.Delete(file.ImagePath);
                return View("Index");
            }
        }
    }




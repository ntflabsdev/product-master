using Product_Master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Product_Master.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductMasterEntities dbentities = new ProductMasterEntities();
            List<Product> pdtlist = dbentities.Product.Distinct().ToList();
            return View(pdtlist);
          
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductMasterEntities dbentities = new ProductMasterEntities();
            Product pdt = dbentities.Product.SingleOrDefault(x => x.Id == id);
            Product_passvalue ppv=   new Product_passvalue
            {
                Country = pdt.Country,
                Id = pdt.Id,
                HS_Code = pdt.HS_Code,
                Grade = pdt.Grade,
                Product_Name = pdt.Product_Name,
                Product_Code = pdt.Product_Code,
                Manufacturer = pdt.Manufacturer,
                Unit_Of_Measurement = pdt.Unit_Of_Measurement,
                Image=pdt.Image,
                Free_Trade_Agreement= pdt.Free_Trade_Agreement,
                MSDS= pdt.MSDS,
                TDS= pdt.TDS
            };

            return View(ppv);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product_passvalue collection_1)
        {
            Product collection = new Product
            {
                Country = collection_1.Country,
                Id = collection_1.Id,
                HS_Code = collection_1.HS_Code,
                Grade = collection_1.Grade,
                Product_Name = collection_1.Product_Name,
                Product_Code = collection_1.Product_Code,
                Manufacturer = collection_1.Manufacturer,
                Unit_Of_Measurement = collection_1.Unit_Of_Measurement


            };
            
            try
            {
                string UploadPath = Server.MapPath("~/Uploading Files/");
                ProductMasterEntities dbentities = new ProductMasterEntities();
                Product updateobj = new Product();
                updateobj = dbentities.Product.SingleOrDefault(x => x.Id == collection.Id);

                //Update for images
                if (collection_1.ImageFile!=null)
                {
                    string FileName = System.IO.Path.GetFileNameWithoutExtension(collection_1.ImageFile.FileName);
                    string FileExtension = System.IO.Path.GetExtension(collection_1.ImageFile.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMDD") + "-" + FileName.Trim() + FileExtension;
                    string ImagePath = UploadPath + FileName;
                    collection_1.ImageFile.SaveAs(ImagePath);
                    byte[] bytes = System.IO.File.ReadAllBytes(ImagePath);
                    collection.Image = bytes;
                }
                else
                {
                    if (updateobj.Image!=null)
                    {
                        collection.Image = updateobj.Image;
                    }
                }

                // update TDS File
                if (collection_1.TDS_File != null)
                {
                    string TDS_FileName = System.IO.Path.GetFileNameWithoutExtension(collection_1.TDS_File.FileName);
                    string TDS_FileExtension = System.IO.Path.GetExtension(collection_1.TDS_File.FileName);
                    TDS_FileName = UploadPath + collection_1.Product_Code + "/TDS_" + TDS_FileName.Trim() + TDS_FileExtension;
                    string TDS_FilePath = "/Uploading Files/" + collection.Product_Code + "/TDS_" + collection_1.TDS_File.FileName;
                    if (updateobj.TDS != TDS_FilePath)
                    {
                        collection.TDS = TDS_FilePath;
                        if (!System.IO.Directory.Exists(UploadPath + collection.Product_Code))
                        {
                            System.IO.Directory.CreateDirectory(UploadPath + collection.Product_Code);
                        }
                        collection_1.TDS_File.SaveAs(TDS_FileName);
                    }
                }
                else
                {
                    collection_1.TDS = updateobj.TDS;
                }



                //Update MSDS File
                if (collection_1.MSDS_File != null)
                {
                    string MSDS_FileName = System.IO.Path.GetFileNameWithoutExtension(collection_1.MSDS_File.FileName);
                    string MSDS_FileExtension = System.IO.Path.GetExtension(collection_1.MSDS_File.FileName);
                    MSDS_FileName = UploadPath + collection.Product_Code + "/MSDS_" + MSDS_FileName.Trim() + MSDS_FileExtension;
                    string MSDS_FilePath = "/Uploading Files/" + collection.Product_Code + "/MSDS_" + collection_1.MSDS_File.FileName;
                    if (updateobj.MSDS != MSDS_FilePath)
                    {
                        collection.MSDS = MSDS_FilePath;
                        if (!System.IO.Directory.Exists(UploadPath + collection.Product_Code))
                        {
                            System.IO.Directory.CreateDirectory(UploadPath + collection.Product_Code);
                        }
                        collection_1.MSDS_File.SaveAs(MSDS_FileName);
                    }
                }
                else
                {
                    collection_1.MSDS = updateobj.MSDS;
                }



                //Update FTA File
                if (collection_1.FTA_File != null)
                {
                    string FTA_FileName = System.IO.Path.GetFileNameWithoutExtension(collection_1.FTA_File.FileName);
                    string FTA_FileExtension = System.IO.Path.GetExtension(collection_1.FTA_File.FileName);
                    FTA_FileName = UploadPath + collection.Product_Code + "/FAT_" + FTA_FileName.Trim() + FTA_FileExtension;
                    string FTA_FilePath = "/Uploading Files/" + collection.Product_Code + "/FTA_" + collection_1.FTA_File.FileName;
                    if (updateobj.Free_Trade_Agreement != FTA_FilePath)
                    {
                        collection.Free_Trade_Agreement = FTA_FilePath;
                        if (!System.IO.Directory.Exists(UploadPath + collection.Product_Code))
                        {
                            System.IO.Directory.CreateDirectory(UploadPath + collection.Product_Code);
                        }
                        collection_1.FTA_File.SaveAs(FTA_FileName);
                    }
                }
                else
                {
                    collection.Free_Trade_Agreement = updateobj.Free_Trade_Agreement;
                }
                

                if (collection !=null)
                {
                    dbentities.Entry(updateobj).CurrentValues.SetValues(collection);
                    dbentities.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductMasterEntities dbentities = new ProductMasterEntities();
            Product pdt = dbentities.Product.SingleOrDefault(x => x.Id == id);
            if(pdt !=null)
            {
                dbentities.Product.Remove(pdt);
                dbentities.SaveChanges();
            }
            return View("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

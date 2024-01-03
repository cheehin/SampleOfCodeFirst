using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Nation.Models;
using System.Data;

namespace Nation.Controllers
{
    public class DbQuo : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public DbQuo(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddNew(DbQuotation ds)
        {
            QueryContext qC = new QueryContext();
            qC.dbQuotations.Add(ds);
            qC.SaveChanges();
            return new EmptyResult();
        }

        public ActionResult AddDefaultData()
        {
            QueryContext queryContext = new QueryContext();
            var defData = new DbQuotation[]
            {
                new DbQuotation{SupplierId="Supplier1 id",Product="Skipping Rope",CostPerUnit=1.05 },
                new DbQuotation{SupplierId="Supplier1 id",Product="Spatula",CostPerUnit= 1.505},
                new DbQuotation{SupplierId="Supplier1 id",Product="Pacifier",CostPerUnit=1 },
                new DbQuotation{SupplierId="Supplier2 id",Product="Diapers",CostPerUnit= 2},
                new DbQuotation{SupplierId="Supplier2 id",Product="Bedsheet",CostPerUnit=null},
                new DbQuotation{SupplierId="Supplier2 id",Product="Blanket",CostPerUnit=3.2},
                new DbQuotation{SupplierId="Supplier3 id",Product="Blanker",CostPerUnit=3}


            };
            foreach (var def in defData)
            {
                queryContext.dbQuotations.Add(def);
            }
            queryContext.SaveChanges();
            return new EmptyResult();
        }
        public ActionResult UpdateData(DbQuotation ds)
        {
            QueryContext qs = new QueryContext();
            var data = qs.dbQuotations.FirstOrDefault(g => g.SupplierId == ds.SupplierId);
            if (data != null)
            {
                data.Product = ds.Product;
                data.CostPerUnit = ds.CostPerUnit;
                qs.SaveChanges();
            }
            return new EmptyResult();
        }

        public ActionResult DeleteData(DbQuotation ds)
        {
            QueryContext qs = new QueryContext();
            var data = qs.dbQuotations.FirstOrDefault(g => g.SupplierId == ds.SupplierId && g.Product== ds.Product);
            if (data != null)
            {
                qs.dbQuotations.Remove(data);
                qs.SaveChanges();
            }
            return new EmptyResult();
        }

        public ActionResult GetDbQuotation()
        {
            Request.Form.TryGetValue("draw", out var draw);
            QueryContext queryContext = new QueryContext();
            var data = queryContext.dbQuotations.ToList().Where(g=>g.CostPerUnit!=null).OrderBy(g=>g.SupplierId).ThenBy(g=>g.Product);
            return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = data });
        }

        public FileContentResult DownloadFile()
        {
            QueryContext queryContext = new QueryContext();
            string rowData = string.Empty;
            rowData = "SupplierID,Product,CostPerUnit\n";

            foreach (DbQuotation data in queryContext.dbQuotations)
            {
                rowData += data.SupplierId.ToString() + ",";
                rowData += data.Product.ToString() + ",";
                rowData += data.CostPerUnit.ToString() + ",";
                rowData += "\n";
            } 
            return File(new System.Text.UTF8Encoding().GetBytes(rowData), "text/csv", "Data.csv");
        }
    }
}

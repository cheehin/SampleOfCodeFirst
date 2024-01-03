using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Nation.Controllers
{
    public class DbSup : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddNew(DbSupplier ds)
        {
            QueryContext qC = new QueryContext();
            qC.Suppliers.Add(ds);
            qC.SaveChanges();
            return new EmptyResult();
        }

        public ActionResult AddDefaultData()
        {
            QueryContext queryContext = new QueryContext();
            var defData = new DbSupplier[]
            {
                new DbSupplier{Id=1,Name="Supplier1",Email="supplier1@gmail.com",CountryCode="GB",DateCreated=Convert.ToDateTime("18-Aug-2021") },
                new DbSupplier{Id=2,Name="Supplier2",Email="supplier2@gmail.com",CountryCode="JP",DateCreated=Convert.ToDateTime("18-Aug-2021") },
                new DbSupplier{Id=3,Name="Supplier3",Email="supplier3@gmail.com",CountryCode="GB",DateCreated=Convert.ToDateTime("19-Aug-2021") },
                new DbSupplier{Id=4,Name="Supplier4",Email="supplier4@gmail.com",CountryCode="GB",DateCreated=Convert.ToDateTime("19-Aug-2021") },
                new DbSupplier{Id=5,Name="Supplier5",Email="supplier5@gmail.com",CountryCode="JP",DateCreated=Convert.ToDateTime("19-Aug-2021")}
            };
            foreach (var def in defData)
            {
                queryContext.Suppliers.Add(def);
            }
            queryContext.SaveChanges();
            return new EmptyResult();
        }

        public ActionResult DeleteData(DbSupplier ds)
        {
            QueryContext qs = new QueryContext();
            var data = qs.Suppliers.FirstOrDefault(g => g.Id == ds.Id);
            if (data != null)
            {
                qs.Suppliers.Remove(data);
                qs.SaveChanges();
            } 
            return new EmptyResult();
        }
        public ActionResult UpdateData(DbSupplier ds)
        {
            QueryContext qs = new QueryContext();
            var data= qs.Suppliers.FirstOrDefault(g=>g.Id == ds.Id);
            if(data != null)
            {
                data.Email= ds.Email;
                data.DateCreated = Convert.ToDateTime(ds.DateCreated);
                data.CountryCode= ds.CountryCode;
                data.Name= ds.Name;
                qs.SaveChanges();
            }
            return new EmptyResult();
        }

        public ActionResult GetDbSupplier()
        {
            Request.Form.TryGetValue("draw", out var draw);
            QueryContext queryContext = new QueryContext();
            var data = queryContext.Suppliers.ToList().OrderBy(g=>g.CountryCode).ThenByDescending(g=>g.DateCreated).ThenByDescending(g=>g.Name);
            return Json(new {draw= draw,recordsFiltered = 0,recordsTotal=0,data= data });
        }

        public FileContentResult DownloadFile()
        {
            QueryContext queryContext = new QueryContext();
            string rowData = string.Empty;
            rowData = "Id,Name,Email,CountryCode,DateCreated\n";

            foreach (DbSupplier data in queryContext.Suppliers)
            {
                rowData += data.Id.ToString() + ",";
                rowData += data.Name.ToString() + ",";
                rowData += data.Email.ToString() + ",";
                rowData += data.CountryCode.ToString() + ",";
                rowData += data.DateCreated.ToString() + ",";
                rowData += "\n";
            }
            return File(new System.Text.UTF8Encoding().GetBytes(rowData), "text/csv", "Data.csv");
        }
    }
}

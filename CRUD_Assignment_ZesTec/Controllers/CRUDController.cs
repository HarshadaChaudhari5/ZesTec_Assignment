using CRUD_Assignment_ZesTec.BAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRUD_Assignment_ZesTec.Controllers
{
    public class CRUDController : Controller
    {
		CustomerBAL ObjCust = new CustomerBAL();
        public ActionResult Index()
        {
			ViewBag.GetCustomerData = ObjCust.GetCustomerData();
			return View();
        }

		
		public ActionResult SaveAddCustomer()
		{
			NameValueCollection Form = Request.Form;
			ObjCust.SaveAddCustomer(Form);
			TempData["Message"] = "Customer Record Added Successfully...!!";
			return RedirectToAction("Index", "CRUD");
		}
		
		public ActionResult EditCustomer(int ID)
		{
			var jsondata = "";
			Dictionary<string, object> Param = new Dictionary<string, object>();

			Param.Add("CustomerData", ObjCust.GetEditCustomerData(ID).Tables[0]);
			jsondata = JsonConvert.SerializeObject(Param);
			return Json(jsondata);
		}
		
		public ActionResult SaveEditCustomer()
		{
			NameValueCollection Form = Request.Form;
			ObjCust.SaveEditCustomer(Form);
			TempData["Message"] = "Customer Record Updated Successfully...!!";
			return RedirectToAction("Index", "CRUD");
		}

		
		public ActionResult DeleteCustomerRecord(string ID)
		{
			ObjCust.DeleteCustomerRecord(ID);
			TempData["Message"] = "Customer Record Delete Successfully...!!";
			return RedirectToAction("Index", "CRUD");
		}

		
		[HttpPost]
		public JsonResult CheckDuplicateRecord(string ColName, string Value)
		{
			string result = "0";
			DataSet DS = ObjCust.CheckDuplicateRecord(ColName, Value);

			if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
			{
				result = DS.Tables[0].Rows[0]["RESULT"].ToString();
			}
			return Json(result);
		}
	}
}
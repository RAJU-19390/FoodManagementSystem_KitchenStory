using FoodLibrary;
using FoodManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodManagementSystem.Controllers
{
    public class FoodItemController : Controller
    {
        FoodDAL dal = new FoodDAL();
        public ActionResult Index()
        {
            List<FoodMaster> itemlist = dal.GetFoodItemList();
            List<FoodModel> foodmodels = new List<FoodModel>();
            foreach (FoodMaster item in itemlist)
            {
                foodmodels.Add(new FoodModel { FId = item.FId, FName = item.FName, FPrice = item.FPrice });

            }
            return View(foodmodels);
        }
        public ActionResult Details(int id)
        {
            int food_id = id;
            FoodMaster item = new FoodMaster();
            item = dal.FindFoodItem(food_id);
            FoodModel model = new FoodModel();
            model.FId = item.FId;
            model.FName = item.FName;
            model.FPrice = item.FPrice;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FoodModel model)
        {
            try
            {
                    FoodMaster item = new FoodMaster
                    {
                        FName = model.FName,
                        FPrice = model.FPrice,
                    };
                    dal.AddFoodItem(item);
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            int food_id = id;
            FoodMaster item = new FoodMaster();
            item = dal.FindFoodItem(food_id);
            FoodModel model = new FoodModel();
            model.FId = item.FId;
            model.FName = item.FName;
            model.FPrice = item.FPrice;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            bool status = false;
            try
            {
                FoodMaster item = new FoodMaster();
                item.FId = id;
                item.FName = collection["FName"];
                item.FPrice = Convert.ToSingle(collection["FPrice"]);
                status = dal.EditFoodItem(item, id);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
            if (status)
                return RedirectToAction("Index");
            else
                return View();
        }

        public ActionResult Delete(int id)
        {
            int food_id = id;
            FoodMaster item = new FoodMaster();
            item = dal.FindFoodItem(food_id);
            FoodModel model = new FoodModel();
            model.FId = item.FId;
            model.FName = item.FName;
            model.FPrice = item.FPrice;
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool status = false;
            try
            {
                int food_id = id;
                status = dal.RemoveFoodItem(food_id);
                if (status)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        public ActionResult FoodMenu(string item)
        {
            List<FoodMaster> itemlist = dal.GetFoodItemList();

            if (itemlist == null)
            {
                return View(new List<FoodModel>());
            }

            var searchResults = itemlist
                .Where(i => i.FName != null && (item == null || i.FName.ToLower().Contains(item.ToLower())))
                .Select(i => new FoodModel { FId = i.FId, FName = i.FName, FPrice = i.FPrice })
                .ToList();

            ViewBag.SearchTerm = item;

            return View(searchResults);
        }


        public ActionResult SelectedItem(int Fid)
        {
            int food_id = Fid;
            FoodMaster item = new FoodMaster();
            item = dal.FindFoodItem(food_id);
            FoodModel model = new FoodModel();
            model.FId = item.FId;
            model.FName = item.FName;
            model.FPrice = item.FPrice;
            TempData["FPrice"] = model.FPrice;
            TempData["FName"] = model.FName;
            TempData.Keep();
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectedItem(int itemqty, string address)
        {
            if (TempData["FName"] != null && TempData["FPrice"] != null)
            {
                string fooditem = TempData["FName"].ToString();
                float price = Convert.ToSingle(TempData["FPrice"]);
                float Total_Amt = price * itemqty;
                TempData["Total_amt"] = Total_Amt;
                TempData["Address"] = address;
                TempData.Keep();
                return RedirectToAction("PaymentMode");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult PaymentMode()
        {
            ViewBag.TotalAmt = Convert.ToSingle(TempData["Total_amt"]);
            ViewBag.Address = TempData["Address"].ToString();
            return View();
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}

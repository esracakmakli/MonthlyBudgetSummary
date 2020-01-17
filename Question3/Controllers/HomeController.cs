using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question3.Models;

namespace Question3.Controllers
{
    public class HomeController : Controller
    {
        budgetsummaryEntities db = new budgetsummaryEntities();
        public ActionResult Index()
        {

            //linq to sql for total expense amount
            var totalExpense =db.Expense.Sum(s=>s.Amount);
            ViewData["totalexpense"] = totalExpense;

            var totalIncome = db.Income.Sum(s => s.Amount);
            ViewData["totalincome"] = totalIncome;

            var totalbudget = totalIncome - totalExpense;
            ViewData["totalbudget"] = totalbudget;

            return View(db.Categorys.ToList());
        }

       
        public ActionResult AddIncome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddIncome(Income income)
        {
            db.Income.Add(income);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddExpense()
        {
            //for Select Expense Category  
            List<SelectListItem> categorys = (from i in db.Categorys.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.Name,
                                                  Value = i.id.ToString()
                                              }).ToList();


            ViewBag.categorys = categorys;
            return View();
        }
        [HttpPost]
        public ActionResult AddExpense(Expense expense)
        {
            expense.Date = DateTime.Now;
            db.Expense.Add(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {

            return View(db.Expense.Where(x => x.CategoryID == id).ToList());
        }
        public ActionResult Delete(int? id)
        {
            //there is no problem in the code but I couldn't solve the problem in the view 
            if (id == null)
            {
                return HttpNotFound();
            }
            Expense expense = db.Expense.Find(id);
            db.Expense.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            List<SelectListItem> categorys = (from i in db.Categorys.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.Name,
                                                  Value = i.id.ToString()
                                              }).ToList();


            ViewBag.categorys = categorys;
            return View(db.Expense.FirstOrDefault(x => x.id == id));
        }
        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (expense != null)
            {
                db.Entry(expense).State = System.Data.Entity.EntityState.Modified;
            }
           
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Incomes()
        {
            return View(db.Income.ToList());
        }

        public ActionResult EditIncomes(int? id)
        {
            return View(db.Income.FirstOrDefault(x=>x.id==id));
        }
        [HttpPost]
        public ActionResult EditIncomes(Income income)
        {
            if (income != null)
            {
                db.Entry(income).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Incomes");
        }

        public ActionResult DeleteIncome(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Income ıncome = db.Income.Find(id);
            db.Income.Remove(ıncome);
            db.SaveChanges();
            return RedirectToAction("Incomes");
        }
    }
}


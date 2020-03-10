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

            DateTime date = DateTime.Today;
            int month = date.Month;
            //I bought this month's spending and revenues if it was empty.
            var expense = db.Expense.Any(s => s.Date.Value.Month == month);
            var income = db.Income.Any(s => s.Date.Value.Month == month);
            //If expenditure and income are not empty, I have received total income, total expenditure and total budget.
            if (expense==true && income==true )
            {
                var totalExpense = db.Expense.Where(s => s.Date.Value.Month == month).Sum(s => s.Amount);
                ViewData["totalexpense"] = totalExpense;
                var totalIncome = db.Income.Where(s => s.Date.Value.Month == month).Sum(s => s.Amount);
                ViewData["totalincome"] = totalIncome;
                var totalbudget = totalIncome - totalExpense;
                ViewData["totalbudget"] = totalbudget;
               
            }
            //if spending is empty, income is not empty
            if (expense==false && income == true)
            {
                var totalIncome = db.Income.Where(s => s.Date.Value.Month == month).Sum(s => s.Amount);
                ViewData["totalincome"] = totalIncome;
                var totalbudget = totalIncome - 0;
                ViewData["totalbudget"] = totalbudget;
                ViewData["totalexpense"] = "0";
            }
            //if income is empty, spending is not empty
            if (income==false && expense == true)
            {
                var totalExpense = db.Expense.Where(s => s.Date.Value.Month == month).Sum(s => s.Amount);
                ViewData["totalexpense"] = totalExpense;
                var totalbudget = 0 - totalExpense;
                ViewData["totalbudget"] = totalbudget;
                ViewData["totalincome"] = "0";
            }
            //if both are empty
            if (income == false && expense == false)
            {
                ViewData["totalincome"] = "0";
                ViewData["totalexpense"] = "0";
                ViewData["totalbudget"] = 0;
            }
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
            //category selection list to add expenditure 
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
            //Show details of expenditures in the category with the submitted id
            return View(db.Expense.Where(x => x.CategoryID == id).ToList());
        }

        public ActionResult Delete(int? id)
        {
            //delete spend on sent id
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
            //save edited expenditure on
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
            return RedirectToAction("Index");
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


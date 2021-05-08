using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using accounts.Models;

namespace accounts.Controllers
{
    public class AccountsController : Controller
    {
        private Mydbcontext db = new Mydbcontext();

        // GET: Accounts
        public ActionResult Index()
        {
            var account = new List<DisplayAccounts>();
            var accunts = (from c in db.accounts
                         join e in db.main_account
                             on c.Main_id equals e.Main_id
                         select new
                         {
                             Account_id = c.Account_id,
                             Account_name = c.Account_name,
                             Account_number = c.Account_number,
                             Main_id = c.Main_id,
                             Father = e.Name,
                            
            }).ToList();
            foreach (var item in accunts)
            {
                var curr = new List<Checkboxviewmodel>();
                var query = from n in db.currencies
                            where n.accountcurrencies.Any(c => c.Account_id == item.Account_id)
                            select n;
                foreach (var item2 in query)
                {
                    curr.Add(new Checkboxviewmodel { Name = item2.Name ,symbol=item2.symbol});
                }
                account.Add(new DisplayAccounts { Account_id = item.Account_id, Account_name = item.Account_name, Account_number = item.Account_number ,FatherName=item.Father,currncies=curr});
            }
                return View(account);
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.Main_id = new SelectList(db.main_account, "Main_id", "Name");
            var result = from b in db.currencies
                         select new
                         {
                             b.Currency_id,
                             b.Name,
                             b.symbol
                         };

            var myviewmodel = new Accountviewmodel();
            var mycheckboxlist = new List<Checkboxviewmodel>();
            foreach (var item in result)
            {
                mycheckboxlist.Add(new Checkboxviewmodel { Id = item.Currency_id, Name = item.Name});
            }
            myviewmodel.currncies = mycheckboxlist;
            return View(myviewmodel);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Accountviewmodel accounts)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Account_name", "Is requaird");
                return View("Create", accounts);
            }
            else if (accounts.Account_name!=null && accounts.Account_number != 0 && accounts.Main_id!=0)
            {

                var Myaccount1 = new Accounts()
                {
                    Account_name = accounts.Account_name,
                    Account_number = accounts.Account_number,
                    Main_id = accounts.Main_id
                };
                db.accounts.Add(Myaccount1);
                int id = Myaccount1.Account_id;
                foreach (var item in accounts.currncies)
                {
                    if (item.Checked)
                    {
                        db.accountcurrencies.Add(new Accountcurrencies() { Account_id = Myaccount1.Account_id, Currency_id = item.Id });
                    }
                }
                db.SaveChanges();
                ViewBag.Main_id = new SelectList(db.main_account, "Main_id", "Name", accounts.Main_id);
                return RedirectToAction("Index");
            }
                return View();

        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.Main_id = new SelectList(db.main_account, "Main_id", "Name", accounts.Main_id);

            var result = from b in db.currencies
                         select new
                         {
                             b.Currency_id,
                             b.Name,
                             b.symbol,
                             Checked = ((from ab in db.accountcurrencies
                                         where (ab.Account_id == id) & (ab.Currency_id == b.Currency_id)
                                         select ab).Count() > 0)
                         };

            var myviewmodel = new Accountviewmodel();
            myviewmodel.Account_id = id.Value;
            myviewmodel.Account_name = accounts.Account_name;
            myviewmodel.Account_number = accounts.Account_number;
            myviewmodel.Main_id = accounts.Main_id;
            var mycheckboxlist = new List<Checkboxviewmodel>();

            foreach (var item in result)
            {
                mycheckboxlist.Add(new Checkboxviewmodel { Id = item.Currency_id, Name = item.Name, Checked = item.Checked });
            }
            myviewmodel.currncies = mycheckboxlist;
            return View(myviewmodel);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Accountviewmodel accounts)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Account_name", "Is requaird");
                return View("Edit", accounts);
            }
            else if (accounts.Account_name != null && accounts.Account_id != 0) {
                var Myaccount = db.accounts.Find(accounts.Account_id);
                Myaccount.Account_name = accounts.Account_name;
                Myaccount.Account_number = accounts.Account_number;
                Myaccount.Main_id = accounts.Main_id;

                foreach (var item in db.accountcurrencies)
                {
                    if (item.Account_id == accounts.Account_id)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in accounts.currncies)
                {
                    if (item.Checked)
                    {
                        db.accountcurrencies.Add(new Accountcurrencies() { Account_id = accounts.Account_id, Currency_id = item.Id });
                    }
                }
                // db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                ViewBag.Main_id = new SelectList(db.main_account, "Main_id", "Name", accounts.Main_id);
                return View(accounts);
            }
            else
            {
                return View("Edit", accounts);
            }
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }

            var account = new DisplayAccounts();
            account.Account_id = id.Value;
            account.Account_name = accounts.Account_name;
            account.Account_number = accounts.Account_number;

            account.FatherName = db.main_account.Find(accounts.Main_id).Name.ToString();
            var mycheckboxlist = new List<Checkboxviewmodel>();
            var query = from n in db.currencies
                        where n.accountcurrencies.Any(c => c.Account_id == id)
                        select n;
            foreach (var item2 in query)
            {
                mycheckboxlist.Add(new Checkboxviewmodel { Name = item2.Name, symbol = item2.symbol });
            }
            account.currncies = mycheckboxlist;
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.accounts.Find(id);   
            var dep = db.accountcurrencies.Where(d => d.Account_id == id).First();
            db.accountcurrencies.Remove(dep);
            db.accounts.Remove(accounts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

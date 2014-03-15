using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

using PagedList;

using NHibernate.Linq;

using MVC4.Helpers;
using MVC4.Models;

namespace MVC4.Controllers
{
    public class UserController : Controller
    {
		// GET: /User/
		public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
			if (String.IsNullOrEmpty (sortOrder)) {
				sortOrder = "name_desc"; 
			}

			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

			int pageSize = 10;
			int pageNumber = (page ?? 1);

			if (null != searchString) {
				page = 1;
			} else {
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			System.Linq.IQueryable<MVC4.Models.User> users = null;

			using (var session = NHibernateHelper.OpenSession())
			{
				users = from u in session.Query<User>()
					select u;

				if (!String.IsNullOrEmpty (searchString)) {
					users = users.Where(u => u.LastName.ToUpper().Contains(searchString.ToUpper())
						|| u.FirstName.ToUpper().Contains(searchString.ToUpper()));
				}

				switch (sortOrder)
				{
				case "name_desc":
					users = users.OrderByDescending(u => u.LastName);
					break;
				case "Date":
					users = users.OrderBy(u => u.EnrollmentDate);
					break;
				case "date_desc":
					users = users.OrderByDescending(u => u.EnrollmentDate);
					break;
				default:  // Name ascending
					users = users.OrderBy(u => u.LastName);
					break;                                                                                                             
				}

				return View(users.ToPagedList(pageNumber, pageSize));
			}
				
        }

		// GET: /User/Details
		public ActionResult Details(Guid? id)
        {
			if (null == id) {
				return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
			}

			using (var session = NHibernateHelper.OpenSession ())
			{
				User user = session.Get<User> (id);

				if (null == user) {
					return HttpNotFound ();
				}

				return View (user);

			}
        }

		// GET: /User/Create
        public ActionResult Create()
        {
            return View ();
        } 

		// POST: /User/Create
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "FirstName, MidName, LastName, EnrollmentDate")]User user)
        {
            try {
                
				if(ModelState.IsValid) {
					using (var session = NHibernateHelper.OpenSession())
					{
						using (var transaction = session.BeginTransaction())                                                     
						{
							session.Save(user);
							transaction.Commit();

							return RedirectToAction ("Index");
						}
					}
				}

			} catch (RetryLimitExceededException /* exc */) {
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
			return View (user);
        }
        
		public ActionResult Edit(Guid? id)
        {
			if (null == id) {
				return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
			}

			using (var session = NHibernateHelper.OpenSession ())
			{
				User user = session.Get<User> (id);

				if (null == user) {
					return HttpNotFound ();
				}

				return View (user);

			}
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID, FirstName, MidName, LastName, EnrollmentDate")]User user)
        {
            try {
				if(ModelState.IsValid) {

					using (var session = NHibernateHelper.OpenSession ()) {
						using (var transaction = session.BeginTransaction()) {

							session.SaveOrUpdate(user);
							transaction.Commit();

						}
					}

					return RedirectToAction ("Index");
				}
                
			} catch (RetryLimitExceededException /* exc */) {
				ModelState.AddModelError ("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

			return View (user);
        }

		public ActionResult Delete(Guid? id, bool? saveChangesError = false)
        {
			if (null == id) {
				return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
			}

			if (saveChangesError.GetValueOrDefault ()) {
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}

			using (var session = NHibernateHelper.OpenSession ())
			{
				User user = session.Get<User> (id);

				if (null == user) {
					return HttpNotFound ();
				}

				return View (user);

			}
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid? id)
        {
            try {

				if(ModelState.IsValid) {

					using (var session = NHibernateHelper.OpenSession ()) {

						User user = session.Get<User> (id);

						if (null == user) {
							return HttpNotFound ();
						}

						using (var transaction = session.BeginTransaction()) {

							session.Delete(user);
							transaction.Commit();

						}
					}

					return RedirectToAction ("Index");
				}

                return RedirectToAction ("Index");
			} catch (RetryLimitExceededException /* exc */) {
				return RedirectToAction ("Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}
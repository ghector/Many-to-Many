using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Milo.Models;

namespace Milo.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {



            //var availableemployees = db.Employees.ToList();
            //var selectedemployees = db.Employees.ToList().Where(x => x.Age > 30).Select(x=>x.EmployeeId);


            //ViewBag.employeeId = new MultiSelectList(availableemployees, "EmployeeId", "Name", selectedemployees);

            ViewBag.DirectorId = new SelectList(db.Directors.OrderBy(x=>x.FirstName), "DirectorId", "FirstName");

            var availableActors = db.Actors.ToList();
            ViewBag.ActorId = new SelectList(availableActors, "ActorId", "FirstName");


            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Title,DirectorId,ActorId")] Movie movie, List<int> ActorId)
        {
            if (ModelState.IsValid)
            {
               var director=  db.Directors.Find(movie.DirectorId);

                if (director is null)
                {
                    ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);
                    return View(movie);
                }
                else
                {
                    db.Movies.Add(movie);

                    foreach (var id in ActorId)
                    {
                        Actor actor = db.Actors.Find(id);
                        if(actor!=null)
                        {
                            movie.Actors.Add(actor);
                        }

                    }


                    db.SaveChanges();
                }
             
                return RedirectToAction("Index");
            }

            
            return View(movie);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);



            var availableActors = db.Actors.ToList();
            var selectedActors = movie.Actors.Select(x => x.ActorId);

            ViewBag.ActorId = new MultiSelectList(availableActors, "ActorId", "FirstName", selectedActors);



            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,DirectorId,ActorId")] Movie movie, List<int> ActorId)
        {
            if (ModelState.IsValid)
            {
                movie.MovieId = 19;
                db.Movies.Attach(movie);//Eimaste etoimoi na kanoume kati me aytin tin tainia
                db.Entry(movie).Collection("Actors").Load();  //Fortose tous ithopoious apo aytin tin tainia
                movie.Actors.Clear();
                db.SaveChanges();  //Se ayto to simeio i tainia den exei ithopoious

                if(ActorId is null || ActorId.Count()==0)
                {
                    db.SaveChanges();
                }
                else
                {

                    foreach (var id in ActorId)
                    {
                        Actor actor = db.Actors.Find(id);
                        if (actor != null)
                        {
                            movie.Actors.Add(actor);
                        }

                    }
                    db.SaveChanges();
                }




                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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

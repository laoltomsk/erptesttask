﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_testTask.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace ERP_testTask.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.Movies.OrderByDescending(movie => movie.Id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Movies/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View(new MovieCreateModel());
        }

        // POST: Movies/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Name = model.Name,
                    Description = model.Description,
                    Year = model.Year,
                    Director = model.Director,
                    UserId = User.Identity.GetUserId()
                };

                if (model.Poster != null)
                {
                    string fileName = System.IO.Path.GetFileName(model.Poster.FileName);
                    model.Poster.SaveAs(Server.MapPath("~/Posters/" + model.Name + "_" + fileName));
                    movie.PosterURL = "/Posters/" + model.Name + "_" + fileName;
                }

                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = movie.Id });
            }

            return View(model);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var model = new MovieCreateModel
            {
                Name = movie.Name,
                Description = movie.Description,
                Year = movie.Year,
                Director = movie.Director
            };

            return View(model);
        }

        // POST: Movies/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Int32? id, MovieCreateModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            if (movie.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                movie.Name = model.Name;
                movie.Description = model.Description;
                movie.Year = model.Year;
                movie.Director = model.Director;

                if (model.Poster != null)
                {
                    string fileName = System.IO.Path.GetFileName(model.Poster.FileName);
                    model.Poster.SaveAs(Server.MapPath("~/Posters/" + model.Name + "_" + fileName));
                    movie.PosterURL = "/Posters/" + model.Name + "_" + fileName;
                }

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = movie.Id });
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            if (movie.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

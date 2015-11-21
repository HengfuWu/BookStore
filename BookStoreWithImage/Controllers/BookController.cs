using BookStoreWithImage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWithImage.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Show(int pageNumber)
        {
            List<GetBooksOfPage_Result> books = null;
            using (BookStoreEntities dbContext = new BookStoreEntities())
            {
                int bookPerPage = 5;
                books = dbContext.GetBooksOfPage(bookPerPage, pageNumber).ToList();
                ViewBag.MaxPageNumber = dbContext.Books.Count()/bookPerPage + 1;
            }
            return View(books);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = null;
            using (BookStoreEntities dbContext = new BookStoreEntities())
            {
                book = dbContext.Books.SingleOrDefault(b => b.Id == id);
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            using (BookStoreEntities dbContext = new BookStoreEntities())
            {
                if (this.Request.Files != null && this.Request.Files.Count > 0 && this.Request.Files[0].ContentLength > 0 && this.Request.Files[0].ContentLength < 1024 * 100)
                {
                    string fileName = Path.GetFileName(this.Request.Files[0].FileName);
                    string filePathOfWebsite = "~/Images/Covers/" + fileName;
                    book.CoverImagePath = filePathOfWebsite;
                    this.Request.Files[0].SaveAs(this.Server.MapPath(filePathOfWebsite));
                }

                dbContext.Books.Attach(book);
                dbContext.Entry(book).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }

            return RedirectToAction("Show", new { pageNumber = 1 });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book book = null;
            using (BookStoreEntities dbContext = new BookStoreEntities())
            {
                book = dbContext.Books.SingleOrDefault(b=>b.Id==id);
                if (book != null)
                {
                    dbContext.Books.Remove(book);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            using (BookStoreEntities dbContext = new BookStoreEntities())
            {
                dbContext.Books.Add(book);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Show");
        }
    }
}
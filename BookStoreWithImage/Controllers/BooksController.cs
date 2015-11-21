using BookStoreWithImage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWithImage.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Books book)
        {
            if (book.Name == null || book.Price == null)
            {
                return View("CreateError");
            }
            else if (book != null && this.ModelState.IsValid)
            {
                book.Id = ++BookProviders.Counter + 50;
                BookProviders.Instance.AllBooks.Add(book);
                return RedirectToAction("Show", new { pageNumber = 1 });
            }
            else
            {
                return RedirectToAction("Show", new { pageNumber = 1 });
            }
        }

        public ActionResult Show(int pageNumber)
        {
            List<Books> books = null;

            int bookPerPage = 5;
            ViewBag.MaxPageNumber = BookProviders.Instance.AllBooks.Count() / bookPerPage + 1;
            books = BookProviders.Instance.AllBooks.Skip(bookPerPage * (pageNumber - 1)).Take(bookPerPage).ToList();
            return View(books);
        }


        public ActionResult Delete(int id)
        {
            Books deletingBook = BookProviders.Instance.AllBooks.SingleOrDefault(b => b.Id == id);
            if (deletingBook != null)
            {
                BookProviders.Instance.AllBooks.Remove(deletingBook);
            }

            return RedirectToAction("Show", new { pageNumber = 1 });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Books editingBook = BookProviders.Instance.AllBooks.SingleOrDefault(b => b.Id == id);
            if (editingBook != null)
            {
                return View(editingBook);
            }

            return View("EditError");
        }

        [HttpPost]
        public ActionResult Edit(Books book)
        {
            if (book.Name != null && book.Price != null && this.ModelState.IsValid)
            {
                Books updatedBook = BookProviders.Instance.AllBooks.SingleOrDefault(b => b.Id == book.Id);
                if (updatedBook != null)
                {
                    updatedBook.Name = book.Name;
                    updatedBook.Price = book.Price;
                }
            }
            else
            {
                return View("EditError");
            }

            return RedirectToAction("Show", new { pageNumber = 1 });
        }

        [HttpGet]
        public ActionResult SearchBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query(int? minPrice, int? maxPrice, int maxResult)
        {
            if (minPrice == null || maxPrice == null)
            {
                return View("SearchError");
            }
            else {
                IEnumerable<Books> selectedBooks = BookProviders.Instance.AllBooks.Where(b => b.Price >= minPrice && b.Price <= maxPrice).Take(maxResult);

                int bookPerPage = 5;
                List<Books> books = null;
                ViewBag.MaxPageNumber = selectedBooks.Count() / bookPerPage + 1;
                books = selectedBooks.Skip(bookPerPage * (2 - 1)).Take(bookPerPage).ToList();

                return View("BookList", selectedBooks);
            }
        }
    }
}
using BookStoreWithImage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWithImage.Controllers
{
    public class TradeController : Controller
    {
        // GET: Trade
        [HttpGet]
        public ActionResult Buy()
        {
            BuyViewModel vm = new BuyViewModel() { Limit = 99999 };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Buy(BuyViewModel vm)
        {
            if (vm.Amount <= 0 || vm.Amount > vm.Limit || vm == null || !this.ModelState.IsValid)
            {
                return View("TradeError");
            }

            Books targeBook = BookProviders.Instance.AllBooks.SingleOrDefault(b => b.Id == vm.BookId);
            User targeUser = UserProvider.Instance.AllUser.SingleOrDefault(u => u.Id == vm.UserId);
            if (targeUser == null || targeBook == null)
            {
                return View("TradeError");
            }

            Order order = new Order();
            order.ID = (new Random()).Next(1, 10000);
            order.BookId = vm.BookId;
            order.UserId = vm.UserId;
            order.Total = (double)targeBook.Price * vm.Amount;

            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Order = order;
            orderViewModel.Book = targeBook;
            orderViewModel.User = targeUser;

            return View("Order", orderViewModel);
        }
    }
}
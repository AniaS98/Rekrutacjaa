using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rekrutacjaa.Models;
using Microsoft.AspNetCore.Http;

namespace Rekrutacjaa.Controllers
{
    public class LibraryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    user.UserId = Guid.NewGuid();
                    user.AccountType = accTypes.user;
                    uow.UserRepository.Insert(user);
                    uow.Commit();
                }
                ModelState.Clear();
                ViewBag.Message = "User " + user.Login + " succesfully registered!";
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
            {
                var _user = uow.UserRepository.GetUserbyCreds(user.Login, user.Password);

                if(_user != null)
                {
                    HttpContext.Session.SetString("UserID", _user.UserId.ToString());
                    HttpContext.Session.SetString("Login", _user.Login.ToString());

                    if(_user.AccountType == accTypes.user)
                        return RedirectToAction("ListOfBooks");
                    else
                        return RedirectToAction("EditableListOfBooks");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }
            }
            return View();
        }

        // GET: LibraryController/ListOfBooks/5
        public ActionResult ListOfBooks()
        {
            if(HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    return View(uow.BookRepository.GetAll());
                }

            }
            else
            {
                return RedirectToAction("Login");
            }

            
        }

        public ActionResult EditableListOfBooks()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    return View(uow.BookRepository.GetAll());
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Reserve(Guid id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    Guid _userid = Guid.Parse(HttpContext.Session.GetString("UserID"));
                    Book _book = uow.BookRepository.GetBook(id);

                    if(_book.IsAvailable == true)
                    {
                        _book.IsAvailable = false;
                        Reservation reservation = new Reservation() { ReservationId = Guid.NewGuid(), BookId = id, UserId = _userid, ReservationDate = DateTime.Now };
                        uow.ReservationRepository.Insert(reservation);
                        uow.Commit();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ReservationHistory(Guid id)
        {
            List<Reservation> selectedReservations = new List<Reservation>();

            if (HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    List<Reservation> reservationList = (List<Reservation>)uow.ReservationRepository.GetReservations(id);

                    foreach (Reservation r in reservationList)
                    {
                        Console.WriteLine(r.ReservationDate);
                        if (r.ReservationId.Equals(id) == true)
                        {
                            selectedReservations.Add(r);
                        }
                    }

                    return View(selectedReservations);
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult NewBook()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View(new Book());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: LibraryController/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewBook(Book book)
        {
            using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
            {
                book.BookId = Guid.NewGuid();
                uow.BookRepository.Insert(book);
                uow.Commit();

                return RedirectToAction("EditableListOfBooks");
            }
        }

        public ActionResult Edit(Guid id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    return View(uow.BookRepository.GetBook(id));
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: LibraryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Book book)
        {
            using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
            {
                Book newBook = uow.BookRepository.GetBook(id);
                newBook.Author = book.Author;
                newBook.Description = book.Description;
                newBook.IsAvailable = book.IsAvailable;
                newBook.Name = book.Name;
                newBook.ReleaseDate = book.ReleaseDate;
                uow.Commit();
                return RedirectToAction("EditableListOfBooks");
            }
        }

        public ActionResult Return(Guid id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
                {
                    return View(uow.BookRepository.GetBook(id));
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: LibraryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(Guid id, Book book)
        {
            using (IUnitOfWork uow = new LibraryUnitOfWork(new LibraryDBContext()))
            {
                Book newBook = uow.BookRepository.GetBook(id);
                newBook.IsAvailable = true;
                uow.Commit();
                return RedirectToAction("EditableListOfBooks");
            }
        }




    }
}

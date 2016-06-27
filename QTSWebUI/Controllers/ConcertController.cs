using System;
using System.Linq;
using System.Web.Mvc;
using QTSDataManager.DB;
using QTSDataManager.Repositories;

namespace QTSWebUI.Controllers
{
    [Authorize]
    public class ConcertController : MyBaseController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var cr = new ConcertRepository();
                var concerts = cr.Read();
                return View(concerts);

            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Concert concert)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(concert);
                }

                var cr = new ConcertRepository();
                cr.Create(concert);
                return RedirectToAction("Index");
            } // TODO: повна валідація форми
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var res = new ConcertRepository().Read(id);
                return View(res);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Concert concert)
        {
            try
            {
                var cr = new ConcertRepository();
                cr.Update(concert);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult Ticket(int id)
        {
            try
            {
                ViewBag.ConcertId = id;
                var tr = new TicketsRepository();
                var tickets = tr.ReadByConcertId(id).OrderBy(x => x.Fan);
                return View(tickets);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateTicket(int concertId)
        {
            try
            {
                var tr = new TicketsRepository();
                var tickets = tr.ReadByConcertId(concertId);
                var funsZones = tickets.Select(x => x.Fan).Distinct();
                return View(new CreateTicketViewModel
                {
                    ConcertId = concertId,
                    AvailableFans = funsZones
                });
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel ticket)
        {
            try
            {
                var tr = new TicketsRepository();
                tr.Create(new Ticket
                {
                    ConcertId = ticket.ConcertId,
                    Fan = ticket.Fan,
                    Price = ticket.Price,
                    TicketType = ticket.TicketType
                }, ticket.Count);
                return RedirectToAction("Ticket", new { id = ticket.ConcertId });
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }


        [Authorize]
        [HttpGet]
        public ActionResult TicketEdit(int id, int concertId)
        {
            try
            {
                var tr = new TicketsRepository();
                var res = tr.Read(id);
                var tickets = tr.ReadByConcertId(concertId);
                var funsZones = tickets.Select(x => x.Fan).Distinct();
                ViewBag.funsZones = funsZones;
                return View(res);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult TicketEdit(Ticket ticket)
        {
            try
            {
                var tr = new TicketsRepository();
                tr.Update(ticket);
                return RedirectToAction("Ticket", new { id = ticket.ConcertId });
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult Customers(int concertId, string ticketType, string fan, int ticketId)
        {
            try
            {
                var cr = new CustomerRepository();
                var customers = cr.ReadByTicketType(concertId, ticketType, fan, ticketId);
                ViewBag.ConcertId = concertId;

                return View(customers);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        
        public ActionResult Unreserve(int customerId, int concertId)
        {
            try
            {
                var cr = new CustomerRepository();
                cr.Delete(customerId);
                return RedirectToAction("Ticket", new {Id = concertId});
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        public ActionResult AddGroup(int concertId)
        {
            try
            {
                ViewBag.ConcertId = concertId;
                var res = new MusicGroupRepository();
                return View(res.Read());
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        public ActionResult AddGroupToConcert(int groupId, int concertId)
        {
            try
            {
                var res = new MusicGroupRepository();
                res.AddGroupToConcert(concertId, groupId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }
    }
}
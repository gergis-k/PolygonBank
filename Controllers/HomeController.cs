using Microsoft.AspNetCore.Mvc;
using PolygonBank.Data;
using PolygonBank.DTO;
using PolygonBank.Models;
using System.Diagnostics;
using System.Text;

namespace PolygonBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/Transactions")]
        public IActionResult Transactions()
        {
            var TransactionDtoList = (from tfr in _context.Transfers
                                      select new TransactionDto()
                                      {
                                          Id = tfr.Id,
                                          Amount = tfr.Transaction.Amount.ToString(),
                                          Date = tfr.Date,
                                          Reciever = tfr.Receiver.Username,
                                          Sender = tfr.Sender.Username,
                                          Serial = tfr.Transaction.Serial,
                                      }).ToList();

            return View(TransactionDtoList);
        }

        [Route("Home/Customers")]
        public IActionResult Customers()
        {
            var CustomersList = _context.Customers.ToList();
            return View(CustomersList);
        }

        [HttpGet]
        [Route("Home/Customer/{id}")]
        public IActionResult Customer(int id, TransactionDto model, string? fake)
        {
            
            var c = _context.Customers.FirstOrDefault(x => x.Id == id);

            if (c is null)
                return RedirectToAction("Customers", "Home");

            ViewBag.customer = c;

            ViewBag.CustomersList = _context.Customers.Where(x => x.Id != c.Id).ToList();

            ViewBag.TransfersList = _context.Transfers.Where(x => x.Receiver.Id == id).Select(
                x => new TransactionDto()
                {
                    Id = x.Id,
                    Sender = x.Sender.Username,
                    Amount = x.Transaction.Amount.ToString(),
                    Serial = x.Transaction.Serial,
                    Date = x.Date,
                    Reciever = x.Receiver.Username,
                }).ToList();

            return View(model);
        }

        private string genSer()
        {
            Random rand = new Random();
            var digits = "0123456789ABCDEFGHIGKLMNOPQRSTUVWXYZ";
            var output = new StringBuilder();

            for (int i = 0; i < 14; i++)
            {
                var randomNumber = rand.Next(digits.Length);
                var randomChar = digits[randomNumber];
                output.Append(randomChar);
            }
            return output.ToString();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Customer(int id, TransactionDto model)
        {
            var sen = _context.Customers.FirstOrDefault(x => x.Id == id);

            var rec = _context.Customers.FirstOrDefault(x => x.Username == model.Reciever);

            double amo = 0;

            double.TryParse(model.Amount, out amo);

            sen.Balance -= amo;
            rec.Balance += amo;

            var tra = new Transaction()
            {
                Amount = amo,
                Serial = genSer()
            };

            _context.Transactions.Add(tra);
            _context.SaveChanges();

            var tfr = new Transfer();
            tfr.Date = DateTime.Now;
            tfr.Receiver = rec;
            tfr.Sender = sen;
            tfr.Transaction = tra;

            _context.Transfers.Add(tfr);
            _context.SaveChanges();

            return RedirectToAction("Transactions");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
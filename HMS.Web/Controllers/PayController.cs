using HMS.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;


namespace HMS.Web.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Index()
        {
            List<BillEntity> billEntities = new List<BillEntity>();

            billEntities = new List<BillEntity>
            {
                new BillEntity
                {
                    Billtype = "Water",
                    Rate = 1500,
                    Quantity=1,
                    ImagePath= "img/pexels-pixabay-358636.jpg",
                },
                new BillEntity
                {
                    Billtype = "Electricity",
                    Rate = 1500,
                    Quantity=1,
                    ImagePath= "img/pexels-pixabay-358636.jpg",
                }
            };
            return View(billEntities);
        }

        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();

            Session session = service.Get(TempData["Session"].ToString());

            if(session.Status == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("Success");

            }
            return View("Login");

        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult PayNow()
        {
            List<BillEntity> billEntities = new List<BillEntity>();

            billEntities = new List<BillEntity>
            {
                new BillEntity
                {
                    Billtype = "Water",
                    Rate = 1500,
                    Quantity=1,
                    ImagePath= "img/pexels-pixabay-358636.jpg",
                },
                new BillEntity
                {
                    Billtype = "Electricity",
                    Rate = 1500,
                    Quantity=1,
                    ImagePath= "img/pexels-pixabay-358636.jpg",
                }
            };

            var domain = "https://localhost:7055/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + $"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };
            
            foreach (var item in billEntities)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Rate * item.Quantity),
                        Currency = "aud",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Billtype.ToString(),
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }
    }
}

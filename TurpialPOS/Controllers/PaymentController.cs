using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurpialPOS.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult BeginPayment()
        {
            return PartialView("_BeginPayment");
        }
    }
}
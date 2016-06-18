using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SaleOfDetails.Domain.DataAccess.Interfaces;

namespace SaleOfDetails.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            
        }

        public ActionResult Index()
        {
            Logger.Info("Открытие главной страницы приложения.");

            return View();
        }
    }
}

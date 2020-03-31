using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurpialPOS.ViewModel.Admin;

namespace TurpialPOS.Controllers
{
    public class AdminController : Controller
    {
        private RegisterRepository _registerRepository;
        private readonly int storeId = 1;

        public AdminController()
        {
            _registerRepository = new RegisterRepository();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var viewModel = new MenuViewModel();
            viewModel.OpenRegister = _registerRepository.CheckIfTodayHasRegisterOpen(storeId);
            viewModel.OldOpenRegister = _registerRepository.CheckIfOpenedRegisterIsOld(storeId);
            viewModel.ClosedRegister = _registerRepository.CheckIfTodayRegisterWasClosed(storeId);
            return PartialView("_Menu", viewModel);
        }
    }
}
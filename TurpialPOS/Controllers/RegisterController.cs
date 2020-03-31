using DAL.DTO;
using DAL.Repositories;
using DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurpialPOS.Controllers
{
    public class RegisterController: Controller
    {
        private RegisterRepository _registerRepository;
        private readonly int storeId = 1;

        public RegisterController()
        {
            _registerRepository = new RegisterRepository();
        }

        public ActionResult OpenRegister()
        {
            var dto = new RegisterDTO();
            return PartialView("_OpenRegister", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpenRegister(RegisterDTO dto)
        {
            var username = User.Identity.Name;
            dto.StoreId = storeId;
            dto.OpeningUsername = username;
            _registerRepository.Add(dto);
            TempData["Message"] = "La caja para el día " + DateTime.Now.ToShortDateString() + " ha sido abierta.";
            return RedirectToAction("Dashboard", "Admin", null);
        }

        public ActionResult CloseRegister()
        {
            var storeId = 1;
            var username = User.Identity.Name;
            var dto = _registerRepository.GetLastUnClosedRegister(storeId);

            if (dto == null)
                dto = new RegisterDTO();
            dto.CashInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Cash, storeId);
            dto.CreditInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Credit, storeId);
            dto.DebitInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Debit, storeId);
            dto.CheckInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Check, storeId);
            dto.TransferInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Transfer, storeId);

            return PartialView("_CloseRegister", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseRegister(RegisterDTO dto)
        {
            var username = User.Identity.Name;
            dto.ClosingUsername = username;
            dto.CashInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Cash, storeId);
            dto.CreditInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Credit, storeId);
            dto.DebitInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Debit, storeId);
            dto.CheckInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Check, storeId);
            dto.TransferInfo = _registerRepository.GetClosingRegisterInfo(dto.OpeningTime, PaymentTypes.Transfer, storeId);
            _registerRepository.Close(dto);
            TempData["Message"] = "La caja para el día " + dto.OpeningTime.ToShortDateString() + " ha sido cerrada.";
            return RedirectToAction("Dashboard", "Admin", null);
        }

        public ActionResult ProcessRegisterExit()
        {
            var register = _registerRepository.GetTodaysRegister(storeId);
            var dto = new RegisterExitDTO();
            if (register != null)
                dto.TodaysExits = register.RegisterCashExits.ToList();
            return PartialView("_ProcessRegisterExit", dto);
        }

        [HttpPost]
        public ActionResult ProcessRegisterExit(RegisterExitDTO dto)
        {
            if (dto.Amount > 0 && dto.Description != null)
            {
                var userName = User.Identity.Name;
                var register = _registerRepository.GetTodaysRegister(storeId);
                if (register != null)
                {
                    if (register.ClosingTime == null)
                    {
                        var todayPayments = _registerRepository.GetTodaysCashPayments(storeId);
                        var registerActualAmount = register.GetActualAmount() + todayPayments;
                        if (dto.Amount > registerActualAmount && !dto.CashEntering)
                        {
                            TempData["ErrorMessage"] = "Se intentó retirar de la caja un monto mayor al de apertura y la suma de pagos en efectivo.";
                            return RedirectToAction("Dashboard", "Admin", null);
                        }
                        dto.RegisterId = register.Id;
                        dto.Username = userName;
                        _registerRepository.AddRegisterCashExit(dto);
                        TempData["Message"] = "Se ha registrado un movimiento de efectivo en la caja del día de hoy.";
                        return RedirectToAction("Dashboard", "Admin", null);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "La caja del día de hoy ha sido cerrada.";
                        return RedirectToAction("Dashboard", "Admin", null);
                    }
                }
            }
            TempData["ErrorMessage"] = "Problemas al insertar el movimiento de caja, por favor revisar los datos.";
            return RedirectToAction("Dashboard", "Admin", null);
        }
    }
}
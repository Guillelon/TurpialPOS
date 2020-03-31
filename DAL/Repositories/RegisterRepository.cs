using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RegisterRepository
    {
        private TurpialPOSDbContext _context;

        public RegisterRepository()
        {
            _context = new TurpialPOSDbContext();
        }

        public Register Add(RegisterDTO dto)
        {
            var register = new Register();
            register.StoreId = dto.StoreId;
            register.OpeningUsername = dto.OpeningUsername;
            //Bills
            register.HundredBillsOpening = dto.HundredBills;
            register.FiftyBillsOpening = dto.FiftyBills;
            register.TwentyBillsOpening = dto.TwentyBills;
            register.TenBillsOpening = dto.TenBills;
            register.FiveBillsOpening = dto.FiveBills;
            register.OneBillsOpening = dto.OneBills;
            //Coins
            register.OneCoinsOpening = dto.OneCoins;
            register.FiftyCentsCoinsOpening = dto.FiftyCentsCoins;
            register.QuarterCoinsOpening = dto.QuarterCoins;
            register.TenCentsCoinsOpening = dto.TenCentsCoins;
            register.FiveCentsCoinsOpening = dto.FiveCentsCoins;
            register.OneCentCoinsOpening = dto.OneCentCoins;

            var returnRegister = _context.Register.Add(register);
            _context.SaveChanges();
            return returnRegister;
        }

        public Register Close(RegisterDTO dto)
        {
            var register = _context.Register.Where(r => r.Id == dto.Id).FirstOrDefault();
            register.ClosingUsername = dto.ClosingUsername;
            register.ClosingTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            register.HundredBillsClosing = dto.HundredBillsClosing;
            register.FiftyBillsClosing = dto.FiftyBillsClosing;
            register.TwentyBillsClosing = dto.TwentyBillsClosing;
            register.TenBillsClosing = dto.TenBillsClosing;
            register.FiveBillsClosing = dto.FiveBillsClosing;
            register.OneBillsClosing = dto.OneBillsClosing;
            register.OneCoinsClosing = dto.OneCoinsClosing;
            register.FiftyCentsCoinsClosing = dto.FiftyCentsCoinsClosing;
            register.QuarterCoinsClosing = dto.QuarterCoinsClosing;
            register.TenCentsCoinsClosing = dto.TenCentsCoinsClosing;
            register.FiveCentsCoinsClosing = dto.FiveCentsCoinsClosing;
            register.OneCentCoinsClosing = dto.OneCentCoinsClosing;

            register.CashTransactionCount = dto.CashInfo.IntValue;
            register.CashTransactionAmount = dto.CashInfo.DecimalValue;

            register.DebitTransactionCount = dto.DebitInfo.IntValue;
            register.DebitTransactionAmount = dto.DebitInfo.DecimalValue;

            register.CreditTransactionCount = dto.CreditInfo.IntValue;
            register.CreditTransactionAmount = dto.CreditInfo.DecimalValue;

            register.CheckTransactionCount = dto.CheckInfo.IntValue;
            register.CheckTransactionAmount = dto.CheckInfo.DecimalValue;

            register.TransferTransactionCount = dto.TransferInfo.IntValue;
            register.TransferTransactionAmount = dto.TransferInfo.DecimalValue;

            _context.Entry(register).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return register;
        }

        public Register GetRegisterByDate(DateTime date, int storeId)
        {
            try
            {
                var nextDate = date.AddDays(1).Date;
                var pastDay = date.Date.AddSeconds(-1);
                var register = _context.Register.Where(r => r.CreateDate < nextDate
                                                                   && r.CreateDate > pastDay
                                                                   && r.StoreId == storeId).FirstOrDefault();
                return register;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool CheckIfTodayHasRegisterOpen(int storeId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var registerToday = _context.Register.Where(r => r.StoreId == storeId).OrderByDescending(r => r.CreateDate).FirstOrDefault();
            if (registerToday != null)
            {
                if (registerToday.CreateDate.Date == today.Date)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public Register GetTodaysRegister(int storeId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var registerToday = _context.Register.Where(r => r.StoreId == storeId).OrderByDescending(r => r.CreateDate).FirstOrDefault();
            if (registerToday != null)
            {
                if (registerToday.CreateDate.Date == today.Date)
                    return registerToday;
                else
                    return null;
            }
            else
                return null;
        }

        public RegisterCashExit AddRegisterCashExit(RegisterExitDTO dto)
        {
            var registerCashExit = new RegisterCashExit();
            registerCashExit.Username = dto.Username;
            registerCashExit.Amount = dto.Amount;
            registerCashExit.Description = dto.Description;
            registerCashExit.RegisterId = dto.RegisterId;
            registerCashExit.CashEntering = dto.CashEntering;
            _context.RegisterCashExit.Add(registerCashExit);
            _context.SaveChanges();
            return registerCashExit;
        }

        public bool CheckIfTodayRegisterWasClosed(int storeId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var registerToday = _context.Register.Where(r => r.StoreId == storeId).OrderByDescending(r => r.CreateDate).FirstOrDefault();
            if (registerToday != null)
            {
                if (registerToday.CreateDate.Date == today.Date && registerToday.ClosingTime.HasValue)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool CheckIfOpenedRegisterIsOld(int storeId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var registerToday = _context.Register.Where(r => r.StoreId == storeId).OrderByDescending(r => r.CreateDate).FirstOrDefault();
            if (registerToday != null)
            {
                if (registerToday.CreateDate.Date < today.Date && !registerToday.ClosingTime.HasValue)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public RegisterDTO GetLastUnClosedRegister(int storeId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var model = _context.Register.Where(r => r.StoreId == storeId).OrderByDescending(r => r.CreateDate).FirstOrDefault();
            if (model != null)
            {
                if (!model.ClosingTime.HasValue)
                {
                    var dto = new RegisterDTO(model);
                    return dto;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public IntDecimalDual GetClosingRegisterInfo(DateTime date, string typeSystemName, int storeId)
        {
            var nextDate = date.AddDays(1).Date;
            var pastDay = date.Date.AddSeconds(-1);

            var value = new IntDecimalDual();
            var paymentCash = _context.PaymentType.Where(t => t.SystemName == typeSystemName).FirstOrDefault();
            var cashPayments = _context.Payment.Where(t => t.CreateDate > pastDay
                                                             && t.CreateDate < nextDate
                                                             && t.PaymentTypeId == paymentCash.Id
                                                             && t.StoreId == storeId).ToList();
            value.IntValue = cashPayments.Count();
            value.DecimalValue = cashPayments.Sum(t => t.Amount);
            return value;
        }

        public IList<RegisterCashExit> GetRegisterExits(int id)
        {
            var exits = _context.RegisterCashExit.Where(r => r.RegisterId == id).ToList();
            return exits;
        }

        public decimal GetTodaysCashPayments(int franchiseId)
        {
            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var cashPaymentsType = _context.PaymentType.Where(pt => pt.SystemName == Resources.PaymentTypes.Cash).FirstOrDefault();
            var nextDate = today.AddDays(1).Date;
            var pastDay = today.Date.AddSeconds(-1);
            var payments = _context.Payment.Where(p => p.CreateDate < nextDate &&
                                                                 p.CreateDate > pastDay &&
                                                                 p.StoreId == franchiseId).ToList();
            var paymentsAmount = payments.Sum(p => p.Amount);
            return paymentsAmount;
        }
    }
}

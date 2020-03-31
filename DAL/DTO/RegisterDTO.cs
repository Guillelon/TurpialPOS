using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string OpeningUsername { get; set; }
        public string ClosingUsername { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public int HundredBills { get; set; }
        public int FiftyBills { get; set; }
        public int TwentyBills { get; set; }
        public int TenBills { get; set; }
        public int FiveBills { get; set; }
        public int OneBills { get; set; }
        public int OneCoins { get; set; }
        public int FiftyCentsCoins { get; set; }
        public int QuarterCoins { get; set; }
        public int TenCentsCoins { get; set; }
        public int FiveCentsCoins { get; set; }
        public int OneCentCoins { get; set; }

        public int HundredBillsClosing { get; set; }
        public int FiftyBillsClosing { get; set; }
        public int TwentyBillsClosing { get; set; }
        public int TenBillsClosing { get; set; }
        public int FiveBillsClosing { get; set; }
        public int OneBillsClosing { get; set; }
        public int OneCoinsClosing { get; set; }
        public int FiftyCentsCoinsClosing { get; set; }
        public int QuarterCoinsClosing { get; set; }
        public int TenCentsCoinsClosing { get; set; }
        public int FiveCentsCoinsClosing { get; set; }
        public int OneCentCoinsClosing { get; set; }

        public IntDecimalDual CashInfo { get; set; }
        public IntDecimalDual DebitInfo { get; set; }
        public IntDecimalDual CheckInfo { get; set; }
        public IntDecimalDual CreditInfo { get; set; }
        public IntDecimalDual TransferInfo { get; set; }

        public IList<RegisterExitDTO> RegisterExists { get; set; }

        public RegisterDTO()
        {
            OpeningTime = DateTime.Now;
            RegisterExists = new List<RegisterExitDTO>();
            CashInfo = new IntDecimalDual { DecimalValue = 0, IntValue = 0 };
            CreditInfo = new IntDecimalDual { DecimalValue = 0, IntValue = 0 };
            DebitInfo = new IntDecimalDual { DecimalValue = 0, IntValue = 0 };
            CheckInfo = new IntDecimalDual { DecimalValue = 0, IntValue = 0 };
            TransferInfo = new IntDecimalDual { DecimalValue = 0, IntValue = 0 };
        }

        public RegisterDTO(Register model)
        {
            Id = model.Id;
            OpeningTime = model.OpeningTime;
            RegisterExists = model.RegisterCashExits.Select(t => new RegisterExitDTO
            {
                Amount = t.Amount,
                Description = t.Description,
                Username = t.Username,
                CashEntering = t.CashEntering
            }).ToList();
            OpeningUsername = model.OpeningUsername;
            ClosingUsername = model.ClosingUsername;
            HundredBills = model.HundredBillsOpening;
            FiftyBills = model.FiftyBillsOpening;
            TwentyBills = model.TwentyBillsOpening;
            TenBills = model.TenBillsOpening;
            FiveBills = model.FiveBillsOpening;
            OneBills = model.OneBillsOpening;

            HundredBillsClosing = model.HundredBillsClosing;
            FiftyBillsClosing = model.FiftyBillsClosing;
            TwentyBillsClosing = model.TwentyBillsClosing;
            TenBillsClosing = model.TenBillsClosing;
            FiveBillsClosing = model.FiveBillsClosing;
            OneBillsClosing = model.OneBillsClosing;

            OneCoins = model.OneCoinsOpening;
            QuarterCoins = model.QuarterCoinsOpening;
            FiftyCentsCoins = model.FiftyCentsCoinsOpening;
            TenCentsCoins = model.TenCentsCoinsOpening;
            FiveCentsCoins = model.FiveCentsCoinsOpening;
            OneCentCoins = model.OneCentCoinsOpening;

            OneCoinsClosing = model.OneCoinsClosing;
            QuarterCoinsClosing = model.QuarterCoinsClosing;
            FiftyCentsCoinsClosing = model.FiftyCentsCoinsClosing;
            TenCentsCoinsClosing = model.TenCentsCoinsClosing;
            FiveCentsCoinsClosing = model.FiveCentsCoinsClosing;
            OneCentCoinsClosing = model.OneCentCoinsClosing;

            CashInfo = new IntDecimalDual { DecimalValue = model.CashTransactionAmount, IntValue = model.CashTransactionCount };
            CreditInfo = new IntDecimalDual { DecimalValue = model.CreditTransactionAmount, IntValue = model.CreditTransactionCount };
            DebitInfo = new IntDecimalDual { DecimalValue = model.DebitTransactionAmount, IntValue = model.DebitTransactionCount };
            CheckInfo = new IntDecimalDual { DecimalValue = model.CheckTransactionAmount, IntValue = model.CheckTransactionCount };
            TransferInfo = new IntDecimalDual { DecimalValue = model.TransferTransactionAmount, IntValue = model.TransferTransactionCount };

            RegisterExists = model.RegisterCashExits.Select(t => new RegisterExitDTO
            {
                Amount = t.Amount,
                Description = t.Description,
                Username = t.Username,
                CashEntering = t.CashEntering
            }).ToList();
        }

        public decimal TotalExitsAmount()
        {
            var existsCash = RegisterExists.Where(r => !r.CashEntering).Sum(r => r.Amount);
            var entersCash = RegisterExists.Where(r => r.CashEntering).Sum(r => r.Amount);
            return entersCash - existsCash;
        }

        public decimal OpeningAmount()
        {
            var total = decimal.Zero;
            total += HundredBills * 100;
            total += FiftyBills * 50;
            total += TwentyBills * 20;
            total += TenBills * 10;
            total += FiveBills * 5;
            total += OneBills * 1;

            total += OneCoins * 1;
            total += FiftyCentsCoins * 0.5m;
            total += QuarterCoins * 0.25m;
            total += TenCentsCoins * 0.10m;
            total += FiveCentsCoins * 0.05m;
            total += OneCentCoins * 0.01m;

            return total;
        }

        public decimal ClosingShouldCash()
        {
            return CashInfo.DecimalValue + OpeningAmount()
                + (RegisterExists.Where(r => r.CashEntering).Sum(r => r.Amount))
                - (RegisterExists.Where(r => !r.CashEntering).Sum(r => r.Amount));
        }

        public decimal ClosingAmount()
        {
            var total = decimal.Zero;
            total += HundredBillsClosing * 100;
            total += FiftyBillsClosing * 50;
            total += TwentyBillsClosing * 20;
            total += TenBillsClosing * 10;
            total += FiveBillsClosing * 5;
            total += OneBillsClosing * 1;

            total += OneCoinsClosing * 1;
            total += FiftyCentsCoinsClosing * 0.5m;
            total += QuarterCoinsClosing * 0.25m;
            total += TenCentsCoinsClosing * 0.10m;
            total += FiveCentsCoinsClosing * 0.05m;
            total += OneCentCoinsClosing * 0.01m;

            return total;
        }
    }

    public class RegisterExitDTO
    {
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public bool CashEntering { get; set; }
        public string Description { get; set; }
        public int RegisterId { get; set; }
        public IList<RegisterCashExit> TodaysExits { get; set; }

        public RegisterExitDTO()
        {
            TodaysExits = new List<RegisterCashExit>();
        }
    }
}

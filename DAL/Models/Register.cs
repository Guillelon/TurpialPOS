using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string OpeningUsername { get; set; }
        public string ClosingUsername { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public int HundredBillsOpening { get; set; }
        public int FiftyBillsOpening { get; set; }
        public int TwentyBillsOpening { get; set; }
        public int TenBillsOpening { get; set; }
        public int FiveBillsOpening { get; set; }
        public int OneBillsOpening { get; set; }
        public int OneCoinsOpening { get; set; }
        public int FiftyCentsCoinsOpening { get; set; }
        public int QuarterCoinsOpening { get; set; }
        public int TenCentsCoinsOpening { get; set; }
        public int FiveCentsCoinsOpening { get; set; }
        public int OneCentCoinsOpening { get; set; }

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

        public int CashTransactionCount { get; set; }
        public decimal CashTransactionAmount { get; set; }

        public int DebitTransactionCount { get; set; }
        public decimal DebitTransactionAmount { get; set; }

        public int CreditTransactionCount { get; set; }
        public decimal CreditTransactionAmount { get; set; }

        public int CheckTransactionCount { get; set; }
        public decimal CheckTransactionAmount { get; set; }

        public int TransferTransactionCount { get; set; }
        public decimal TransferTransactionAmount { get; set; }

        public virtual IList<RegisterCashExit> RegisterCashExits { get; set; }

        public virtual Store Store { get; set; }
        public int StoreId { get; set; }

        public Register()
        {
            CreateDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            OpeningTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            CashTransactionCount = 0;
            CashTransactionAmount = 0m;
            DebitTransactionCount = 0;
            DebitTransactionAmount = 0m;
        }

        public decimal GetActualAmount()
        {
            var existsCash = RegisterCashExits.Where(r => !r.CashEntering).Sum(r => r.Amount);
            var entersCash = RegisterCashExits.Where(r => r.CashEntering).Sum(r => r.Amount);
            var opening = GetOpeningAmount();
            return opening + (entersCash - existsCash);
        }

        public decimal GetOpeningAmount()
        {
            var totalAmount = 0m;
            totalAmount += HundredBillsOpening * 100;
            totalAmount += FiftyBillsOpening * 50;
            totalAmount += TwentyBillsOpening * 20;
            totalAmount += TenBillsOpening * 10;
            totalAmount += FiveBillsOpening * 5;
            totalAmount += OneBillsOpening * 1;
            totalAmount += OneCoinsOpening * 1;
            totalAmount += FiftyCentsCoinsOpening * 0.5m;
            totalAmount += QuarterCoinsOpening * 0.25m;
            totalAmount += TenCentsCoinsOpening * 0.1m;
            totalAmount += FiveCentsCoinsOpening * 0.05m;
            totalAmount += OneCentCoinsOpening * 0.01m;
            return totalAmount;
        }
    }
}

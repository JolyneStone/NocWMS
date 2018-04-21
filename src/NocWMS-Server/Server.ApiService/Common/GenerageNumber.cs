using System;
using System.Threading;
namespace Server.ApiService.Common
{
    public class GenerageNumber
    {
        private static int _receiptCount = 0;
        public static string GetInboundReceiptNumber()
        {
            return "IN-" + GetNumberCore();
        }

        public static string GetOutboundReceiptNumber()
        {
            return "OUT-" + GetNumberCore();
        }

        public static string GetStaffNumber()
        {
            return "S-" + GetNumberCore(); 
        }

        public static string GetVendorNumber()
        {
            return "V-" + GetNumberCore();
        }

        public static string GetCustomerNumber()
        {
            return "C-" + GetNumberCore();
        }

        private static string GetNumberCore()
        {
            Interlocked.CompareExchange(ref _receiptCount , 1, 1000);
            var number = _receiptCount .ToString("D3") + DateTime.Now.ToString("yyMMddHHmmss");
            Interlocked.Increment(ref _receiptCount );
            return number;
        }
    }
}

using EgyptCurrencyRates.ViewModels;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace EgyptCurrencyRates.Help
{
    public static class RequestExtensions
    {
        //regex from http://detectmobilebrowsers.com/
        private static readonly Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static bool IsMobileBrowser(this HttpRequest request)
        {
            var userAgent = request.UserAgent();
            if ((b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4))))
            {
                return true;
            }
            return false;
        }
        public static string UserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"];
        }
    }


    public static class Analytics
    {
        public static DateTime DateTime()
        {

            //// Cross-platform solution
            string egyptTimeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Egypt Standard Time"     // Windows ID
                : "Africa/Cairo";          // Linux/Unix ID

            DateTime egyptTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, egyptTimeZoneId);
            return egyptTime;
            //return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "Africa/Cairo");
        }

        public static string Day()
        {
            string day = DateTime().DayOfWeek.ToString();

            switch (day)
            {
                case "Friday":
                    day = "الجمعة";
                    break;
                case "Saturday":
                    day = "السبت";
                    break;
                case "Sunday":
                    day = "الأحد";
                    break;
                case "Monday":
                    day = "الاثنين";
                    break;
                case "Tuesday":
                    day = "الثلاثاء";
                    break;
                case "Wednesday":
                    day = "الأربعاء";
                    break;
                case "Thursday":
                    day = "الخميس";
                    break;
            }
            return day;
        }
        public static string Day(DayOfWeek dayOfWeek)
        {
            string day = dayOfWeek.ToString();

            switch (day)
            {
                case "Friday":
                    day = "الجمعة";
                    break;
                case "Saturday":
                    day = "السبت";
                    break;
                case "Sunday":
                    day = "الأحد";
                    break;
                case "Monday":
                    day = "الاثنين";
                    break;
                case "Tuesday":
                    day = "الثلاثاء";
                    break;
                case "Wednesday":
                    day = "الأربعاء";
                    break;
                case "Thursday":
                    day = "الخميس";
                    break;
            }
            return day;
        }
        public static string Month(int month)
        {
            switch (month)
            {
                case 1:
                    return "يناير";
                case 2:
                    return "فبراير";
                case 3:
                    return "مارس";
                case 4:
                    return "أبريل";
                case 5:
                    return "مايو";
                case 6:
                    return "يونيو";
                case 7:
                    return "يوليو";
                case 8:
                    return "أغسطس";
                case 9:
                    return "سبتمبر";
                case 10:
                    return "أكتوبر";
                case 11:
                    return "نوفمبر";
                case 12:
                    return "ديسمبر";
                default:
                    return "";
            }
        }
        public static string Date()
        {
            return System.DateTime.Now.ToString("dd-MM-yyyy");
        }

        public static string ConvertDate(DateTime date)
        {
            string minute = date.Minute.ToString();
            string time = (date.Hour > 12 ? date.Hour - 12 : date.Hour).ToString();
            string period = date.Hour > 12 ? "م" : "ص";
            string dayOfWeek = Analytics.Day(date.DayOfWeek);
            string day = date.Day.ToString();
            string month = Analytics.Month(date.Month);
            string year = date.Year.ToString();
            return string.Format("{1}:{0} {2}, {3}, {4} {5} {6}", minute, time, period, dayOfWeek, day, month, year);
        }

        public static string BankKeywords(string bank, List<CurrencyViewModel> Currencies)
        {
            Currencies.Where(c => c.ID == 1 || c.ID == 2 || c.ID == 13).Select(c => c.Name).ToList();
            string result = string.Format("{0} ,اسعار العملات {0},أسعار العملات فى {0} ,{0} اسعار العملات, أسعار العملات فى {0} اليوم", bank);
            foreach (var Currency in Currencies)
            {

                result += string.Format(",سعر {1} اليوم فى {0},سعر {1} فى {0},سعر {1} اليوم {0},سعر {1} {0} اليوم,سعر {1} فى {0} اليوم", bank, Currency.Name);
            }
            return result += ",egypt currency rates,egyptcurrencyrates.com,egyptcurrencyrates";
        }


        //public static List<Rates> Top_Currency_Buy_Price()
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        var res = db.Rates.Where(r => r.BankId != 8 && r.BankId != 9 && r.BuyPrice != 0)
        //        .GroupBy(
        //            x => x.CurrencyId,
        //            (x, y) => new
        //            {
        //                ID = y.OrderByDescending(z => z.BuyPrice).FirstOrDefault().Id
        //            }
        //        ).ToList();

        //        List<Rates> rates = new List<Rates>();
        //        res.ForEach(e => rates.Add(db.Rates.Where(r => r.Id == e.ID).FirstOrDefault()));
        //        return rates.ToList();
        //    }
        //}

        //public static List<Rates> Lowest_Currency_Sale_Price()
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        var res = db.Rates.Where(r => r.BankId != 8 && r.BankId != 9 && r.SalePrice != 0)
        //        .GroupBy(
        //            x => x.CurrencyId,
        //            (x, y) => new
        //            {
        //                ID = y.OrderBy(z => z.SalePrice).FirstOrDefault().Id
        //            }
        //        ).ToList();
        //        List<Rates> rates = new List<Rates>();
        //        res.ForEach(e => rates.Add(db.Rates.Where(r => r.Id == e.ID).FirstOrDefault()));
        //        return rates.ToList();
        //    }
        //}

        //public static List<Rates> Central_Bank()
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        var res = db.Rates.Where(r => r.CurrencyId.Equals(1) && r.BankId.Equals(8)).ToList();
        //        return res;
        //    }
        //}

        //public static List<Rates> Avrage_Price()
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        var res = db.Rates.Where(r => r.CurrencyId.Equals(1) && r.BankId.Equals(9)).ToList();
        //        return res;
        //    }
        //}

        //public static Rates Top_Buy_Price_Per_Currency(int Currency_id)
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        return db.Rates.Where(r => r.CurrencyId.Equals(Currency_id) && r.BankId != 8 && r.BankId != 9 && r.BuyPrice != 0).OrderByDescending(r => r.BuyPrice).FirstOrDefault();
        //    }
        //}

        //public static Rates Lawest_Sale_Price_Per_Currency(int Currency_id)
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        return db.Rates.Where(r => r.CurrencyId.Equals(Currency_id) && r.BankId != 8 && r.BankId != 9 && r.SalePrice != 0)
        //            .OrderBy(r => r.SalePrice).FirstOrDefault();
        //    }
        //}

        //internal static List<Rates> Report(int CurrencyId)
        //{
        //    using (var db = new EgyptCurrencyRatesContext())
        //    {
        //        var res = db.Rates.Where(r => r.CurrencyId.Equals(CurrencyId))
        //            .Where(r => r.BankId != 8 && r.BankId != 9)
        //            .OrderByDescending(r => r.BuyPrice).ToList();
        //        return res;
        //    }
        //}


    }
}
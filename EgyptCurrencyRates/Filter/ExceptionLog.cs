using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.Filter
{
    public class ExceptionLogAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            using (var db = new EgyptCurrencyRatesContext())
            {
                ExceptionsLog exceptionsLog = new ExceptionsLog()
                {
                    Url = context.HttpContext.Request.Path.Value,
                    Exeption = context.Exception.Message,
                    Time = Analytics.DateTime(),
                };
                db.ExceptionsLogs.Add(exceptionsLog);
                db.SaveChanges();
            }
        }
    }
}

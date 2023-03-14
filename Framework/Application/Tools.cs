using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Application
{
    public static class Tools
    {
        public static string ToFileName(this DateTime date)
        {
            return $"{date.Year:0000}-{date.Month:00}-{date.Day:00}-{date.Hour:00}-{date.Minute:00}-{date.Second:00}";
        }
        public static string GetFileUrl(HttpContext context, string image)
        {
            var url = $"http://{context.Request.Host.Value}/Images{image}";
            return url;
        }
    }
}
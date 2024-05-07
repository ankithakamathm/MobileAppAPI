using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MobileAppAPI.DAL
{
    public static class Helper
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string ParseDateTime(string dateTimeString)
        {
            string[] formats = { "M/d/yyyy h:mm:ss tt", "dd-MM-yyyy HH:mm:ss" };
            DateTime parsedDateTime;

            if (DateTime.TryParseExact(dateTimeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
            {
                return parsedDateTime.ToString("yyyy-MM-dd");
            }
            else
            {
                return string.Empty; // or set a default value
                                     // Handle the error or set a default value as needed
            }
        }

        public static string ParseDateString(string dateString)
        {
            // Get the current system culture
            CultureInfo systemCulture = CultureInfo.CurrentCulture;

            // Get the short date pattern of the system culture
            string systemDateFormat = systemCulture.DateTimeFormat.ShortDatePattern;

            // Parse the date string using the appropriate format based on the system date format
            DateTime dateValue;
            switch (systemDateFormat)
            {
                case "dd-MM-yyyy":
                    if (DateTime.TryParseExact(dateString, "dd-MM-yyyy 00:00:00", null, DateTimeStyles.None, out dateValue))
                    {
                        // Successfully parsed date string in dd-MM-yyyy format
                        return dateValue.ToShortDateString();
                    }
                    else
                    {
                        // Invalid date string format
                        // Handle error case here
                        throw new ArgumentException("Invalid date string format");
                    }

                case "MM/dd/yyyy":
                    if (DateTime.TryParseExact(dateString, "MM/dd/yyyy 00:00:00", null, DateTimeStyles.None, out dateValue))
                    {
                        // Successfully parsed date string in MM/dd/yyyy format
                        return dateValue.ToShortDateString();
                    }
                    else
                    {
                        // Invalid date string format
                        // Handle error case here
                        throw new ArgumentException("Invalid date string format");
                    }

                case "MM-dd-yyyy":
                    if (DateTime.TryParseExact(dateString, "MM-dd-yyyy 00:00:00", null, DateTimeStyles.None, out dateValue))
                    {
                        // Successfully parsed date string in MM-dd-yyyy format
                        return dateValue.ToShortDateString();
                    }
                    else
                    {
                        // Invalid date string format
                        // Handle error case here
                        throw new ArgumentException("Invalid date string format");
                    }

                default:
                    // Unknown date format
                    // Handle error case here
                    throw new ArgumentException("Unknown date format");
            }
        }

       
    }
}

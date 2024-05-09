using MobileAppAPI.BLL;
using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.WebApi.Helpers
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
        public static string Encrypt(this string text, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("The text must have valid value.", nameof(text));

            var buffer = Encoding.UTF8.GetBytes(text);
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(buffer))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    var result = resultStream.ToArray();
                    var combined = new byte[aes.IV.Length + result.Length];
                    Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
                    Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                    return Convert.ToBase64String(combined);
                }
            }
        }

        public static string Decrypt(this string encryptedText, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentException("The encrypted text must have valid value.", nameof(encryptedText));

            var combined = Convert.FromBase64String(encryptedText);
            var buffer = new byte[combined.Length];
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                var iv = new byte[aes.IV.Length];
                var ciphertext = new byte[buffer.Length - iv.Length];

                Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
                Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(ciphertext))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    return Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
        }
        public static DataTable CreateDataTableFroOrderItems(List<OrderItems> orderItems)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ItemName", typeof(string));
            dataTable.Columns.Add("ItemQuantity", typeof(int));
            dataTable.Columns.Add("Currency", typeof(string));
            
            dataTable.Columns.Add("Price", typeof(decimal));
            dataTable.Columns.Add("SubTotal", typeof(decimal));


            var dataRows = orderItems.Select(item => new object[]
            {
            item.itemName,
            item.itemQuantity,
             item.currency,
            
            item.itemPrice,
             item.itemTotal,

            });

            foreach (var dataRow in dataRows)
            {
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
namespace CurrencyConversionOnStereoids
{
    class GetConversionRate
    {
        string currencyFrom;
        string currencyTo;
        decimal rate;
        public GetConversionRate(string from, string to)
        {
            this.currencyFrom = from.ToUpper();
            this.currencyTo = to.ToUpper();
        }
        public bool LoadRate()
        {
            try
            {

            string resp = string.Empty;
            string url = @"https://open.er-api.com/v6/latest/" + currencyFrom;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                resp = reader.ReadToEnd();
            }
                // Console.WriteLine(resp);//Have to delete this debug info and add errror handling



                string dat;
                if (resp!=null)
                {
                   dat = JObject.Parse(resp)["rates"].ToString();

                }
                else
                {
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                    dat = "0.0";
                }




                //Console.WriteLine(dat);
                string curr;
                 if (dat!=null)
                 {
                     curr = JObject.Parse(dat)[currencyTo].ToString();
                 }
                else
                {
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                    curr = "0.0";
                }
            //Console.WriteLine(curr);
            rate = decimal.Parse(curr);
            return true;
            }
            catch
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                return false;
            }

        }
        public decimal GetRate()
        {
            return rate;
        }
    }
}

using System;
using System.Threading;
namespace CurrencyConversionOnStereoids
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zdejte ticker měny {USD; CZK; EUR} z které chcete převést");
            string from = Console.ReadLine();
            Console.WriteLine("Zdejte ticker měny {USD; CZK; EUR} do které chcete převést");
            string to = Console.ReadLine();
            //Paralell conversion rate loading
            GetConversionRate cr = new GetConversionRate(from, to);
            bool succes = true;
            Thread t = new Thread(()=> {
                succes = cr.LoadRate();
                });
            t.Start();
            //Gets amount on main thread
            decimal amount = GetAmount(from);
            t.Join();
            
            if (!succes)
            {
                Console.WriteLine("Něco se nepodařilo, prosím zkuste to později");
                return;
            }

            Console.WriteLine($"{amount} {from} je {decimal.Round(amount * cr.GetRate(),2)} {to}");


        }
        static decimal GetAmount(string ticker)
        {
            Console.WriteLine($"Zadejte množství ${ticker.ToUpper()}, které chcete převést");
            decimal am = decimal.Zero;
            try
            {
                am = decimal.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                am = GetAmount(ticker);
               
            }
            return am;
        }
    }
}

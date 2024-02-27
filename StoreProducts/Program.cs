

using System;
using System.Globalization;
using StoreProducts.Entity;

namespace StoreProducts 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sourcePath = @"C:\temp\FileProducts\Itens.csv";
            var products = new List<Product>();
            
            try
            {
                using (var sr = new StreamReader(File.Open(sourcePath, FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        var lineSplit = sr.ReadLine().Split(",");

                        var product = new Product(lineSplit[0], double.Parse(lineSplit[1], CultureInfo.InvariantCulture), int.Parse(lineSplit[2]));
                        products.Add(product);
                    }
                }

                var sourceDirectory = Path.GetDirectoryName(sourcePath);
                Directory.CreateDirectory(sourceDirectory + @"\out");
                
                using (var sw = File.AppendText(sourceDirectory + @"\out\Summary.csv"))
                {
                    foreach (var product in products)
                    {
                        sw.WriteLine(product);
                    }
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
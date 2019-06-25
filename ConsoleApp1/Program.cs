using Newtonsoft.Json;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Price = 10;
            product.size = new string[] {"Small","Medium","Large" };

            string output = JsonConvert.SerializeObject(product,Formatting.Indented);
            File.WriteAllText(@"C:\Users\output.txt", output);

        }
    }
}

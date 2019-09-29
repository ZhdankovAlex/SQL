using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Sql
{
    class Program
    {
        static void Main()
        {
            IEnumerable<Car> autoCatalog = ReadFile("input.txt");
            //1
            foreach(var lada in from auto in autoCatalog
                               where auto.Name.StartsWith("Lada")
                               select auto)
            {
                Console.WriteLine(lada);
            }
            Console.WriteLine("");
            //2
            foreach (var longName in from auto in autoCatalog
                                 where auto.Name.Length > 20
                                 orderby auto.Name.Length descending
                                 select auto)
            {
                Console.WriteLine(longName);
            }
            Console.WriteLine("");
            //3
            foreach (var containsW in from auto in autoCatalog
                                     where auto.Name.Contains("W")
                                     select auto)
            {
                Console.WriteLine(containsW);
            }
            Console.WriteLine("");
            //4
            foreach (var mixed in from auto in autoCatalog
                                     where (auto.Name.Length > 15 && (auto.Name.Contains("s") || auto.Name.Contains("S")))
                                     orderby auto.Name.Length
                                     select auto)
            {
                Console.WriteLine(mixed);
            }
            Console.WriteLine("");
            //5
            foreach(var q in from auto in autoCatalog
                             where (auto.Name.Length > 23)
                             select auto)
            {
                foreach(var s in from symbol in q.Name orderby symbol select symbol)
                {
                    Console.Write(s);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private static IEnumerable<Car> ReadFile(string fileName)
        {
            var list = new List<Car>();
            using (var inputFile = new StreamReader(fileName))
            {
                string name = inputFile.ReadLine();
                while (name != null)
                {
                    list.Add(new Car(name));
                    name = inputFile.ReadLine(); 
                }
            }
            return list;
        }
    }
}

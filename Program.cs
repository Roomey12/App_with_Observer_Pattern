using System;
using System.Collections.Generic;

namespace lab3NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver driver1 = new Driver("AA3167BD", 100);
            _ = new DAI("Post 1", driver1);
            _ = new DAI("Post 2");
            _ = new DAI("Post 3");
            _ = new DAI("Post 4");
            _ = new DAI("Post 5");
            DAI.StartPatrol(driver1);
            Console.ReadLine();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfPictureService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WcfPictureService)))
            {
                host.Open();
                Console.WriteLine("Server is open.");
                Console.WriteLine("<Press enter to close server>");
                Console.ReadLine();
            }
        }
    }
}

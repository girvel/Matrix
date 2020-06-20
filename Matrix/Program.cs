using System;
using System.Linq;
using System.Threading;
using Matrix.Core;
using Matrix.Tools;

namespace Matrix
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var session = new Session();
            session.Start();
        }
    }
}
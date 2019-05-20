using System;
using System.Collections.Generic;
using Homework;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {

            var numberSubstitutions = new Dictionary<int, string>() {
                {3, "fizz"},
                {5, "buzz"}
            };

            var result = new Numbers().SubstitutedNumbers(100, numberSubstitutions);
            foreach(var r in result) {
                Console.WriteLine(r);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    public static class Fibonacci {
        static long FibRec(int n) {
            if(n < 0)
                return 0;
            else if(n < 2)
                return n;
            else
                return FibRec(n - 1) + FibRec(n - 2);
        }

        static long FibDin(int n) {
            if(n < 1) {
                return 0;
            } else {
                long[] temp = { 0, 0, 1 };
                for(int i = 1; i < n; i++) {
                    temp[0] = temp[1];
                    temp[1] = temp[2];
                    temp[2] = temp[1] + temp[0];
                }
                return temp[2];
            }
        }

        public static void Resolver() {
            const int salto = 30;
            const int separación = 12;
            for(int i = 0; i < 11; i++) {
                Console.CursorTop = i;
                Console.CursorLeft = 0;
                Console.Write("Fib({0}) = {1}", i, FibRec(i));

                Console.CursorTop = i + separación;
                Console.CursorLeft = 0;
                Console.Write("Fib({0}) = {1}", i + salto, FibRec(i + salto));
            }

            for(int i = 0; i < 11; i++) {
                Console.CursorTop = i;
                Console.CursorLeft = 40;
                Console.Write("Fib({0}) = {1}", i, FibDin(i));

                Console.CursorTop = i + separación;
                Console.CursorLeft = 40;
                Console.Write("Fib({0}) = {1}", i + salto, FibDin(i + salto));
            }
            Console.WriteLine();
        }
    }
}

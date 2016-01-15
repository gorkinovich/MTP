using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP {
    public static class Util {
        public static void Intercambiar(ref int a, ref int b) {
            int aux = a;
            a = b;
            b = aux;
        }

        public static void Mostrar(int[] lista) {
            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
        }
    }
}
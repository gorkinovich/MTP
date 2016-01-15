using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    public static class Monedas01 {
        public static bool ObtenerCambio(int[] tipos, int[] r, int c) {
            int i = 0;
            while(c > 0 && i < tipos.Length) {
                if(c < tipos[i]) {
                    i++;
                } else {
                    r[i]++;
                    c -= tipos[i];
                }
            }
            return c <= 0;
        }

        public static void Resolver() {
            int[] tipos = { 200, 100, 50, 20, 2, 1 };
            int[] result = { 0, 0, 0, 0, 0, 0 };
            int c = 393;

            ObtenerCambio(tipos, result, c);

            Console.WriteLine("Tipos: " + tipos.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("\nCantidad: {0}\n", c);
            Array.ForEach(Enumerable.Range(0, tipos.Length)
                                    .Select(i => tipos[i].ToString() + "\t" + result[i].ToString())
                                    .ToArray(), Console.WriteLine);
            Console.WriteLine("\nSuma: {0}\n", Enumerable.Range(0, tipos.Length)
                                                         .Select(i => tipos[i] * result[i])
                                                         .Sum());
        }
    }
}

// Esquema: Divide y vencerás

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    public static partial class DivideyVenceras {
        public static void Ejemplo01() {
            int[] lista = { 9, 4, 8, 2, 7, 1, 5, 3, 6 };
            Util.Mostrar(lista);
            lista = OrdenarPorMezcla(lista);
            Util.Mostrar(lista);
            Array.ForEach(Enumerable.Range(0, 11)
                                    .Select(i => i.ToString() + " -> " + Busqueda(i, lista).ToString())
                                    .ToArray(), Console.WriteLine);
        }

        public static void Ejemplo02() {
            int[] lista = { 9, 4, 8, 2, 7, 1, 5, 3, 6 };
            Util.Mostrar(lista);
            QuickSort(lista);
            Util.Mostrar(lista);
        }
    }
}
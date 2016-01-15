// Esquema: Divide y vencerás
// Ejemplo: Quicksort

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    public static partial class DivideyVenceras {
        private static void Particion(int[] lista, int inf, int sup, out int p) {
            int pivote = lista[inf];
            int j = inf;
            for(int i = inf + 1; i <= sup; i++) {
                if(lista[i] < pivote) {
                    j++;
                    Util.Intercambiar(ref lista[i], ref lista[j]);
                }
            }

            p = j;
            Util.Intercambiar(ref lista[inf], ref lista[p]);
        }

        public static void QuickSort(int[] lista, int inf, int sup) {
            if(inf < sup) {
                int piv;
                Particion(lista, inf, sup, out piv);
                QuickSort(lista, inf, piv);
                QuickSort(lista, piv + 1, sup);
            }
        }

        public static void QuickSort(int[] lista) {
            QuickSort(lista, 0, lista.Length - 1);
        }
    }
}
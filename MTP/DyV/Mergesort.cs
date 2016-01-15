// Esquema: Divide y vencerás
// Ejemplo: Mergesort

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    public static partial class DivideyVenceras {
        public static void Mezcla(int[] r, int[] l1, int[] l2) {
            int i = 0, j = 0, k = 0;
            while(i < l1.Length && j < l2.Length) {
                if(l1[i] <= l2[j]) {
                    r[k] = l1[i]; i++;
                } else {
                    r[k] = l2[j]; j++;
                }
                k++;
            }
            while(i < l1.Length) {
                r[k] = l1[i]; i++; k++;
            }
            while(j < l2.Length) {
                r[k] = l2[j]; j++; k++;
            }
        }

        public static int[] OrdenarPorMezcla(int[] lista) {
            int[] result = new int[lista.Length];
            if(lista.Length > 1) { // Caso recursivo
                int mitad = lista.Length / 2;
                int[] l1 = lista.Take(mitad).ToArray();
                int[] l2 = lista.Skip(mitad).ToArray();
                int[] r1 = OrdenarPorMezcla(l1);
                int[] r2 = OrdenarPorMezcla(l2);
                Mezcla(result, r1, r2);
            } else { // Caso base
                result[0] = lista[0];
            }
            return result;
        }
    }
}
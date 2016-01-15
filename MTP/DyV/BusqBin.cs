// Esquema: Divide y vencerás
// Ejemplo: Busqueda binaria

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    public static partial class DivideyVenceras {
        public static int Busqueda(int x, int[] lista, int pri, int ult) {
            if(pri <= ult) {
                int mitad = (pri + ult) / 2;
                if(lista[mitad] == x) {
                    return mitad;
                } else if(lista[mitad] > x) {
                    return Busqueda(x, lista, pri, mitad - 1);
                } else {
                    return Busqueda(x, lista, mitad + 1, ult);
                }
            } else {
                return -1;
            }
        }

        public static int Busqueda(int x, int[] lista) {
            return Busqueda(x, lista, 0, lista.Length - 1);
        }
    }
}
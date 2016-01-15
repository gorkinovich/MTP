using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Ejercicio 2:
     * Se dispone de 3 puntos de luz (casquillos) para iluminar un jardín y de M bombillas (M>=3)
     * para ajustar a dichos casquillos. Las bombillas pertenecen a marcas y calidades diferentes,
     * por lo que cada una de ellas posee una duración distinta. El objetivo es mantener una
     * iluminación completa del jardín durante el mayor tiempo posible y para ello se pide cuál
     * debería ser la asignación de bombillas a casquillos. Se pueden asignar varias bombillas al
     * mismo casquillo de tal manera que cuando una bombilla se apague se pueda sustituir por otra.
     * Resuelve el problema mediante un algoritmo voraz (no necesariamente óptimo). Proporciona un
     * contraejemplo si el algoritmo no es óptimo.
     */
    public static class Ejercicio102 {
        const int NumBom = 3;

        static int Max(int[] lista, int[] result, int val) {
            int vmax = int.MinValue;
            int imax = -1;
            int vmin = int.MaxValue;
            int imin = -1;

            for(int i = 0; i < lista.Length; i++) {
                if(result[i] == 0) {
                    if(vmax < lista[i] && lista[i] <= val) {
                        vmax = lista[i];
                        imax = i;
                    }

                    if(vmin > lista[i] && lista[i] >= val) {
                        vmin = lista[i];
                        imin = i;
                    }
                }
            }

            if(imax != -1 && imin != -1) {
                if(vmin - val < val - vmax) {
                    return imin;
                } else {
                    return imax;
                }
            } else {
                if(imax != -1) {
                    return imax;
                } else {
                    return imin;
                }
            }
        }

        static int[] Asignar(int[] lista) {
            int[] r = Enumerable.Range(0, lista.Length)
                                .Select(x => 0)
                                .ToArray();
            int media = lista.Sum() / NumBom;
            int[] bomb = { 0, 0, 0 };
            int j = 0, k;

            for(int i = 0; i < lista.Length; i++) {
                if(j < 2 && bomb[j] >= media) j++;
                k = Max(lista, r, media - bomb[j]);
                if(j < 2 && bomb[j] > 0 && bomb[j] + lista[k] - media > 1)
                    j++;
                r[k] = j + 1;
                bomb[j] += lista[k];
            }

            return r;
        }

        static void Mostrar(string msj, IEnumerable<int> lista) {
            Console.Write(msj);
            if(lista.Count() > 0) {
                Console.Write(" ({0}): ", lista.Sum().ToString());
                Console.WriteLine(lista.Select(x => x.ToString())
                                       .Aggregate((x, xs) => x + " " + xs));
            } else {
                Console.WriteLine(": No tiene solución...");
            }
        }

        static void Mostrar(int[] lista, int[] result) {
            List<int> b1 = new List<int>();
            List<int> b2 = new List<int>();
            List<int> b3 = new List<int>();

            for(int i = 0; i < lista.Length; i++) {
                switch(result[i]) {
                    case 1: b1.Add(lista[i]); break;
                    case 2: b2.Add(lista[i]); break;
                    case 3: b3.Add(lista[i]); break;
                }
            }

            Console.WriteLine("Media: {0}", lista.Sum() / NumBom);

            Mostrar("Bombilla 1", b1);
            Mostrar("Bombilla 2", b2);
            Mostrar("Bombilla 3", b3);
        }

        public static void Resolver(int[] lista) {
            int[] result = Asignar(lista);
            Mostrar(lista, result);
        }

        public static void Resolver() {
            int[] lista = { 5, 15, 9, 6, 10, 2, 11, 1,
                            12, 14, 4, 7, 13, 3, 16, 8 };
            Resolver(lista);

            Console.WriteLine();
            int[] lista2 = { 30, 20, 5, 6 };
            Resolver(lista2);

            Console.WriteLine();
            int[] lista3 = { 30, 20, 5, 10 };
            Resolver(lista3);

            Console.WriteLine();
            int[] lista4 = { 7, 2, 1, 6, 5, 7 };
            Resolver(lista4);
        }
    }
}

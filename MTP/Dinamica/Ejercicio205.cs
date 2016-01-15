using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Se dispone de 3 puntos de luz (casquillos) para iluminar un jardín y de M bombillas (M >= 3) para
     * ajustar a dichos casquillos. Las bombillas pertenecen a marcas y calidades diferentes, por lo que
     * cada una de ellas posee una duración distinta. El objetivo es mantener una iluminación completa
     * del jardín durante el mayor tiempo posible y para ello se pide cuál debería ser la asignación de
     * bombillas a casquillos. Se pueden asignar varias bombillas al mismo casquillo de tal manera que
     * cuando una bombilla se apague se pueda sustituir por otra.
     */
    public static class Ejercicio205 {
        const int NumBom = 3;

        static int Cercano(int a, int b, int x) {
            return (Math.Abs(x - b) < Math.Abs(x - a)) ? b : a;
        }

        static int[,] Calcular(int[] lista, bool[] usado, int media) {
            int MaxElem = lista.Length + 1;
            int MaxCant = media + 1;
            int[,] tabla = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                tabla[i, 0] = 0;
            }
            for(int i = 1; i < MaxCant; i++) {
                tabla[0, i] = 0;
            }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    if(!usado[i - 1] && j >= lista[i - 1]) {
                        a = tabla[i - 1, j];
                        b = tabla[i - 1, j - lista[i - 1]]
                            + lista[i - 1];
                        tabla[i, j] = Cercano(a, b, media);
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                    }
                }
            }
            //Mostrar(tabla, MaxElem, MaxCant);
            return tabla;
        }

        static void Marcar(int[] lista, bool[] usado,
                           int[,] tabla, int[] r, int media, int x) {
            int i = lista.Length, j = media;
            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(j >= lista[i - 1] &&
                          tabla[i - 1, j - lista[i - 1]] ==
                          tabla[i, j] - lista[i - 1]) {
                    r[i - 1] = x;
                    usado[i - 1] = true;
                    j -= lista[i - 1];
                    i--;
                } else if(j < lista[i - 1]) {
                    j = 0;
                }
            }
        }

        static int[] Asignar(int[] lista) {
            int[] r = Enumerable.Range(0, lista.Length)
                                .Select(x => 0)
                                .ToArray();
            bool[] usado = Enumerable.Range(0, lista.Length)
                                     .Select(x => false)
                                     .ToArray();
            int media = lista.Sum() / NumBom;
            int[,] tabla;

            for(int i = 1; i < NumBom; i++) {
                tabla = Calcular(lista, usado, media);
                Marcar(lista, usado, tabla, r, media, i);
            }
            for(int i = 0; i < r.Length; i++) {
                if(r[i] == 0) r[i] = NumBom;
            }
            return r;
        }

        public static void Resolver(int[] lista) {
            int[] l = lista.OrderByDescending(x => x).ToArray();
            int[] result = Asignar(l);
            Mostrar(l, result);
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 5, 15, 9, 6, 10, 2, 11, 1,
                            12, 14, 4, 7, 13, 3, 16, 8 };
            Resolver(lista);

            int[] lista2 = { 30, 20, 5, 6 };
            Resolver(lista2);

            int[] lista3 = { 30, 20, 5, 10 };
            Resolver(lista3);

            int[] lista4 = { 7, 2, 1, 6, 5, 7 };
            Resolver(lista4);
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

            Console.WriteLine("Lista: " +
                              lista.Select(x => x.ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Media: {0}", lista.Sum() / NumBom);
            Mostrar("Bombilla 1", b1);
            Mostrar("Bombilla 2", b2);
            Mostrar("Bombilla 3", b3);
        }

        static void Mostrar(int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    Console.Write(tabla[i, j].ToString("00") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /*
        static int[] Calcular2(int[] lista, bool[] usado, int media) {
            int MaxElem = lista.Length + 1;
            int[] tabla = new int[MaxElem];

            for(int i = 0; i < MaxElem; i++) {
                tabla[i] = 0;
            }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                if(!usado[i - 1]) {
                    a = tabla[i - 1];
                    b = tabla[i - 1] + lista[i - 1];
                    tabla[i] = Cercano(a, b, media);
                } else {
                    tabla[i] = tabla[i - 1];
                }
            }
            return tabla;
        }

        static void Marcar2(int[] lista, bool[] usado,
                           int[] tabla, int[] r, int media, int x) {
            int i = lista.Length;
            while(i > 0 ) {
                if(tabla[i] == tabla[i - 1]) {
                    i--;
                } else if(tabla[i - 1] == tabla[i] - lista[i - 1]) {
                    r[i - 1] = x;
                    usado[i - 1] = true;
                    i--;
                }
            }
        }

        static int[] Asignar2(int[] lista) {
            int[] r = Enumerable.Range(0, lista.Length)
                                .Select(x => 0)
                                .ToArray();
            bool[] usado = Enumerable.Range(0, lista.Length)
                                     .Select(x => false)
                                     .ToArray();
            int media = lista.Sum() / NumBom;
            int[] tabla;

            for(int i = 1; i <= NumBom; i++) {
                tabla = Calcular2(lista, usado, media);
                Marcar2(lista, usado, tabla, r, media, i);
            }

            for(int i = 0; i < r.Length; i++) {
                if(r[i] == 0) r[i] = 3;
            }

            return r;
        }

        public static void Resolver2(int[] lista) {
            int[] result = Asignar2(lista);
            Mostrar(lista, result);
            Console.WriteLine();
        }
        //*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Dados n enteros cualesquiera a1, a2, ..., an, necesitamos calcular el valor de la expresión:
     * max(1<=i<=j<=n) { Sum(k=i..j) a[k] }, que calcula el máximo de las sumas parciales de elementos
     * consecutivos. Como ejemplo, dados 6 números enteros: (-2, 11, -4, 13, -5, -2), la solución
     * al problema es 20 (suma de a[2] hasta a[4]). Deseamos implementar un algoritmo Divide y
     * Vencerás de complejidad (n log n) que resuelva el problema. ¿Existe algún otro algoritmo
     * que lo resuelva en menos tiempo?
     */
    public static class Ejercicio009 {
        //*****************************************************************
        // Solución O(n log n):
        //*****************************************************************

        // Ver Ejercicio005.cs

        //*****************************************************************
        // Solución esotérica O(n):
        //*****************************************************************

        #region Tipos de datos auxiliares
        public struct Secuencia {
            public Secuencia(int i, int l, int s) {
                this.inicio = i;
                this.longitud = l;
                this.suma = s;
            }
            public int inicio;
            public int longitud;
            public int suma;
        }

        public struct Resultado {
            public Resultado(int t) {
                this.total = t;
                this.maxIzq = new Secuencia(0, 0, 0);
                this.maxDer = new Secuencia(0, 0, 0);
                this.max = new Secuencia(0, 0, 0);
            }
            public int total;
            public Secuencia maxIzq;
            public Secuencia maxDer;
            public Secuencia max;
        }
        #endregion


        #region Algoritmo para calcular la súma máxima
        public static Secuencia Mayor2(Secuencia a, Secuencia b) {
            return (b.suma > a.suma) ? b : a;
        }

        public static Secuencia Mayor3(Secuencia a, Secuencia b, Secuencia c) {
            if((c.suma > a.suma) && (c.suma > b.suma))
                return c;
            else
                return Mayor2(a, b);
        }

        public static Secuencia Suma(Secuencia inf, Secuencia sup) {
            return new Secuencia(inf.inicio,
                                 (sup.inicio + sup.longitud) - inf.inicio,
                                 inf.suma + sup.suma);
        }

        public static Resultado SumaMaxima(int[] lista, int inf, int sup) {
            Resultado r = new Resultado();
            if(sup == inf) {
                r.total = lista[sup];
                r.maxIzq = new Secuencia(sup, 1, lista[sup]);
                r.maxDer = new Secuencia(sup, 1, lista[sup]);
                r.max = new Secuencia(sup, 1, lista[sup]);
            } else if(inf < sup) {
                int mitad = (inf + sup) / 2;
                Resultado i = SumaMaxima(lista, inf, mitad);
                Resultado d = SumaMaxima(lista, mitad + 1, sup);

                Secuencia totalIzq = new Secuencia(inf, mitad + 1 - inf, i.total);
                Secuencia totalDer = new Secuencia(mitad + 1, sup - mitad, d.total);

                r.total = i.total + d.total;
                r.maxIzq = Mayor2(i.maxIzq, Suma(totalIzq, d.maxIzq));
                r.maxDer = Mayor2(d.maxDer, Suma(i.maxDer, totalDer));
                r.max = Mayor3(d.max, i.max, Suma(i.maxDer, d.maxIzq));
            }
            return r;
        }

        public static Secuencia SumaMaxima(int[] lista) {
            if(lista.Length > 0) {
                return SumaMaxima(lista, 0, lista.Length - 1).max;
            } else {
                return new Secuencia(-1, 0, 0);
            }
        }
        #endregion


        #region Entrada del programa
        public static void MostrarResultado(int[] lista, Secuencia resultado) {
            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));

            if(resultado.inicio != -1) {
                IEnumerable<int> sublista = lista.Skip(resultado.inicio)
                                                 .Take(resultado.longitud);

                Console.WriteLine("Secuencia: {0}, {1}", resultado.inicio, resultado.longitud);
                Console.WriteLine("Suma: {0}", sublista.Sum());
                Console.WriteLine("Sublista: " + sublista.Select(x => x.ToString())
                                                         .Aggregate((x, xs) => x + " " + xs));
            } else {
                Console.WriteLine("No se ha encontrado solución...");
            }
        }

        public static void Resolver(int[] lista) {
            Ejercicio005.Resolver(lista);

            Console.WriteLine();
            Secuencia resultado = SumaMaxima(lista);
            MostrarResultado(lista, resultado);
        }

        public static void Resolver() {
            int[] lista = { 4, -1, 3, -2, 1, 5, -20, 10, -4, 10, -5, 1 };
            Resolver(lista);

            Console.WriteLine("\n");
            int[] lista2 = { 4, -1, 3, -2, 1, 5, -10, 10, -4, 10, -5, 1 };
            Resolver(lista2);

            Console.WriteLine("\n");
            int[] lista3 = { 15, -10, -30, -2, -1, 11, -10, 10, -4, 10, -5, 1 };
            Resolver(lista3);

            Console.WriteLine("\n");
            int[] lista4 = { 9, -10, -30, -30, 9, 9, -20, 4, -1, 10, -4, 10, -5, 1, -10, 1 };
            Resolver(lista4);
        }
        #endregion
    }
}

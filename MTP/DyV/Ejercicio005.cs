using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Ejercicio 5: Desarrolla un algoritmo para resolver el problema de la subsecuencia de suma
     * máxima con complejidad lineal y utilizando la técnica divide y vencerás, pero que en cada
     * paso de recursión del algoritmo divida en dos el vector de entrada.
     */
    public static class Ejercicio005 {
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

        public static Secuencia SumaMaxima(int[] lista, int inf, int sup) {
            Secuencia result = new Secuencia(sup, 1, lista[sup]);

            if(inf < sup) {
                int mitad = (inf + sup) / 2;
                Secuencia r1 = SumaMaxima(lista, inf, mitad);
                Secuencia r2 = SumaMaxima(lista, mitad + 1, sup);

                if(r1.suma >= r2.suma) {
                    result = r1;
                } else {
                    result = r2;
                }

                Secuencia r3 = r1;
                int final = r2.inicio + r2.longitud;
                for(int i = r1.inicio + r1.longitud; i < final; i++) {
                    r3.longitud++;
                    r3.suma += lista[i];
                    if(r3.suma >= result.suma) {
                        result = r3;
                    }
                }

                r3 = r2;
                final = r1.inicio + r1.longitud;
                for(int i = r2.inicio - 1; i >= final; i--) {
                    r3.inicio--;
                    r3.longitud++;
                    r3.suma += lista[i];
                    if(r3.suma >= result.suma) {
                        result = r3;
                    }
                }
            }

            return result;
        }

        public static Secuencia SumaMaxima(int[] lista) {
            if(lista.Length > 0) {
                return SumaMaxima(lista, 0, lista.Length - 1);
            } else {
                return new Secuencia(-1, 0, 0);
            }
        }

        public static Secuencia SumaMaximaIterativo(int[] lista) {
            Secuencia result = new Secuencia(-1, 0, 0);
            if(lista.Length > 1) {
                Secuencia aux = new Secuencia();
                result.inicio = 0;
                result.longitud = 1;
                result.suma = lista[0];

                for(int i = 0; i < lista.Length; i++) {
                    aux.inicio = i;
                    aux.longitud = 0;
                    aux.suma = 0;

                    for(int j = i; j < lista.Length; j++) {
                        aux.longitud++;
                        aux.suma += lista[j];
                        if(aux.suma > result.suma) {
                            result = aux;
                        }
                    }
                }
            } else if(lista.Length == 1) {
                result.inicio = 0;
                result.longitud = 1;
            }
            return result;
        }

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
            Secuencia resultado = SumaMaximaIterativo(lista);
            MostrarResultado(lista, resultado);

            Console.WriteLine();
            resultado = SumaMaxima(lista);
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
    }
}

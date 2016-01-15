using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Ejercicio 4: Dado un vector de caracteres, diseña un algoritmo que utilice la técnica divide
     * y vencerás para encontrar la subsecuencia más larga de caracteres iguales. Calcula el orden
     * de complejidad del algoritmo desarrollado.
     */
    public static class Ejercicio004 {
        public struct Secuencia {
            public Secuencia(int i, int l) {
                this.inicio = i;
                this.longitud = l;
            }
            public int inicio;
            public int longitud;
        }

        public static Secuencia SubsecuenciaMayor(string lista, int inf, int sup) {
            Secuencia result = new Secuencia(sup, 1);

            if(inf < sup) {
                int mitad = (inf + sup) / 2;
                Secuencia r1 = SubsecuenciaMayor(lista, inf, mitad);
                Secuencia r2 = SubsecuenciaMayor(lista, mitad + 1, sup);

                if((r1.inicio + r1.longitud == r2.inicio) && (lista[r1.inicio] == lista[r2.inicio])) {
                    result.inicio = r1.inicio;
                    result.longitud = r1.longitud + r2.longitud;
                } else {
                    if(r2.longitud > r1.longitud) {
                        result.inicio = r2.inicio;
                        result.longitud = r2.longitud;
                    } else {
                        result.inicio = r1.inicio;
                        result.longitud = r1.longitud;
                    }

                    if(lista[mitad] == lista[mitad + 1]) {
                        Secuencia r3 = new Secuencia(mitad, 2);

                        for(int i = mitad - 1; (i >= inf) && (lista[mitad] == lista[i]); i--) {
                            r3.inicio--;
                            r3.longitud++;
                        }
                        for(int i = mitad + 2; (i <= sup) && (lista[mitad] == lista[i]); i++) {
                            r3.longitud++;
                        }

                        if(r3.longitud > result.longitud) {
                            result.inicio = r3.inicio;
                            result.longitud = r3.longitud;
                        }
                    }
                }
            }

            return result;
        }

        public static Secuencia SubsecuenciaMayor(string lista) {
            if(lista.Length > 0) {
                return SubsecuenciaMayor(lista, 0, lista.Length - 1);
            } else {
                return new Secuencia(-1, 0);
            }
        }

        public static Secuencia SubsecMayorIterativo(string lista) {
            Secuencia result = new Secuencia(-1, 0);
            if(lista.Length > 1) {
                Secuencia aux = new Secuencia(1, 1);
                for(int i = 1; i < lista.Length; i++) {
                    if(lista[i - 1] == lista[i]) {
                        aux.longitud++;
                    } else {
                        if(result.longitud < aux.longitud) {
                            result.inicio = aux.inicio;
                            result.longitud = aux.longitud;
                        }
                        aux.inicio = i;
                        aux.longitud = 1;
                    }
                }
            } else if(lista.Length == 1) {
                result.inicio = 0;
                result.longitud = 1;
            }
            return result;
        }

        public static void MostrarResultado(string víctima, Secuencia resultado) {
            Console.WriteLine("Víctima:   {0}", víctima);
            if(resultado.inicio != -1) {
                Console.WriteLine("Secuencia: {0}, {1}, {2}", resultado.inicio, resultado.longitud,
                                  víctima.Substring(resultado.inicio, resultado.longitud));
            } else {
                Console.WriteLine("Secuencia: {0}, {1}", resultado.inicio, resultado.longitud);
            }
        }

        public static void Resolver() {
            string víctima = "as222df1111as333df";
            Secuencia resultado = SubsecMayorIterativo(víctima);
            MostrarResultado(víctima, resultado);

            Console.WriteLine();
            resultado = SubsecuenciaMayor(víctima);
            MostrarResultado(víctima, resultado);

            Console.WriteLine();
            víctima = "as2222df888888df";
            resultado = SubsecuenciaMayor(víctima);
            MostrarResultado(víctima, resultado);
        }
    }
}

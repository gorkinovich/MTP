using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    public static class Reinas {
        const int N = 8;

        static bool Comprobar(int j, int[] lista) {
            int k = 1;
            for(int i = j - 1; i >= 0; i--, k++) {
                if(lista[j] == lista[i] || lista[j] == lista[i] - k ||
                   lista[j] == lista[i] + k) {
                    return false;
                }
            }
            return true;
        }

        static bool Calcular(int j, int[] lista) {
            if(j < N) {
                for(int i = 0; i < N; i++) {
                    lista[j] = i;
                    if(Comprobar(j, lista) && Calcular(j + 1, lista)) {
                        return true;
                    }
                }
                return false;
            } else {
                return true;
            }
        }

        public static void Resolver() {
            int[] lista = new int[N];

            for(int i = 0; i < N; i++) {
                lista[i] = 0;
            }

            if(Calcular(0, lista)) {
                Console.WriteLine("Solución:");
                for(int i = 0; i < N; i++) {
                    for(int j = 0; j < N; j++) {
                        if(lista[j] == i) {
                            Console.Write("X ");
                        } else {
                            Console.Write("· ");
                        }
                    }
                    Console.WriteLine();
                }
            } else {
                Console.WriteLine("No hay solución...");
            }
        }
    }
}

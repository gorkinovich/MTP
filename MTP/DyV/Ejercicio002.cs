using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Ejercicio 2: Diseña un algoritmo para calcular x^n, n ϵ N, con un coste O(log n) en
     * términos del número de multiplicaciones. Para simplificar el problema, considera el caso
     * en el que el resultado de la operación puede representarse en una variable de coma flotante.
     */
    public static class Ejercicio002 {
        public static float ElevarIterativo(float x, int n) {
            if(n < 0) {
                return 1 / ElevarIterativo(x, -n);
            } else if(n == 0) {
                return 1;
            } else {
                float result = x;
                for(int i = 1; i < n; i++) {
                    result *= x;
                }
                return result;
            }
        }

        public static float ElevarRecursivo(float x, int n) {
            if(n < 0) {
                return 1 / ElevarRecursivo(x, -n);
            } else if(n == 0) {
                return 1;
            } else {
                if(n == 1) {
                    return x;
                } else {
                    float aux = ElevarRecursivo(x, n / 2);
                    if(n % 2 == 0) {
                        return aux * aux;
                    } else {
                        return aux * aux * x;
                    }
                }
            }
        }

        public static void Resolver() {
            for(int i = -2; i < 10; i++) {
                Console.WriteLine("{0}\t{1}", ElevarIterativo(2.0f, i), ElevarRecursivo(2.0f, i));
            }
        }
    }
}

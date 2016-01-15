using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Un montículo ascendente es un árbol binario en el que cada nodo es mayor que sus hijos (si
     * existen). Consideremos un montículo de m nodos con valores positivos implementado mediante
     * un vector de la siguiente manera. Dado un nodo que se encuentra en la posición i del vector:
     * + El hijo izquierdo (si existe) está en la posición 2i.
     * + El hijo derecho (si existe) está en la posición 2i + 1.
     * + Si la posición i corresponde a un hijo inexistente, esta posición contiene el valor 0.
     * 
     * Diseña una función utilizando la técnica Divide y Vencerás que determine si un árbol binario
     * implementado mediante un array de P elementos contiene un montículo ascendente.
     */
    public static class Ejercicio007 {
        public static int Valor(int[] lista, int i) {
            return lista[i - 1];
        }

        public static int Izquierdo(int i) {
            return i * 2;
        }

        public static int Derecho(int i) {
            return i * 2 + 1;
        }

        private static bool EsVacio(int[] lista, int i) {
            if(i <= lista.Length) {
                if(Valor(lista, i) != 0) {
                    return false;
                } else {
                    if(EsVacio(lista, Izquierdo(i))) {
                        return EsVacio(lista, Derecho(i));
                    } else {
                        return false;
                    }
                }
            } else {
                return true;
            }
        }

        private static bool EsAscendente(int[] lista, int padre, int i) {
            if(i <= lista.Length) {
                int val = Valor(lista, i);
                if(val != 0) {
                    if(padre > val) {
                        if(EsAscendente(lista, val, Izquierdo(i))) {
                            return EsAscendente(lista, val, Derecho(i));
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                } else {
                    if(EsVacio(lista, Izquierdo(i))) {
                        return EsVacio(lista, Derecho(i));
                    } else {
                        return false;
                    }
                }
            } else {
                return true;
            }
        }

        public static bool EsAscendente(int[] lista) {
            if(lista.Length > 0) {
                int val = Valor(lista, 1);
                bool izq = EsAscendente(lista, val, Izquierdo(1));
                bool der = EsAscendente(lista, val, Derecho(1));
                return izq && der;
            } else {
                return false;
            }
        }

        public static void Resolver(int[] lista) {
            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Es ascendente: {0}\n", EsAscendente(lista));
        }

        public static void Resolver() {
            int[] lista = { 8, 3, 4, 5, 2, 0, 0, 1, 0, 7, 7, 0, 0, 0, 0, 2, 3 };
            Resolver(lista);

            int[] lista2 = { 42, 20, 40, 18, 19, 35, 34, 15, 14, 18, 15, 0, 0, 31, 32, 0, 0, 0, 0, 8, 8, 1, 1 };
            Resolver(lista2);
        }
    }
}

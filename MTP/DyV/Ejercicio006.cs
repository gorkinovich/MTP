using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Se dispone de un vector de secuencias de vídeo video[1..N] en el que cada elemento contiene
     * dos atributos: duración (en segundos) y secuencia (contiene el fragmento de vídeo). Se quiere
     * ordenar este vector en orden creciente de duración.
     * 
     * Sin embargo, no es posible utilizar directamente uno de los algoritmos de ordenación conocidos,
     * ya que las secuencias de vídeo almacenadas en el atributo secuencia ocupan gran cantidad de
     * memoria y se quiere evitar mover estos datos de forma innecesaria durante el proceso de ordenación.
     * 
     * Diseña un algoritmo basado en la técnica Divide y Vencerás que proporcione un vector ordenado
     * de secuencias de vídeo, de forma que realice una sola copia de los datos contenidos en el
     * atributo secuencia de cada elemento del vector de origen video[1..N] al vector ordenado.
     */
    public static class Ejercicio006 {
        #region Tipos de datos
        public struct Video {
            public int duración;
            public byte[] secuencia;

            public override string ToString() {
                return duración.ToString();
            }
        }

        public struct Nodo {
            public Nodo(int o, int d) {
                this.origen = o;
                this.duración = d;
            }

            public int origen;
            public int duración;

            public override string ToString() {
                return "(" + origen.ToString() + " " + duración.ToString() + ")";
            }
        }
        #endregion


        #region Ordenación de la lista
        private static void Combinar(Nodo[] lista, int inf, int sup, int mitad) {
            Nodo[] result = new Nodo[lista.Length];
            int i = inf, j = mitad + 1, k = inf;

            while(i <= mitad && j <= sup) {
                if(lista[i].duración <= lista[j].duración) {
                    result[k] = lista[i]; i++;
                } else {
                    result[k] = lista[j]; j++;
                }
                k++;
            }

            while(i <= mitad) {
                result[k] = lista[i]; i++; k++;
            }

            while(j <= sup) {
                result[k] = lista[j]; j++; k++;
            }

            for(k = inf; k <= sup; k++) {
                lista[k] = result[k];
            }
        }

        private static void Ordenar(Nodo[] lista, int inf, int sup) {
            if(inf < sup) {
                int mitad = (inf + sup) / 2;
                Ordenar(lista, inf, mitad);
                Ordenar(lista, mitad + 1, sup);
                Combinar(lista, inf, sup, mitad);
            }
        }

        public static void Ordenar(Nodo[] lista) {
            if(lista.Length > 0) {
                Ordenar(lista, 0, lista.Length - 1);
            }
        }
        #endregion


        #region Ordenación del vector de secuencias
        private static int Siguiente(bool[] ordenado) {
            for(int i = 0; i < ordenado.Length; i++) {
                if(!ordenado[i]) return i;
            }
            return -1;
        }

        public static void Mover(Video[] video, Nodo[] lista) {
            int act = 0, prev = -1, mov = 1;
            Video aux = new Video();
            bool[] ordenado = lista.Select(x => false).ToArray();

            Console.WriteLine();
            while(mov < lista.Length) {
                Console.WriteLine(act.ToString() + " " + prev.ToString());
                if(lista[act].origen == act) {
                    ordenado[act] = true;
                    act = Siguiente(ordenado);
                } else {
                    if(prev == -1) {
                        aux = video[act];
                        video[act] = video[lista[act].origen];
                        ordenado[act] = true;
                        prev = act;
                        act = lista[act].origen;
                    } else if(lista[act].origen != prev) {
                        video[act] = video[lista[act].origen];
                        ordenado[act] = true;
                        act = lista[act].origen;

                    } else {
                        video[act] = aux;
                        ordenado[act] = true;
                        prev = -1;
                        act = Siguiente(ordenado);
                    }
                }
                mov++;
                Console.WriteLine(act.ToString() + " " + prev.ToString());
                Console.WriteLine("Video: " + video.Select(x => x.ToString())
                                                   .Aggregate((x, xs) => x + " " + xs));
            }

            Console.WriteLine(ordenado.Select(x => x.ToString()).Aggregate((x, xs) => x + " " + xs));

            if(prev != -1) {
                video[act] = aux;
            }
        }
        #endregion

        #region Copia ordenada del vector de secuencias
        public static Video[] Copiar(Video[] video, Nodo[] lista) {
            Video[] aux = new Video[video.Length];
            for(int i = 0; i < video.Length; i++) {
                aux[i].duración = video[lista[i].origen].duración;
                aux[i].secuencia = (byte[])video[lista[i].origen].secuencia.Clone();
            }
            return aux;
        }
        #endregion


        public static void Resolver() {
            Video[] video = InitDatos();
            Nodo[] lista = Preparar(video);

            Console.WriteLine("Video: " + video.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            
            Ordenar(lista);
            Mover(video, lista);
            //video = Copiar(video, lista);
            Console.WriteLine();

            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Video: " + video.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
        }


        #region Inicialización de datos
        public static Nodo[] Preparar(Video[] video) {
            int origen = 0;
            return video.Select(x => new Nodo(origen++, x.duración)).ToArray();
        }

        public static Video[] InitDatos() {
            Video[] lista = new Video[10];

            lista[0].duración = 8;
            lista[0].secuencia = new byte[] { 1, 3, 2, 4, 5, 6, 7, 8 };

            lista[1].duración = 10;
            lista[1].secuencia = new byte[] { 1, 2, 3, 5, 4, 6, 7, 8 };

            lista[2].duración = 6;
            lista[2].secuencia = new byte[] { 1, 2, 3, 4, 5, 7, 6, 8 };

            lista[3].duración = 4;
            lista[3].secuencia = new byte[] { 8, 2, 3, 4, 5, 6, 7, 1 };

            lista[4].duración = 7;
            lista[4].secuencia = new byte[] { 1, 6, 3, 4, 5, 2, 7, 8 };

            lista[5].duración = 1;
            lista[5].secuencia = new byte[] { 1, 2, 7, 4, 5, 6, 3, 8 };

            lista[6].duración = 9;
            lista[6].secuencia = new byte[] { 1, 2, 3, 8, 5, 6, 7, 4 };

            lista[7].duración = 3;
            lista[7].secuencia = new byte[] { 5, 2, 3, 4, 1, 6, 7, 8 };

            lista[8].duración = 5;
            lista[8].secuencia = new byte[] { 1, 2, 5, 4, 3, 6, 7, 8 };

            lista[9].duración = 2;
            lista[9].secuencia = new byte[] { 1, 2, 3, 6, 5, 4, 7, 8 };

            return lista;
        }
        #endregion
    }
}

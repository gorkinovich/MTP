using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    public static class Mochila02 {
        const int I = int.MaxValue; // Infinito

        struct Objeto {
            public int valor;
            public int espacio;

            public Objeto(int v, int t) {
                this.valor = v;
                this.espacio = t;
            }

            public override string ToString() {
                return "(" + this.valor + "; " + this.espacio + ")";
            }
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }

        static int[,] Calcular2(Objeto[] lista, int límite) {
            int MaxElem = lista.Length + 1;
            int MaxCant = límite + 1;
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
                    if(lista[i - 1].espacio <= j) {
                        a = tabla[i - 1, j];
                        b = tabla[i - 1, j - lista[i - 1].espacio] + lista[i - 1].valor;
                        tabla[i, j] = Max(a, b);
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                    }
                }
            }

            return tabla;
        }

        static void Mostrar(int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("IN ");
                    } else {
                        Console.Write(tabla[i, j].ToString("00") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static Objeto[] Transformar(Objeto[] lista, int c, int[,] tabla) {
            int MaxElem = lista.Length + 1;
            int MaxCant = c + 1;
            List<Objeto> r = new List<Objeto>();

            Mostrar(tabla, MaxElem, MaxCant);

            int i = MaxElem - 1;
            int j = MaxCant - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(j >= lista[i - 1].espacio &&
                          tabla[i - 1, j - lista[i - 1].espacio] ==
                          tabla[i, j] - lista[i - 1].valor) {
                    r.Add(lista[i - 1]);
                    j -= lista[i - 1].espacio;
                    i--;
                } else if(j < lista[i - 1].espacio) {
                    j = 0;
                }
            }

            return r.ToArray();
        }

        static Objeto[] Calcular(Objeto[] lista, int límite) {
            int[,] tabla = Calcular2(lista, límite);
            return Transformar(lista, límite, tabla);
        }

        static void Resolver(Objeto[] lista, int límite) {
            Objeto[] result = Calcular(lista, límite);
            Console.WriteLine("Resultado: " +
                              result.Select(x => x.ToString())
                                    .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Suma: " + result.Select(x => x.valor)
                                               .Sum());
            Console.WriteLine();
        }

        public static void Resolver() {
            Objeto[] lista =
            {
                new Objeto(1, 4), new Objeto(1, 4),
                new Objeto(2, 4), new Objeto(10, 8),
                new Objeto(12, 8), new Objeto(12, 8),
                new Objeto(10, 10), new Objeto(14, 10),
                new Objeto(14, 10), new Objeto(10, 15)
            };
            Resolver(lista, 18);
            Resolver(lista, 20);
            Resolver(lista, 42);
        }
    }

    public static class Mochila01 {
        const int I = int.MaxValue; // Infinito

        struct Objeto {
            public int valor;
            public int espacio;

            public Objeto(int v, int t) {
                this.valor = v;
                this.espacio = t;
            }

            public override string ToString() {
                return "(" + this.valor + "; " + this.espacio + ")";
            }
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }

        static int[,] Calcular2(Objeto[] lista, int límite) {
            int MaxElem = lista.Length + 1;
            int MaxCant = límite + 1;
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
                    if(lista[i - 1].espacio <= j) {
                        a = tabla[i - 1, j];
                        b = tabla[i, j - lista[i - 1].espacio] +
                            lista[i - 1].valor;
                        tabla[i, j] = Max(a, b);
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                    }
                }
            }

            return tabla;
        }

        static void Mostrar(int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("IN ");
                    } else {
                        Console.Write(tabla[i, j].ToString("00") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static Objeto[] Transformar(Objeto[] lista, int c, int[,] tabla) {
            int MaxElem = lista.Length + 1;
            int MaxCant = c + 1;
            List<Objeto> r = new List<Objeto>();

            Mostrar(tabla, MaxElem, MaxCant);

            int i = MaxElem - 1;
            int j = MaxCant - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(j >= lista[i - 1].espacio &&
                          tabla[i, j - lista[i - 1].espacio] ==
                          tabla[i, j] - lista[i - 1].valor) {
                    j -= lista[i - 1].espacio;
                    r.Add(lista[i - 1]);
                } else if(j < lista[i - 1].espacio) {
                    j = 0;
                }
            }

            return r.ToArray();
        }

        static Objeto[] Calcular(Objeto[] lista, int límite) {
            int[,] tabla = Calcular2(lista, límite);
            return Transformar(lista, límite, tabla);
        }

        static void Resolver(Objeto[] lista, int límite) {
            Objeto[] result = Calcular(lista, límite);
            Console.WriteLine("Resultado: " +
                              result.Select(x => x.ToString())
                                    .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Suma: " + result.Select(x => x.valor)
                                               .Sum());
            Console.WriteLine();
        }

        public static void Resolver() {
            Objeto[] lista =
            {
                new Objeto(1, 4), new Objeto(2, 4),
                new Objeto(10, 8), new Objeto(12, 8),
                new Objeto(10, 10), new Objeto(14, 10)
            };
            Resolver(lista, 20);
            Resolver(lista, 42);
        }
    }
}

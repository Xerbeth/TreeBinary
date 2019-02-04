using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeBinary
{
    /// <summary>
    /// Clase para Arbol Binario 
    /// </summary>
    class TreeBinary
    {

        /// <summary>
        /// Clase Nodo que contiene las propiedades de los nodos del arbol binario
        /// </summary>
        class Nodo
        {
            /// <summary>
            /// Informaciòn que contiene el nodo
            /// </summary>
            public int data;
            /// <summary>
            /// Apuntadores del nodo hacia derecha e izquierda
            /// </summary>
            public Nodo right, left;
        }
        /// <summary>
        /// Nodo raiz del arbol
        /// </summary>
        private Nodo root;
        /// <summary>
        /// Lista para guardar el recorrido al buscar nodos
        /// </summary>
        private List<int> wayOne, wayTwo;

        /// <summary>
        /// Mètodo constructor
        /// </summary>
        public TreeBinary()
        {
            root = null;
            wayOne = new List<int>();
            wayTwo = new List<int>();
        }

        /// <summary>
        /// Mètodo para insertar nodos en el arbol binario 
        /// </summary>
        /// <param name="data"> Valor del nodo a insertar </param>
        public void InsertNodo(int data)
        {
            if (!ExistNodoData(data))
            {
                Nodo nuevo;
                nuevo = new Nodo();
                nuevo.data = data;
                nuevo.left = null;
                nuevo.right = null;
                if (root == null)
                {
                    root = nuevo;
                }
                else
                {
                    Nodo anterior = null, aux;
                    aux = root;
                    while (aux != null)
                    {
                        anterior = aux;
                        if (data < aux.data)
                            aux = aux.left;
                        else
                            aux = aux.right;
                    }
                    if (data < anterior.data)
                    {
                        anterior.left = nuevo;
                    }
                    else
                    {
                        anterior.right = nuevo;
                    }
                }
            }
            else
            {
                Console.WriteLine("El dato: " + data + " ya existe. No fue posible insertarlo en el arbol.");
            }
        }

        /// <summary>
        /// Método para validar si el dato ya existe en el arbol
        /// </summary>
        /// <param name="data"> Valor del nodo a insertar </param>
        /// <returns> bandera de true o false si existe o no el valor </returns>
        public bool ExistNodoData(int data)
        {
            Nodo aux = root;
            while (aux != null)
            {
                if (data == aux.data)
                    return true;
                else
                    if (data > aux.data)
                    aux = aux.right;
                else
                    aux = aux.left;
            }
            return false;
        }

        /// <summary>
        /// Mètodo privado y recursivo para imprimir el arbol
        /// </summary>
        /// <param name="aux"> Nodo raiz </param>
        private void PrintNodos(Nodo aux)
        {
            if (aux != null)
            {
                PrintNodos(aux.left);
                Console.Write(aux.data + " ");
                PrintNodos(aux.right);
            }
        }

        /// <summary>
        /// Método para acceder al método privado para imprimir los nodos del arbol
        /// </summary>
        public void PrintNodos()
        {
            PrintNodos(root);
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodo"></param>
        /// <param name="value"></param>
        /// <param name="way"></param>
        private void ExistsNodos(Nodo nodo, int value, List<int> way)
        {
            // Valido que exista raiz
            if (nodo != null)
            {
                //Console.WriteLine("camino recorrido: "+ nodo.info);
                // valor es igual al valor del nodo
                if (value == nodo.data)
                {
                    way.Add(nodo.data);
                    // Encontre el nodo que se esta buscando
                    //Console.WriteLine("Dato " + nodo.info + " encontrado");
                }
                // Valor a buscar es menor al valor del nodo
                else if (value < nodo.data)
                {
                    way.Add(nodo.data);
                    // Buscar por la izquierda
                    ExistsNodos(nodo.left, value, way);
                }
                else
                {
                    way.Add(nodo.data);
                    // Buscar por la dereca
                    ExistsNodos(nodo.right, value, way);
                }
            }
            else
            {
                Console.WriteLine("Dato " + value + " no encontrado en el arbol");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="way"></param>
        private void ExistsNodo(int value, List<int> way)
        {
            ExistsNodos(root, value, way);
        }

        /// <summary>
        /// Método pùblico para recibir los dos valores a buscar el primer nodo en comùn 
        /// </summary>
        /// <param name="a"> Valor "a" para buscar </param>
        /// <param name="b"> Valor "b" para buscar </param>
        /// <returns> Valor del primer nodo padre en común </returns>
        public int ParentNode(int a, int b)
        {
            ExistsNodo(a, wayOne);
            ExistsNodo(b, wayTwo);
            return Parent();
        }

        /// <summary>
        /// Elimina los todos los elementos contenidos en las listas 
        /// </summary>
        private void ClearLists()
        {
            wayOne.Clear();
            wayTwo.Clear();
        }

        /// <summary>
        /// Método para obtener el primer valor del nodo padre en comùn de las listas cargadas
        /// </summary>
        /// <returns> Valor del primer nodo padre en común </returns>
        public int Parent()
        {
            int aux = 0;
            for (int lTwo = (int)wayTwo.LongCount() - 1; lTwo >= 0; lTwo--)
            {
                for (int lOne = (int)wayOne.LongCount() - 1; lOne >= 0; lOne--)
                {
                    if (wayTwo[lTwo] == wayOne[lOne])
                    {
                        aux = wayTwo[lTwo];
                        ClearLists();
                        return aux;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Mètodo principal 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TreeBinary tb = new TreeBinary();

            /* Generar arbol mediante array */
            int[] numbers = new int[] { 70, 84, 85, 78, 80, 76, 49, 54, 51, 37, 40, 22 };
            for (int l = 0; (l < numbers.Length); l++)
            {
                tb.InsertNodo(numbers[l]);
            }

            Console.WriteLine("Ancentro(" + 40 + "," + 78 + ") = " + tb.ParentNode(40, 78));
            Console.WriteLine("Ancentro(" + 51 + "," + 37 + ") = " + tb.ParentNode(51, 37));
            Console.WriteLine("Ancentro(" + 76 + "," + 85 + ") = " + tb.ParentNode(76, 85));

            Console.ReadKey();
        }

    }
}

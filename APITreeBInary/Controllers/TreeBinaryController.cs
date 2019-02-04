using APITreeBInary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APITreeBInary.Controllers
{
    public class TreeBinaryController : ApiController
    {
        /// <summary>
        /// Objeto de la clase Nodo
        /// </summary>
        public Nodo Nodo;

        /// <summary>
        /// Lista para guardar el recorrido al buscar nodos
        /// </summary>
        private List<int> wayOne, wayTwo;

        /// <summary>
        /// Nodo raiz del arbol
        /// </summary>
        private Nodo root;

        /// <summary>
        /// Método para validar si el dato ya existe en el arbol
        /// </summary>
        /// <param name="data"> Valor del nodo a insertar </param>
        /// <returns> bandera de true o false si existe o no el valor </returns>
        private bool ExistNodoData(int data)
        {

            Nodo aux = Nodo.root;

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
        /// Mètodo para insertar nodos en el arbol binario 
        /// </summary>
        /// <param name="data"> Valor del nodo a insertar </param>
        private void InsertNodo(int data)
        {
            
            if (!ExistNodoData(data))
            {
                Nodo nuevo = new Nodo();
                nuevo.data = data;
                nuevo.left = null;
                nuevo.right = null;
                if (Nodo.root == null)
                {
                    Nodo.root = nuevo;
                }
                else
                {
                    Nodo anterior = null, aux;
                    aux = Nodo.root;
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
        /// Método para buscar el valor del nodo y guardar el recorrido 
        /// </summary>
        /// <param name="nodo"> Nodo actual </param>
        /// <param name="value"> Valor a validar </param>
        /// <param name="way"> Lista con el camino de los nodos  </param>
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
        /// Mètodo para llamar acceder al Metodo ExistNodos, agregando como paramertro el nodo raiz
        /// </summary>
        /// <param name="value"> Valor a buscar </param>
        /// <param name="way"> Lista para guardar el camino al recorrer el arbol </param>
        private void ExistsNodo(int value, List<int> way)
        {
            ExistsNodos(root, value, way);
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
        private int Parent()
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
        /// Método pùblico para recibir los dos valores a buscar el primer nodo en comùn 
        /// </summary>
        /// <param name="a"> Valor "a" para buscar </param>
        /// <param name="b"> Valor "b" para buscar </param>
        /// <returns> Valor del primer nodo padre en común </returns>
        private int ParentNode(int a, int b)
        {
            ExistsNodo(a, wayOne);
            ExistsNodo(b, wayTwo);
            return Parent();
        }

        /// <summary>
        /// Mètodo para crear el arbol binario
        /// </summary>
        /// <param name="treeBinary"> Json con los valores para construir el arbol binario </param>
        /// <returns> Estado y mensaje de la petición </returns>
        [HttpPost]
        public IHttpActionResult CreateTreeBinary(TreeBinary treeBinary)
        {

            Nodo = new Nodo();
            int data = 0;
            foreach (string number in treeBinary.treeBinary)
            {
                // Valida la conversión de los datos del Json 
                // Si la conversión falla, se cierra el proceso y se envia un mensaje de error Status 400
                if (!Int32.TryParse(number, out data))
                {
                    return BadRequest("Ocurrió un error al momento de contruir el arbol, favor validar la información enviada.");
                }
                InsertNodo(data);

            }
            // Mensaje de Status 200; solicitud efectuada correctamente!
            return Ok("Arbol construido correctamente!.");
        }

        /// <summary>
        /// Mètodo para crear arbol y consultar nodo padre 
        /// </summary>
        /// <param name="treeBinaryNodos">  </param>
        /// <returns> Estado y mensaje de la petición </returns>
        [HttpPost]
        public IHttpActionResult ParentNode(TreeBinaryNodos treeBinaryNodos)
        {

            Nodo = new Nodo();            
            int data = 0;
            foreach (string number in treeBinaryNodos.treeBinary)
            {
                // Valida la conversión de los datos del Json 
                // Si la conversión falla, se cierra el proceso y se envia un mensaje de error Status 400
                if (!Int32.TryParse(number, out data))
                {
                    return BadRequest("Ocurrió un error al momento de contruir el arbol, favor validar la informaciòn enviada.");
                }
                InsertNodo(data);

            }

            // Inicializo las variables necesarias para obtener el nodo padre comùn 
            wayOne = new List<int>();
            wayTwo = new List<int>();
            root = Nodo.root;

            if (!Int32.TryParse(treeBinaryNodos.NodoOne, out data))
            {
                return BadRequest("Ocurrió un error al momento validar el valor del nodo uno.");
            }
            int NodoUno = data;

            if (!Int32.TryParse(treeBinaryNodos.NodoTwo, out data))
            {
                return BadRequest("Ocurrió un error al momento validar el valor del nodo dos.");
            }
            int NodoDos = data;

            // Mensaje de Status 200; solicitud efectuada correctamente!
            return Ok("Ancentro comùn más cercano: "+ ParentNode(NodoUno, NodoDos));
        }

    }
}
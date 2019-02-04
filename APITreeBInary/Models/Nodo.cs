using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITreeBInary.Models
{
    /// <summary>
    /// Clase modelo del nodo del arbol
    /// </summary>
    public class Nodo
    {
        /// <summary>
        /// Informaciòn que contiene el nodo
        /// </summary>
        public int data;
        /// <summary>
        /// Apuntadores del nodo hacia derecha e izquierda
        /// </summary>
        public Nodo right, left;

        /// <summary>
        /// Nodo raiz del arbol
        /// </summary>
        public Nodo root;

        public Nodo()
        {
            data = 0;
            right = null;
            left = null;
            root = null;

        }
        
    }

    /// <summary>
    /// Clase modelo del para el json para construir el arbol
    /// </summary>
    public class TreeBinary
    {
        public List<string> treeBinary { get; set; }
    }
}
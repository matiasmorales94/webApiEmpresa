using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiEmpresa
{
    public class Compania
    {

        // Declaracion de variables

        private Int32 id;
        private string nombre, url;


        public Compania() {

            this.id = 0;
            this.nombre = "";
            this.url = "";

        }

        public Compania(int id, string nombre, string url) {

            this.id = id;
            this.nombre = nombre;
            this.url = url;

        }

        public int Id {

            get
            {
                return id;
            }

            set {

                id = value;

            }
        }

        public string Nombre {

            get
            {

                return nombre;
            }


            set
            {

                nombre = value;

            }
        }


        public string Url
            {


            get{

                return url;

                     }

            set {

                url = value;
            }

             }

    }
}
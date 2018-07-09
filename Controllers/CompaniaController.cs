using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApiEmpresa.Models;

namespace webApiEmpresa.Controllers
{
    public class CompaniaController : ApiController

    {

        // Crear instancias

        private DataBase db;


        // metodo constructor

        public CompaniaController() {

            db = new DataBase();

        }

        // GET: api/Compania
        [HttpGet]
        public HttpResponseMessage Get()
        {


            List<Compania> listCompania = db.allCompania();


            // se valida si existen empresas en la base de datos 

            if (listCompania.Count() > 0)
            {

                return Request.CreateResponse(listCompania); // se retorna el objeto

            } // fin del IF

            else {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Hay Compañias registradas en el Sistema");
            } // fin del else



        } // fin del metodo

        // GET: api/Compania/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Compania res = db.getCompania(id);

            // Validar si la compañia Existe

            if (res.Id != 0)
            {

                return Request.CreateResponse(res);


            }// fin if

            else {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalido id");
            } // fin else

        }// fin metodo

        // POST: api/Compania
        [HttpPost]
        public HttpResponseMessage Post(string nombre, string url)
        {

            // se valida que los parametros no esten vacios

            if (nombre.Length > 0 && url.Length > 0)
            {

                return this.getMessage(db.postCompania(new Compania(0, nombre, url)));

            }//fin del if


            else {

                return this.getMessage(1);

            } // fin else

        } // fin del Metodo

        // PUT: api/Compania/5
        [HttpPut]
        public HttpResponseMessage Put(int id,string nombre, string url)
        {

            // valida que los parametros no esten vacios

            if (nombre.Length > 0 && url.Length > 0)
            {

                return this.getMessage(db.putCompania(new Compania(id, nombre, url)));
            } // Fin del if

            else {

                return this.getMessage(1);

            } //fin del else 
        }// fin del metodo


        // DELETE: api/Compania/5
        public HttpResponseMessage Delete(int id)
        {

            return this.getMessage(db.deleteCompany(id));
        }// fin del metodo

        public HttpResponseMessage getMessage(Int32 id) {

            switch (id) {

                case 0: return Request.CreateResponse("Ejecutado Correctamente!");
                case 1: return Request.CreateErrorResponse(HttpStatusCode.Conflict,"Tu no has especificado toda la información de la compañia");
                case 3: return Request.CreateErrorResponse(HttpStatusCode.Conflict,"La compañia no existe");
                default: return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalido ID");
            } // fin del switch

        }// fin del metodo
    }
}

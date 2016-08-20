using JacksonApi.Interfaces;
using JacksonApi.Models;
using JacksonApi.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JacksonApi.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActController : ApiController
    {

        IActAdapter _myActAdapter;

        ActController(IActAdapter newActAdapter)
        {
            _myActAdapter = newActAdapter;
        }

        // GET: api/Acts
        [HttpGet]
        [Route("Acts")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myActAdapter.GetActs()); 
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        // GET: api/Acts/(guid)
        [HttpGet]
        [Route("Acts/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.Unused);
        }

        // POST: api/Acts
        [HttpPost]
        [Route("Acts")]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]Act value)
        {    
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myActAdapter.UpsertAct(value)); 
            }
            catch (Exception ex)
            {
                // Convert the exception to an Http Exception. This will need to be cleaned up. 
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }  
        }

        // PUT: api/Acts/5
        [Route("Acts/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse(HttpStatusCode.Unused);
        }

        // DELETE: api/Acts
        [HttpDelete]
        [Route("Acts/{id}")]
        // TODO: convert this to take a guid or maybe an act number? 
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    _myActAdapter.DeleteAct(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                // Convert the exception to an Http Exception. This will need to be cleaned up. 
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

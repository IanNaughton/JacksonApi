using AutoMapper;
using JacksonApi.Interfaces;
using JacksonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JacksonApi.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VolumeController : ApiController
    {

        private IVolumeAdapter _myVolumeAdapter;

        public VolumeController(IVolumeAdapter newVolumeAdapter)
        {
            _myVolumeAdapter = newVolumeAdapter;
        }

        // GET: api/Volumes
        [Route("Volumes")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeAdapter.GetAllVolumes());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Volume/{id}
        [Route("Volumes/Id/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeAdapter.GetVolumeById(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Volume/Act/{act}
        [Route("Acts/{act:int}/Volumes")]
        public HttpResponseMessage Get(int act)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeAdapter.GetVolumesByAct(act));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Acts/{act}/Volume/{volume}
        [Route("Acts/{act:int}/Volumes/{volume:int}")]
        public HttpResponseMessage Get(int act, int volume)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeAdapter.GetVolumeByActAndVolume(act, volume));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // POST: api/Acts/{act number}/Volumes
        [HttpPost]
        [Route("Acts/{act:int}/Volumes")]
        public HttpResponseMessage Post([FromBody]Volume newVolume)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeAdapter.UpsertVolume(newVolume));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } 
            
        }

        // PUT: api/Volume/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse(HttpStatusCode.Unused);
        }

        // DELETE: api/Acts/{act number}/Volume/{volume number}
        [HttpDelete]
        [Route("Acts/{act:int}/Volumes/{volume:int}")]
        public HttpResponseMessage Delete(int act, int volume)
        {
            try
            {
                _myVolumeAdapter.DeleteVolume(act, volume);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } 
        }
    }
}

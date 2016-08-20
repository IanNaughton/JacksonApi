using JacksonApi.Interfaces;
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
    public class VolumeTextController : ApiController
    {

        private IVolumeTextAdapter _myVolumeTextAdapter;
       
        public VolumeTextController(IVolumeTextAdapter newVolumeTextAdapter)
        {
            _myVolumeTextAdapter = newVolumeTextAdapter;
        }

        // GET: api/VolumeText/{id}
        [Route("VolumeText/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeTextAdapter.GetVolumeTextByVolumeId(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Acts/{act}/Volume/{volume}
        [Route("Acts/{act:int}/Volumes/{volume:int}/Text")]
        public HttpResponseMessage Get(int act, int volume)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeTextAdapter.GetVolumeTextByActAndVolumeNumber(act, volume));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST: api/VolumeText
        [Route("Acts/{act:int}/Volumes/{volume:int}/Text")]
        public HttpResponseMessage Post(int act, int volume, [FromBody]Models.VolumeText volumeText)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _myVolumeTextAdapter.UpsertVolumeText(act, volume, volumeText));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT: api/VolumeText/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse(HttpStatusCode.Unused);
        }

        // DELETE: api/VolumeText/5
        [HttpDelete]
        [Route("Acts/{act:int}/Volumes/{volume:int}/Text")]
        public HttpResponseMessage Delete(int act, int volume)
        {
            try
            {
                _myVolumeTextAdapter.DeleteVolumeText(act, volume);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

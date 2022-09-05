using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/CovenantMaster")]
    public class CovenantMasterController : ApiController
    {
        private ICovenantMasterRecord _repository;
        public CovenantMasterController(ICovenantMasterRecord repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("getCovenantRatio")]
        [HttpGet]
        public HttpResponseMessage GetCovenantRatio(string covenantType)
        {
            var entry = _repository.GetCovenantRatio(covenantType);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Covenant Ratio Records of covenant type : " + covenantType.ToString() + " not found");
            }
        }
    }
}

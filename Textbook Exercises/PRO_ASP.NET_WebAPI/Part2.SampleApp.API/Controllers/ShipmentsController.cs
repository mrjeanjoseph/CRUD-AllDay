using PingYourPackage.ApiModel;
using PingYourPackage.Domain;
using System.Web.Http;
using System.Linq;

namespace PingYourPackage.API.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class ShipmentsController : ApiController
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentsController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public PaginatedDto<ShipmentDto> GetShipments(PaginatedRequestCommand requestCommand)
        {
            var shipments = _shipmentService.GetShipments(requestCommand.Page, requestCommand.Take);

            return shipments.ToPaginatedDto(shipments.Select(sh => sh.ToShipmentDto()));
        }
    }
}

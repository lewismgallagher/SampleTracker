using DAL.Data;
using DAL.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;

namespace SampleTrackerWebAPI.RackConfiguration
{
    public static class RackConfigurationEndpoints
    {
        public static async Task<IResult> GetRack(RackConfigurationService service, int id)
        {
            return TypedResults.Ok(await service.GetRack(id));
        }
        public static async Task<IResult> GetRacks(RackConfigurationService service)
        {
            return TypedResults.Ok( await service.GetRacks());
        }

        //todo return newly saved object with new id.
        public static async Task<IResult> CreateRack(RackConfigurationService service, [FromBody] RackConfigurationDTO rack)
        {
            var result = await service.CreateAsync(rack);

                return TypedResults.CreatedAtRoute(
                routeName: "GetRack",
                routeValues: new { id = rack.Id },
                value: rack);

            //else return TypedResults.UnprocessableEntity();
        }

        public static async Task<IResult> UpdateRack(RackConfigurationService service,[FromBody] RackConfigurationDTO rack)
        {
            if(rack.Id == 0) { return TypedResults.UnprocessableEntity(); }

            var result = await service.Updateasync(rack);

            return TypedResults.Ok();
        }

        //TODO make delete return what object was deleted
        public static async Task<IResult> DeleteRack(RackConfigurationService service, int id)
        {
            if (id == 0) {  }

            var result = await service.DeleteRack(id);

            if (result) {  return TypedResults.Ok(); }
            else
            {
                return TypedResults.UnprocessableEntity();
            }
        }

    }
}

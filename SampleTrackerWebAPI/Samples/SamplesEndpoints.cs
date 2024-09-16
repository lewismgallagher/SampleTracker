using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DTOs;

namespace SampleTrackerWebAPI.Samples
{
    public static class SamplesEndpoints
    {
        public static async Task<IResult> GetRacks (SampleRackService service ,int? id, string? name)
        {
            return TypedResults.Ok(await service.SearchRacks(id,name));
        }
        public static async Task<IResult> CheckSampleExists(SampleRackService service, string IdentifyingValue)
        {
            // returns boolean
            return TypedResults.Ok(await service.CheckSampleExists(IdentifyingValue));
        }

        public static async Task<IResult> GetSampleIdByIdentifyingValue(SampleRackService service, string Identifyingvalue)
        {
            // returns sampleid
            return TypedResults.Ok(await service.GetSampleByIdentifyingValue(Identifyingvalue));

        }

        public static async Task<IResult> GetSampleByIdentifyingValue(SampleRackService service, string Identifyingvalue)
        {
            // returns the sample
            return TypedResults.Ok(await service.GetSampleIdByIdentifyingValue(Identifyingvalue));
        }

        public static async Task<IResult> GetSampleById(SampleRackService service, int id)
        {
            // returns the sample
            return TypedResults.Ok(await service.GetSamplebyId(id));
        }

        public static async Task<IResult> GetRack(SampleRackService service, int id)
        {
            return TypedResults.Ok(await service.GetRack(id));
        }

        public static async Task<IResult> GetRackSamples(SampleRackService service, int rackId)
        {
            return TypedResults.Ok(await service.GetRackSamples(rackId));
        }

        public static async Task<IResult> GetSampleTypes(SampleRackService service)
        {
            return TypedResults.Ok(await service.GetSampleTypes());
        }

        public static async Task<IResult> CreateSample(SampleRackService service, SampleDTO editedSample)
        {
            var result =  await service.CreateSample(editedSample);

            if (result != null)
            {
                return TypedResults.CreatedAtRoute(
                routeName: "GetSample",
                routeValues: new { id = result.Id },
                value: result);
            }
            else return Results.StatusCode(statusCode: 500);
        }

        public static async Task<IResult> UpdateSample(SampleRackService service, SampleDTO editedSample)
        {
            if (editedSample.Id == 0) { return TypedResults.UnprocessableEntity(); }

            var result = await service.UpdateSample(editedSample);

            if (result != null) { return TypedResults.Ok(); }
            else return Results.StatusCode(statusCode: 500);
        }

        public static async Task<IResult> DeleteSample(SampleRackService service, int id)
        {
            if (id == 0) { }

            var result = await service.DeleteSample(id);

            if (result) { return TypedResults.Ok(); }
            else
            {
                return TypedResults.UnprocessableEntity();
            }
        }

    }
}

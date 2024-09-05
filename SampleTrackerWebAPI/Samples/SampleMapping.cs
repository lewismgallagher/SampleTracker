using Microsoft.AspNetCore.Mvc;
using SampleTrackerWebAPI.RackConfiguration;
using SampleTrackerWebAPI.Samples;
namespace SampleTrackerWebAPI.Samples
{
    public static class SampleMapping
    {
        public static IEndpointRouteBuilder MapSamples(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("samples");




            // getting racks, samples and sampletypes
            groupBuilder.MapGet("/racks/search", SamplesEndpoints.GetRacks).WithName("GetRacks");
            groupBuilder.MapGet("/racks/", SamplesEndpoints.GetRack).WithName("SamplesGetRack");
            groupBuilder.MapGet("/", SamplesEndpoints.GetRackSamples).WithName("GetRackSamples");
            groupBuilder.MapGet("/sampletypes", SamplesEndpoints.GetSampleTypes).WithName("GetSampleTypes");

            // used for retrieving sample, sample id and checking if exists based on the samples unique indentifying value.
            groupBuilder.MapGet("/samplebyid", SamplesEndpoints.GetSampleByIdentifyingValue).WithName("GetSampleByIdentifyingValue");
            groupBuilder.MapGet("/sampleidbyid", SamplesEndpoints.GetSampleIdByIdentifyingValue).WithName("GetSampleIdByIdentifyingValue");
            groupBuilder.MapGet("/checksampleexists", SamplesEndpoints.CheckSampleExists).WithName("CheckSampleExists");

            // create update and delete
            groupBuilder.MapPost("/", SamplesEndpoints.CreateSample).WithName("CreateSample");
            groupBuilder.MapPut("/", SamplesEndpoints.UpdateSample).WithName("UpdateSample");
            groupBuilder.MapDelete("/", SamplesEndpoints.DeleteSample).WithName("DeleteSample");


            return builder;
        }
    }
}

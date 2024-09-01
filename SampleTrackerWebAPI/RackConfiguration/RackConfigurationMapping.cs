namespace SampleTrackerWebAPI.RackConfiguration
{
    public static class RackConfigurationMapping
    {
        public static IEndpointRouteBuilder MapRackConfiguration(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("configuration/racks");

            groupBuilder.MapGet("/", RackConfigurationEndpoints.GetRacks);
            groupBuilder.MapGet("/{id}", RackConfigurationEndpoints.GetRack).WithName("GetRack");
            groupBuilder.MapPost("/create/", RackConfigurationEndpoints.CreateRack);
            groupBuilder.MapPut("/update/", RackConfigurationEndpoints.UpdateRack);
            groupBuilder.MapDelete("/delete", RackConfigurationEndpoints.DeleteRack);

            return builder;
        }
    }
}

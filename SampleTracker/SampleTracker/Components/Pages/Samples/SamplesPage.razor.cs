using BAL.DTOs;
using DAL.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace SampleTracker.Components.Pages.Samples
{
    public partial class SamplesPage
    {

        [Parameter]
        public string RackId { get; set; }

        public SampleRackDTO Rack { get; set; }
        public List<SampleDTO> Samples { get; set; }
        public List<SampleTypeDTO> SampleTypes { get; set; }
        public bool HasLoaded { get; set; }

        protected override async Task OnInitializedAsync()
        {
            
            Rack = new SampleRackDTO();
            SampleTypes = new List<SampleTypeDTO>();
            Samples = new List<SampleDTO>();

            int rackId = int.Parse(RackId);
            Rack = await SampleRackService.GetRack(rackId);
            SampleTypes = await SampleRackService.GetSampleTypes();
            Samples = await SampleRackService.GetRackSamples(rackId);
            
            HasLoaded = true;

        }
    }
}
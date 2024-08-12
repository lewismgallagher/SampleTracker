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

        protected override async Task OnInitializedAsync()
        {
            Rack = new SampleRackDTO();
            int rackId = int.Parse(RackId);
            Rack = await SampleRackService.GetRack(rackId); 
           
        }
    }
}
using BAL.DTOs;
using BAL;
using Global.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace SampleTracker.Components.Pages.Samples
{
    public partial class RackSearchPage
    {
        public bool IsSearching { get; set; }
        public bool HasLoaded { get; set; }
        public string RackName { get; set; } = "";
        public int? RackId { get; set; }


        public SampleRackDTO RackToEdit { get; set; }
        public List<SampleRackDTO> Racks { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Racks = new List<SampleRackDTO>();
            RackToEdit = new SampleRackDTO();
        }

        public async Task SearchRacks()
        {
            HasLoaded = false;
            IsSearching = true;
            Racks = await SampleRackService.SearchRacks(RackId,RackName);
            IsSearching = false;
            HasLoaded = true;
        }

        public async Task ViewRack(SampleRackDTO rack)
        {
            // TODO take to next page
        }

    }
}
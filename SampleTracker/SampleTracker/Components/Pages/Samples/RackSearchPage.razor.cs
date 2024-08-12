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

        public string Url { get; set; } = "/Pages/Samples/";

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


    }
}
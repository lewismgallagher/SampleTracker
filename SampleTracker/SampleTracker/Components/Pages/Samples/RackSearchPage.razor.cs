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

        public List<RackDTO> Racks { get; set; }

        protected override void OnInitialized()
        {
            Racks = new List<RackDTO>();
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
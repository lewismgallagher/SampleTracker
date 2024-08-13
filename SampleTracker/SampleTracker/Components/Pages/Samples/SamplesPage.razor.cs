using BAL.DTOs;
using DAL.Data.Entities;
using Microsoft.AspNetCore.Components;
using System.Linq;

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
        public int SelectedSampleTypeId { get; set; }
        public SampleTypeDTO SelectedSampleType { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Rack = new SampleRackDTO();
            SampleTypes = new List<SampleTypeDTO>();
            Samples = new List<SampleDTO>();

            int rackId = int.Parse(RackId);
            Rack = await SampleRackService.GetRack(rackId);
            SampleTypes = await SampleRackService.GetSampleTypes();
            Samples = await SampleRackService.GetRackSamples(rackId);
            SelectedSampleType = new SampleTypeDTO();
            HasLoaded = true;

        }

        public bool CheckSampleExistsInRack(int col, int row)
        {
            return Samples.Any(s => s.ColumnNumber == col && s.RowNumber == row);
        }

        public SampleDTO GetSample(int col, int row)
        {
            return Samples.FirstOrDefault(s => s.ColumnNumber == col && s.RowNumber == row);
        }

        public SampleDTO CreateEmptySample(int col, int row)
        {
            return new SampleDTO() { ColumnNumber = col, RowNumber = row };
        }

        public void ChangeSampleType()
        {
            SelectedSampleType = SampleTypes.FirstOrDefault(x => x.Id == SelectedSampleTypeId);
        }

        public SampleTypeDTO GetSampleTypeFromExistingSample(int sampleTypeId)
        {
            return SampleTypes.FirstOrDefault(x => x.Id == sampleTypeId);
        }


    }


}
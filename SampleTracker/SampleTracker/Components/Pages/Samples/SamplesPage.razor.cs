using BAL.DTOs;
using DAL.Data.Entities;
using Global.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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

        public bool ValueChanged { get; set; }

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
            SelectedSampleTypeId = 1;
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
            var sample = new SampleDTO() { ColumnNumber = col, RowNumber = row, RackId = Rack.RackId };
            Samples.Add(sample);
            return sample;
        }

        public void ChangeSampleType()
        {
            SelectedSampleType = SampleTypes.FirstOrDefault(x => x.Id == SelectedSampleTypeId);
        }

        public SampleTypeDTO GetSampleTypeFromExistingSample(int sampleTypeId)
        {
            return SampleTypes.FirstOrDefault(x => x.Id == sampleTypeId);
        }


        // Add functionality to remove sample if being moved from to a different cell on the same rack
        public async void SaveSample(SampleDTO editedSample)
        {
            //if true hasn't been edited
            if (editedSample.IdentifyingValue == editedSample.OriginalIdentifyingValue) return;

            //logic for deletion by removing unique value
            if (string.IsNullOrEmpty(editedSample.IdentifyingValue) && !string.IsNullOrWhiteSpace(editedSample.OriginalIdentifyingValue))
            {
                await SampleRackService.DeleteSample(editedSample.Id);
                Samples.Remove(editedSample);
                return;
            }

            // delete old sample from rack
            if (editedSample.Id != 0)
            {
                await SampleRackService.DeleteSample(editedSample.Id);
                editedSample.Id = 0;
                Samples.Remove(editedSample);
            }

            bool sampleExists = await SampleRackService.CheckSampleExists(editedSample.IdentifyingValue);

            if (sampleExists)
            {
                int sampleId = await SampleRackService.GetSampleIdByIdentifyingValue(editedSample.IdentifyingValue);
                editedSample.Id = sampleId;
            }

            if (editedSample.Id == 0)
            {
                editedSample.SampleTypeId = SelectedSampleTypeId;
            };
            // save
            await SampleRackService.SaveChangesAsync(editedSample);

        }

        public bool CheckSampleExistsInThisRack(string IdentifyingValue)
        {
            return Samples.Any(s => s.IdentifyingValue == IdentifyingValue);
        }

        public int GetSampleIdFromRackByIdentifyingValue(string identifyingValue)
        {
            return Samples.FirstOrDefault(s => s.IdentifyingValue == identifyingValue).Id;
        }



    }

}
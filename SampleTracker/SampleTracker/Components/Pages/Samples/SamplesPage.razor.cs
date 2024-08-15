using BAL.DTOs;
using DAL.Data.Entities;
using Global.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;

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

        protected override async Task OnInitializedAsync()
        {

            Rack = new SampleRackDTO();
            SampleTypes = new List<SampleTypeDTO>();
            Samples = new List<SampleDTO>();

            int rackId = int.Parse(RackId);
            Rack = await SampleRackService.GetRack(rackId);
            SampleTypes = await SampleRackService.GetSampleTypes();
            Samples = await SampleRackService.GetRackSamples(rackId);
            CreateSamplesList();

            //Default Sample Type
            SelectedSampleTypeId = 1;
            HasLoaded = true;

        }

        public void CreateSamplesList()
        {
            int counter = 0;
            for (int r = 1; r < Rack.NumberOfRows + 1; r++)
            {

                for (int c = 1; c < Rack.NumberOfColumns + 1; c++)
                {
                    if (CheckSampleExistsInRack(c, r)) { continue; }

                    Samples.Add(CreateEmptySample(c, r));
                    counter++;
                }
            }
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
            return sample;
        }

        //public void ChangeSampleType()
        //{
        //    SelectedSampleType = SampleTypes.FirstOrDefault(x => x.Id == SelectedSampleTypeId);
        //}

        public SampleTypeDTO GetSampleTypeFromExistingSample(int sampleTypeId)
        {
            return SampleTypes.FirstOrDefault(x => x.Id == sampleTypeId);
        }

        //TODO Heavily refactor, split up and simplify this method
        public async Task SaveSample(SampleDTO editedSample)
        {
            //if true sample hasn't been edited

            if (editedSample.IdentifyingValue == editedSample.OriginalIdentifyingValue) return;

            //logic for deletion by removing unique value in textbox
            if (string.IsNullOrEmpty(editedSample.IdentifyingValue) && !string.IsNullOrWhiteSpace(editedSample.OriginalIdentifyingValue))
            {
                await SampleRackService.DeleteSample(editedSample.Id);
                Samples.Remove(editedSample);
                editedSample = CreateEmptySample(editedSample.ColumnNumber, editedSample.RowNumber);
                Samples.Add(editedSample);
                return;
            }

            // delete old sample from rack
            if (editedSample.Id != 0)
            {
                await SampleRackService.DeleteSample(editedSample.Id);
                editedSample.Id = 0;
            }

            // Check if Samples exists in this rack
            bool sampleExistsInThisRack = CheckSampleExistsInThisRack(editedSample);
            if (sampleExistsInThisRack)
            {
                var existingSample = GetSampleFromRackByEditedSample(editedSample);
                editedSample.Id = existingSample.Id;
                editedSample.SampleType = existingSample.SampleType;
                editedSample.SampleTypeId = existingSample.SampleTypeId;
            }

            bool sampleExists = await SampleRackService.CheckSampleExists(editedSample.IdentifyingValue);

            if (sampleExists)
            {
               var existingSample = await SampleRackService.GetSampleByIdentifyingValue(editedSample.IdentifyingValue);
                editedSample.Id = existingSample.Id;
                editedSample.SampleType = existingSample.SampleType;
                editedSample.SampleTypeId = existingSample.SampleTypeId;
            }

            if (editedSample.Id == 0)
            {
                editedSample.SampleTypeId = SelectedSampleTypeId;
                editedSample.SampleType = SampleTypes
                    .FirstOrDefault(st => st.Id == SelectedSampleTypeId).Name;
            };

            if (sampleExistsInThisRack)
            {
                var existingSample = GetSampleFromRackByEditedSample(editedSample);

                Samples.Remove(existingSample);
                existingSample = CreateEmptySample(existingSample.ColumnNumber, existingSample.RowNumber);
                Samples.Add(existingSample);
            }

            // Save
            await SampleRackService.SaveChangesAsync(editedSample);

            if (editedSample.Id == 0)
            {
                // Set newly saved ID
                editedSample.Id = await SampleRackService.GetSampleIdByIdentifyingValue(editedSample.IdentifyingValue);
            }

            // Sync the two values.
            editedSample.OriginalIdentifyingValue = editedSample.IdentifyingValue;
        }

        public bool CheckSampleExistsInThisRack(SampleDTO editedSample)
        {
            return Samples.Any(s => s.IdentifyingValue == editedSample.IdentifyingValue
            && (s.ColumnNumber != editedSample.ColumnNumber
            || s.RowNumber != editedSample.RowNumber));
        }

        public SampleDTO GetSampleFromRackByIdentifyingValue(string identifyingValue)
        {
            return Samples.FirstOrDefault(s => s.IdentifyingValue == identifyingValue);
        }

        public SampleDTO GetSampleFromRackByEditedSample(SampleDTO editedSample)
        {
            return Samples.FirstOrDefault(s => s.IdentifyingValue == editedSample.IdentifyingValue
            && (s.ColumnNumber != editedSample.ColumnNumber
            || s.RowNumber != editedSample.RowNumber));
        }

    }

}
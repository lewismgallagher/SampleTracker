using BAL.DTOs;
using BAL;
using Global.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace SampleTracker.Components.Pages.Configuration.SampleTypeConfiguration
{
    public partial class SampleTypeConfigurationPage
    {
        private ConfigPageStatus _viewStatus;
        private AlertMessageTypes _messageType;
        private AlertMessageReason _messageReason;
        private bool _showMessage;
        private EditContext _editContext;

        public bool HasLoaded { get; set; } = false;

        public bool InEditMode { get; set; } = false;

        public SampleTypeConfigurationDTO SampleTypeToEdit { get; set; }
        public List<SampleTypeConfigurationDTO> SampleTypes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _viewStatus = ConfigPageStatus.Viewing;
            SampleTypes = new List<SampleTypeConfigurationDTO>();
            SampleTypeToEdit = new SampleTypeConfigurationDTO();
            _editContext = new EditContext(SampleTypeToEdit);

            SampleTypes = await SampleTypeConfigurationService.GetSampleTypes();

            if (SampleTypes != null) { HasLoaded = true; }
        }

        // TODO Implement this so it isn't just for triggering client side validation.

        public void Submit()
        {
        }

        public async Task Save()
        {
            bool itemSaved = await SampleTypeConfigurationService.SaveChangesAsync(SampleTypeToEdit);

            if (itemSaved)
            {
                SampleTypes = await SampleTypeConfigurationService.GetSampleTypes();
                _viewStatus = ConfigPageStatus.Viewing;
                _messageType = AlertMessageTypes.Success;
            }
            else
            {
                _messageType = AlertMessageTypes.Error;
            }
            _messageReason = AlertMessageReason.Save;
            _showMessage = true;
        }

        public async Task Delete()
        {
            bool itemSaved = await SampleTypeConfigurationService.DeleteSampleType(SampleTypeToEdit.Id);

            if (itemSaved)
            {
                SampleTypes = await SampleTypeConfigurationService.GetSampleTypes();
                _viewStatus = ConfigPageStatus.Viewing;
                _messageType = AlertMessageTypes.Success;
            }
            else
            {
                _messageType = AlertMessageTypes.Error;
            }
            _messageReason = AlertMessageReason.Delete;
            _showMessage = true;
        }

        public void EditSampleType(SampleTypeConfigurationDTO sampleType)
        {
            SampleTypeToEdit = sampleType;
            _viewStatus = ConfigPageStatus.Editing;
        }

        public void CreateSampleType()
        {
            SampleTypeToEdit = new SampleTypeConfigurationDTO();
            _viewStatus = ConfigPageStatus.Creating;
        }

        public void ReturnToViewMode()
        {
            SampleTypeToEdit = new SampleTypeConfigurationDTO();
            _viewStatus = ConfigPageStatus.Viewing;
        }

        public async Task CloseAlert()
        {
            _showMessage = false;
        }
    }
}
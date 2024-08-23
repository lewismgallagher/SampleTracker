using Services;
using Services.DTOs;
using Global.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace SampleTracker.Components.Pages.Configuration.RackConfiguration
{
    public partial class RackConfigurationPage
    {
        private ConfigPageStatus _viewStatus;
        private AlertMessageTypes _messageType;
        private AlertMessageReason _messageReason;
        private bool _showMessage;
        private EditContext _editContext;

        public bool HasLoaded { get; set; } = false;

        public bool InEditMode { get; set; } = false;

        public RackConfigurationDTO RackToEdit { get; set; }
        public List<RackConfigurationDTO> Racks { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _viewStatus = ConfigPageStatus.Viewing;
            Racks = new List<RackConfigurationDTO>();
            RackToEdit = new RackConfigurationDTO();
            _editContext = new EditContext(RackToEdit);

            Racks = await RackConfigurationService.GetRacks();

            if (Racks != null) { HasLoaded = true; }
        }

        // TODO Implement this so it isn't just for triggering client side validation.

        public void Submit()
        {
        }

        public async Task Save()
        {
            bool itemSaved = await RackConfigurationService.SaveChangesAsync(RackToEdit);

            if (itemSaved)
            {
                Racks = await RackConfigurationService.GetRacks();
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
            bool itemSaved = await RackConfigurationService.DeleteRack(RackToEdit.Id);

            if (itemSaved)
            {
                Racks = await RackConfigurationService.GetRacks();
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

        public void EditRack(RackConfigurationDTO rack)
        {
            RackToEdit = rack;
            _viewStatus = ConfigPageStatus.Editing;
        }

        public void CreateRack()
        {
            RackToEdit = new RackConfigurationDTO();
            _viewStatus = ConfigPageStatus.Creating;
        }

        public void ReturnToViewMode()
        {
            RackToEdit = new RackConfigurationDTO();
            _viewStatus = ConfigPageStatus.Viewing;
        }

        public async Task CloseAlert()
        {
            _showMessage = false;
        }

    }
}
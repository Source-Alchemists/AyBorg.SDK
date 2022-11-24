using System.ComponentModel.DataAnnotations;

namespace AyBorg.SDK.Data.DAL
{
    public record ProjectSettingsRecord
    {
        [Key]
        public Guid DbId { get; set; }

        public bool IsForceResultCommunicationEnabled { get; set; } = false;

        public bool IsForceWebUiCommunicationEnabled { get; set; } = false;
    }
}

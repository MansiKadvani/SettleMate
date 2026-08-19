namespace SettleMate.Models
{
    public class ErrorViewModel
    {
        // Store error request ID
        public string? RequestId { get; set; }

        // Check if request ID is available
        public bool ShowRequestId =>
            !string.IsNullOrEmpty(RequestId);
    }
}
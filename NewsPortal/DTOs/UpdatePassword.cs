namespace NewsPortal.DTOs
{
    public class UpdatePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReEnterPassword { get; set; }
    }
}

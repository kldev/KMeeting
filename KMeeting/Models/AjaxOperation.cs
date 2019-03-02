namespace KMeeting.Models
{
    public class AjaxOperation
    {
        public string Message { get; set; } = string.Empty;

        public bool Success { get; set; } = false;

        public string Error { get; set; } = string.Empty;

        public static AjaxOperation Failed(string error)
        {
            return new AjaxOperation { Success = false, Error = error };
        }

        public static AjaxOperation Successful(string message)
        {
            return new AjaxOperation { Success = true, Message = message };
        }
    }
}

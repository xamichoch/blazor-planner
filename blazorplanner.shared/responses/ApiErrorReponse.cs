namespace blazorplanner.shared.responses
{
    public class ApiErrorReponse
    {
        public string Message { get; set; }

        public string[] Errors { get; set; }

        public bool IsSuccess { get; set; }

    }
}
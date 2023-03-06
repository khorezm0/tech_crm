namespace Backend.ClientData.Auth
{
    public class TelegramAuthGetRequest
    {
        public int UserId { get; set; }
        public string OneTimePassword { get; set; }
    }
}

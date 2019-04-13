namespace ChatApp.Model
{
    public class Message
    {
        public string Content { get; set; }
        public string Username { get; set; }
        public bool IsServer { get; set; }
        public string GroupName { get; set; }

    }
}

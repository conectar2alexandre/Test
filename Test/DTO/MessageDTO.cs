namespace Test.DTO
{
    public class MessageDTO
    {
        /// <summary>
        /// Message Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Message Type 0 = Success 1 = Warning 2 = Error
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}

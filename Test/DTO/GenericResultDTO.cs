namespace Test.DTO
{
    public class GenericResultDTO<T>
    {
        private List<MessageDTO> _messagesList = new List<MessageDTO>();

        public T Content { get; set; }

        public IEnumerable<MessageDTO> Messages
        {
            get
            {
                return _messagesList;
            }
        }
        /// <summary>
        /// Status Result : 0 = Success | 1 = Warning | 2 = Error
        /// </summary>
        public MessageType StatusResult
        {
            get
            {
                if (Messages == null || !Messages.Any())
                    return MessageType.Success;

                if (Messages.Any(c => c.MessageType == MessageType.Error))
                    return MessageType.Error;

                if (Messages.Any(c => c.MessageType == MessageType.Warning))
                    return MessageType.Warning;

                if (Messages.Any(c => c.MessageType == MessageType.Success))
                    return MessageType.Success;

                return MessageType.Error;
            }
        }

        public void AddErrorMessage(string message)
        {
            this.Add(message, MessageType.Error);
        }
        public void AddWarningMessage(string message)
        {
            this.Add(message, MessageType.Warning);
        }
        public void AddMessage(string message)
        {
            this.Add(message, MessageType.Success);
        }

        public void AddMessageList(IEnumerable<MessageDTO> messageList)
        {
            _messagesList.AddRange(messageList);
        }
        private void Add(string message, MessageType status)
        {
            var _messageResultDTO = new MessageDTO();
            _messageResultDTO.Description = message;
            _messageResultDTO.MessageType = status;

            _messagesList.Add(_messageResultDTO);
        }
    }
}

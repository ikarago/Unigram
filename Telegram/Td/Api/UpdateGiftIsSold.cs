namespace Telegram.Td.Api
{
    public partial class UpdateGiftIsSaved
    {
        public long SenderUserId { get; set; }

        public long MessageId { get; set; }

        public bool IsSaved { get; set; }

        public UpdateGiftIsSaved(long senderUserId, long messageId, bool isSaved)
        {
            SenderUserId = senderUserId;
            MessageId = messageId;
            IsSaved = isSaved;
        }
    }
}

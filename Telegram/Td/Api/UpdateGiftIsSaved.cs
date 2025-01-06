namespace Telegram.Td.Api
{
    public partial class UpdateGiftIsSold
    {
        public long SenderUserId { get; set; }

        public long MessageId { get; set; }

        public UpdateGiftIsSold(long senderUserId, long messageId)
        {
            SenderUserId = senderUserId;
            MessageId = messageId;
        }
    }
}

namespace Telegram.Td.Api
{
    public class UpdateChatAffiliatePrograms
    {
        public UpdateChatAffiliatePrograms(long chatId)
        {
            ChatId = chatId;
        }

        public long ChatId { get; set; }
    }
}

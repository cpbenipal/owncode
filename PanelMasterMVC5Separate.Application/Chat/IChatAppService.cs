using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Chat.Dto;

namespace PanelMasterMVC5Separate.Chat
{
    public interface IChatAppService : IApplicationService
    {
        GetUserChatFriendsWithSettingsOutput GetUserChatFriendsWithSettings();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}

﻿using System;
using Abp;
using Abp.Domain.Services;

namespace PanelMasterMVC5Separate.Chat
{
    public interface IChatMessageManager : IDomainService
    {
        void SendMessage(UserIdentifier sender, UserIdentifier receiver, string message, string senderTenancyName, string senderUserName, Guid? senderProfilePictureId);

        long Save(ChatMessage message);

        int GetUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier sender);
    }
}
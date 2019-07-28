﻿using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Hubs.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        public MessageHub(
            HubConnectionStorage connectionStore
        ) : base(connectionStore)
        {
            topic = "message";
        }


        //public async Task SendMessageToUser(Message message, string toUser)
        //{
        //    string username = Context.User.Identity.Name;            
        //    var recieverInform = await _userManager.FindByIdAsync(toUser);
        //    foreach (var connectionId in connectionStore.GetConnections(recieverInform.UserName))
        //    {
        //        await Clients.Client(connectionId).SendMessage(message);
        //    }
        //}

        public async Task SendMessageViewingStatus(string toUser)
        {
            var username = Context.User.Identity.Name;

            foreach (var connectionId in connectionStore.GetConnections(topic, toUser))
                await Clients.Client(connectionId).SendMessageViewingStatus(username);
        }


        public override Task OnConnectedAsync()
        {
            // Do something just related to message hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Do something just related to message hub            
            return base.OnDisconnectedAsync(exception);
        }
    }
}
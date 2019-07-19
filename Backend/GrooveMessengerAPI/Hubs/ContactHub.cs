using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactHub : HubBase<IContactHubClient>
    {
        private IContactService _contactService;
        private IConversationService _conversationService;
        private IParticipantService _participantService;
        private IUserResolverService _userResolverservice;
        private IMessageService _messageService;

        public ContactHub(
            HubConnectionStore<string> connectionStore,
            IContactService contactService,
            IConversationService conversationService,
            IParticipantService participantService,
            IUserResolverService userResolverservice,
            IMessageService messageService
            ) 
            : base(connectionStore)
        {
            _contactService = contactService;
            _conversationService = conversationService;
            _participantService = participantService;
            _userResolverservice = userResolverservice;
            _messageService = messageService;
        }


        public async Task SendNewContactToUser(string msg, string toUser)
        {
            
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendNewContactToFriend(msg);
            }
        }

        public async Task SendRemoveContactToUser(string msg, string toUser)
        {
            
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendRemoveContactToFriend(msg);
            }
        }

        public  async Task AddContact(HubContact hub, string toUser)
        {

            HubContact contact = new HubContact(Context.User.Identity.Name,Context.ConnectionId);
            HubContact contactToUser = new HubContact(toUser,hub.connId);
            
            // Create A Conversation
            CreateConversationModel createConversationModel = new CreateConversationModel() { Id = Guid.NewGuid() };
            _conversationService.AddConversation(createConversationModel);

            // Create Msg
            CreateMessageModel createMessage = new CreateMessageModel() { ConversationId = createConversationModel.Id };
            _messageService.AddMessage(createMessage);

            //Add Participant
            ParticipantModel newPar = new ParticipantModel() { Id = Guid.NewGuid(), ConvId =createConversationModel.Id ,UserId = _userResolverservice.CurrentUserId()};
            _participantService.NewParticipant(newPar);

            await Clients.Client(hub.connId).AddNewContact(hub);

        }

        public async Task AcceptContact( string toUser, Guid ConversationID)
        {
            HubContact contact = new HubContact(Context.User.Identity.Name, Context.ConnectionId);

            ParticipantModel newPar = new ParticipantModel() { Id = Guid.NewGuid(), ConvId = ConversationID, UserId = _userResolverservice.CurrentUserId() };
            _participantService.NewParticipant(newPar);


            await Clients.Client(Context.ConnectionId).AcceptFriend(contact);
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            if (!connectionStore.GetConnections(name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                connectionStore.Add(name, conn);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            connectionStore.Remove(name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

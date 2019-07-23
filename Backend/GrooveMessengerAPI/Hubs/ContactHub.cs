using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
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
        private IUserService _userInfoContact;
        
        
        public ContactHub(
            HubConnectionStore<string> connectionStore,
            IContactService contactService,
            IConversationService conversationService,
            IParticipantService participantService,
            IUserResolverService userResolverservice,
            IMessageService messageService,
            IUserService userInfoContact
            ) 
            : base(connectionStore)
        {
            _contactService = contactService;
            _conversationService = conversationService;
            _participantService = participantService;
            _userResolverservice = userResolverservice;
            _messageService = messageService;
            _userInfoContact = userInfoContact;
        }

        
        public async Task SendNewContactToUser(AddContactModel Contact, CreateConversationModel createConversationModel)
        {
            foreach (var connectionId in connectionStore.GetConnections(Contact.ContactId))
            {
                await Clients.Client(connectionId).SendNewContactToFriend(Contact.ContactId);
            }
        }

        public async Task SendRemoveContactToUser(string msg, string toUser)
        {
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendRemoveContactToFriend(msg);
            }
        }

        public void UpdateContactList()
        {
            _contactService.GetUserContactList();
        }

        public async Task AddContact(AddContactModel addContact)
        {
            //Add Contact
            AddContactModel contactToUser = new AddContactModel() { UserId = addContact.UserId, DisplayName = addContact.DisplayName,ContactId = addContact.ContactId };
            _contactService.AddContact(contactToUser);
            AddContactModel contact = new AddContactModel() { UserId = _userResolverservice.CurrentUserId(), DisplayName = Context.User.Identity.Name, ContactId = _userResolverservice.CurrentUserInfoId() };
            _contactService.AddContact(contact);
            //Create UserInfo
            CreateUserInfoModel userInfo = new CreateUserInfoModel(){ UserId = _userResolverservice.CurrentUserId()};
            _userInfoContact.AddUserInfo(userInfo);
            CreateUserInfoModel TouserInfo = new CreateUserInfoModel() { UserId = addContact.UserId };
            _userInfoContact.AddUserInfo(TouserInfo);

            // Create A Conversation
            CreateConversationModel createConversationModel = new CreateConversationModel() { Id = Guid.NewGuid() };
            _conversationService.AddConversation();


            // Create Msg
            CreateMessageModel createMessage = new CreateMessageModel() { ConversationId =  createConversationModel.Id };
            _messageService.AddMessage(createMessage);


            //Add Participant
            ParticipantModel newPar = new ParticipantModel() { Id = Guid.NewGuid(), ConvId = createConversationModel.Id ,UserId = _userResolverservice.CurrentUserId()};
            _participantService.AddParticipant(newPar);
            ParticipantModel UserPar = new ParticipantModel() { Id = Guid.NewGuid(), ConvId = createConversationModel.Id, UserId = _userResolverservice.CurrentUserId() };
            _participantService.AddParticipant(UserPar);
            await _contactService.GetUserContactList();
            
            await Clients.Client(addContact.ContactId).AddNewContact(addContact);

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

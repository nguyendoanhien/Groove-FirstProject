//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GrooveMessengerAPI.Auth
//{
//    public class AuthSocial
//    {
//        public async Task<User> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
//        {
//            await Task.Delay(1);
//            return this.FindUserOrAdd(payload);
//        }

//        private User FindUserOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
//        {
//            var u = _users.Where(x => x.email == payload.Email).FirstOrDefault();
//            if (u == null)
//            {
//                u = new User()
//                {
//                    id = Guid.NewGuid(),
//                    name = payload.Name,
//                    email = payload.Email,
//                    oauthSubject = payload.Subject,
//                    oauthIssuer = payload.Issuer
//                };
//                _users.Add(u);
//            }
//            this.PrintUsers();
//            return u;
//        }
//    }
//}

using GrooveMessengerDAL.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class InitialGroupModel
    {
        public string Name { get; set; }

        public string Avatar { get; set; }

        public IEnumerable<IndexUserInfoModel> Members { get; set; }
    }
}

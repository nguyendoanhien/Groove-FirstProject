using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Entities
{

    public class ConversationEntity : AuditBaseEntity<Guid>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Avatar { get; set; }

        public ICollection<MessageEntity> MessageEntity { get; set; }

        public ICollection<ParticipantEntity> ParticipantEntity { get; set; }


    }
}

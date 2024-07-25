using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class Entry : BaseEntity
    {
        public string Subject { get; set; } // entry'nin başlığı

        public string Content { get; set; } // entry'nin içeriği

        public Guid CreatedById { get; set; } // entry'i oluşutarn kişi



        public virtual User CreatedBy { get; set; }

        public virtual ICollection<EntryComment> EntryComments { get; set; }

        public virtual ICollection<EntryVote> EntryVotes { get; set; }

        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }

    }
}

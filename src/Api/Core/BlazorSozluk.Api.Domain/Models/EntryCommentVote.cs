﻿using BlazorSozluk.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryCommentVote : BaseEntity

    {
        public Guid EntryCommentId { get; set; }

        public Guid CreatedById { get; set; }

        public VoteType VoteType { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual EntryComment EntryComment { get; set; } //EntryVote ile Entry arasında bağlantı kurabilmek için bunu yapıyoruz
    }
}

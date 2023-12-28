using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class CommentReply
{
    public int ReplyId { get; set; }

    public int? CommentId { get; set; }

    public string? ReplyText { get; set; }

    public byte[] Timestamp { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual ProductComment? Comment { get; set; }

    public virtual AspNetUser? User { get; set; }
}

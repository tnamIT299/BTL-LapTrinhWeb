using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class CommentReply
{
    public int ReplyId { get; set; }

    public int? CommentId { get; set; }

    public int? UserId { get; set; }

    public string? ReplyText { get; set; }

    public byte[] Timestamp { get; set; } = null!;

    public virtual ProductComment? Comment { get; set; }

    public virtual User? User { get; set; }
}

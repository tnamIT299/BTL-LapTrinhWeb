using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ProductComment
{
    public int CommentId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public string? CommentText { get; set; }

    public byte[] CreatedDate { get; set; } = null!;

    public virtual ICollection<CommentReply> CommentReplies { get; set; } = new List<CommentReply>();

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}

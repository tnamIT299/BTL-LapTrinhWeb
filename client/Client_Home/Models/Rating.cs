using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? ProductId { get; set; }

    public int? StarRating { get; set; }

    public string? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual AspNetUser? User { get; set; }
}

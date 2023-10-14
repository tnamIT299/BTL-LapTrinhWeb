using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ProductSubImage
{
    public int Id { get; set; }

    public string ImgUrl { get; set; } = null!;

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}

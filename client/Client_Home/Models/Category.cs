using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Client_Home.Models;

public partial class Category
{
    [DisplayName("Mã danh mục")]
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

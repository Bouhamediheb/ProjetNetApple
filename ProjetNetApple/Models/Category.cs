using System;
using System.Collections.Generic;

namespace ProjetNetApple.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

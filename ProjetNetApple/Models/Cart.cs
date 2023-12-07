using System;
using System.Collections.Generic;

namespace ProjetNetApple.Models;

public partial class Cart
{
    public Cart()
    {
        CartLines = new List<CartLine>();
    }
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<CartLine> CartLines { get; set; } = new List<CartLine>();

}

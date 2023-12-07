using System;
using System.Collections.Generic;

namespace ProjetNetApple.Models;

public partial class CartLine
{

    public int Id { get; set; }

    public int? CartId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Cart? Cart { get; set; }
}

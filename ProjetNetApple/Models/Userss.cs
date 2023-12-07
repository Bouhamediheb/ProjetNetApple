using System;
using System.Collections.Generic;

namespace ProjetNetApple.Models;

public partial class Userss
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

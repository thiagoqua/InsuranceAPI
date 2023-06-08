using System;
using System.Collections.Generic;

namespace InsuranceAPI.Models;

public partial class Admin
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Token { get; set; }

    public long Producer { get; set; }

    public virtual Producer ProducerNavigation { get; set; } = null!;
}

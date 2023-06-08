using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Phone
{
    public long Id { get; set; }

    public long Insured { get; set; }

    public string Number { get; set; } = null!;

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual Insured InsuredNavigation { get; set; } = null!;
}

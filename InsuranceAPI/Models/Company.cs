using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Company
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Logo { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();
}

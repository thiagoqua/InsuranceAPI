using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Address
{
    public long Id { get; set; }

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public int? Floor { get; set; }

    public int? Departament { get; set; }

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string Country { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();
}

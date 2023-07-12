using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Company
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Logo { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();
}

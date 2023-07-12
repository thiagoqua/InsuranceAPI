using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Phone
{
    public long Id { get; set; }

    [Required]
    public long Insured { get; set; }

    [Required]
    public string Number { get; set; } = null!;

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual Insured InsuredNavigation { get; set; } = null!;
}

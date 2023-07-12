using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Models;

public partial class Producer
{
    public long Id { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public DateTime Joined { get; set; }

    [Required]
    public int Code { get; set; }

    [JsonIgnore]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [JsonIgnore]
    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();
}

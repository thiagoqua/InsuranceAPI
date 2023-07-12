using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.Models;

public partial class Insured
{
    public long Id { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public string License { get; set; } = null!;

    [Required]
    public int Folder { get; set; }

    [Required]
    public string Life { get; set; } = null!;

    [Required]
    public DateTime Born { get; set; }

    [Required]
    public long Address { get; set; }

    [Required]
    public string Dni { get; set; } = null!;

    
    public string? Cuit { get; set; }

    [Required]
    public long Producer { get; set; }

    public string? Description { get; set; }

    [Required]
    public long Company { get; set; }

    public string? InsurancePolicy { get; set; }

    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public short PaymentExpiration { get; set; }

    [Required]
    public virtual Address AddressNavigation { get; set; } = null!;

    [Required]
    public virtual Company CompanyNavigation { get; set; } = null!;

    [Required]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    [Required]
    public virtual Producer ProducerNavigation { get; set; } = null!;
}

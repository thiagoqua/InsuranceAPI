using System;
using System.Collections.Generic;

namespace InsuranceAPI.Models;

public partial class Insured
{
    public long Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string License { get; set; } = null!;

    public int Folder { get; set; }

    public string Life { get; set; } = null!;

    public DateTime Born { get; set; }

    public long Address { get; set; }

    public string Dni { get; set; } = null!;

    public string? Cuit { get; set; }

    public long Producer { get; set; }

    public string? Description { get; set; }

    public long Company { get; set; }

    public string? InsurancePolicy { get; set; }

    public string Status { get; set; } = null!;

    public short PaymentExpiration { get; set; }

    public virtual Address AddressNavigation { get; set; } = null!;

    public virtual Company CompanyNavigation { get; set; } = null!;

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public virtual Producer ProducerNavigation { get; set; } = null!;
}

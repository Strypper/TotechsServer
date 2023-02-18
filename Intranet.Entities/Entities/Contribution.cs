using System;

namespace Intranet.Entities;

public class Contribution : BaseEntity<int>
{
    public User Contributor { get; set; } = default!;
    public decimal Amount { get; set; }
    public DateTime DonateOn { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsApproved { get; set; }
}

public enum PaymentType
{
    Cash, Momo, Bank
}

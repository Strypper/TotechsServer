using System;

namespace Intranet.DataObject;

public class ContributionDTO : BaseDTO<int>
{
    public UserDTO? Contributor { get; set; }
    public decimal Amount { get; set; }
    public DateTime DonateOn { get; set; }
    public PaymentTypeDTO PaymentType { get; set; }
    public bool IsApproved { get; set; }
}

public enum PaymentTypeDTO
{
    Cash, Momo, Bank
}

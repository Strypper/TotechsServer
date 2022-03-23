using System;

namespace Intranet.Entities.Entities
{
    public class Contribution : BaseEntity
    {
        public User        Contributor { get; set; }
        public decimal     Amount      { get; set; }
        public DateTime    DonateOn    { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public enum PaymentType
    {
        Cash, Momo, Bank
    }
}

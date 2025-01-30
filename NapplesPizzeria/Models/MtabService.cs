using System;
using System.Collections.Generic;

namespace NapplesPizzeria.Models;

public partial class MtabService
{
    public int InMtSerPky { get; set; }

    public int? InMtSerTable { get; set; }

    public DateTime? DtMtSerEntry { get; set; }

    public DateTime? DtMtSerOut { get; set; }

    public bool? BoMtSerIsOpen { get; set; }

    public decimal? DeMtSerSpent { get; set; }

    public virtual MtabTable? InMtSerTableNavigation { get; set; }

    public virtual ICollection<MtabOrder> MtabOrders { get; set; } = new List<MtabOrder>();
}

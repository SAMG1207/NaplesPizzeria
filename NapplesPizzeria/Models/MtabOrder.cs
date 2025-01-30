using System;
using System.Collections.Generic;

namespace NapplesPizzeria.Models;

public partial class MtabOrder
{
    public int InMtOrdPky { get; set; }

    public int? InMtOrdServiceFky { get; set; }

    public int? InMtOrdProductFky { get; set; }

    public virtual MtabProduct? InMtOrdProductFkyNavigation { get; set; }

    public virtual MtabService? InMtOrdServiceFkyNavigation { get; set; }
}

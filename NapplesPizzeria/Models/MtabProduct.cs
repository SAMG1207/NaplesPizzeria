using System;
using System.Collections.Generic;

namespace NapplesPizzeria.Models;

public partial class MtabProduct
{
    public int InMtProPky { get; set; }

    public int? InMtProCategorieFky { get; set; }

    public string? SvMtProName { get; set; }

    public string? SvMtProDescription { get; set; }

    public decimal? DeMtProPrice { get; set; }

    public virtual MtabCategory? InMtProCategorieFkyNavigation { get; set; }

    public virtual ICollection<MtabOrder> MtabOrders { get; set; } = new List<MtabOrder>();
}

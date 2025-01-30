using System;
using System.Collections.Generic;

namespace NapplesPizzeria.Models;

public partial class MtabCategory
{
    public int InMtCatPky { get; set; }

    public string? SvMtCatName { get; set; }

    public virtual ICollection<MtabProduct> MtabProducts { get; set; } = new List<MtabProduct>();
}

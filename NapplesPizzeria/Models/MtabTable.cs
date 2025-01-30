using System;
using System.Collections.Generic;

namespace NapplesPizzeria.Models;

public partial class MtabTable
{
    public int InMtTabPky { get; set; }

    public int? InMtTabNumber { get; set; }

    public virtual ICollection<MtabService> MtabServices { get; set; } = new List<MtabService>();
}

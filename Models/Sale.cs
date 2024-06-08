using System;
using System.Collections.Generic;

namespace Practical_work.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public int Count { get; set; }

    public DateTime Saletime { get; set; }

    public virtual Product Product { get; set; } = null!;
}

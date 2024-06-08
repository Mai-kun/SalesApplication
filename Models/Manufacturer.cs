using System;
using System.Collections.Generic;

namespace Practical_work.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Startworkdate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

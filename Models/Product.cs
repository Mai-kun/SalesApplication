using System.IO;

namespace Practical_work.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int Manufacturerid { get; set; }

    public bool Isactive { get; set; }

    public decimal Price { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public string? Photo
    {
        get
        {
            if (Image is not null)
                return Directory.GetCurrentDirectory() + @"\" + Image.Trim();

            return null;
        }
    }

    public string Color
    {
        get
        {
            if (Isactive)
                return "#fff";
            else
                return "#e7fabf";
        }
    }

    public string Status
    {
        get
        {
            if (Isactive)
                return "";
            else
                return "нет в наличии";
        }
    }

    public string Info
    {
        get
        {
            return $"{Name} - {Price} рублей";
        }
    }


}

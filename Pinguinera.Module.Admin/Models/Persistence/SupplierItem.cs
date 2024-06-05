
﻿namespace pinguinera_final_module.Models.Persistence;

public partial class SupplierItem
{
    public Guid SupplierItemId { get; set; }

    public Guid SupplierId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public double BasePrice { get; set; }

    public double SellPrice { get; set; }

    public decimal Stock { get; set; }

    public virtual BookSupplierItem? BookSupplierItem { get; set; }

    public virtual NovelSupplierItem? NovelSupplierItem { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}


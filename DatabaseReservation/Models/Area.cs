using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class Area
{
    [Key]
    [Column("areaId")]
    public int AreaId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string AreaType { get; set; } = null!;

    [InverseProperty("Area")]
    public virtual ICollection<AllTable> AllTables { get; set; } = new List<AllTable>();
}

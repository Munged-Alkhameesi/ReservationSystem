using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class AllTable
{
    [Key]
    [Column("tableId")]
    public int TableId { get; set; }

    [Column("tableName")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TableName { get; set; }

    [Column("areaId")]
    public int AreaId { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("AllTables")]
    public virtual Area Area { get; set; } = null!;

    [InverseProperty("Table")]
    public virtual ICollection<ReservedTable> ReservedTables { get; set; } = new List<ReservedTable>();
}

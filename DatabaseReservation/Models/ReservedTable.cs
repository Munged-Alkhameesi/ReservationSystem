using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class ReservedTable
{
    [Key]
    public int ReservedTableId { get; set; }

    public int ReservationId { get; set; }

    [Column("tableId")]
    public int TableId { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservedTables")]
    public virtual Reservation? Reservation { get; set; } = null!;

    [ForeignKey("TableId")]
    [InverseProperty("ReservedTables")]
    public virtual AllTable? Table { get; set; } = null!;
}

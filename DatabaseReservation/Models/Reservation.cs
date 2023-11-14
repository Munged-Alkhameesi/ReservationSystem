using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [Column("guestCount")]
    public int GuestCount { get; set; }

    [Column("reservationSource")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ReservationSource { get; set; }

    [Column("startDateTime", TypeName = "datetime")]
    public DateTime StartDateTime { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    [Column("notes")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column("sittingId")]
    public int SittingId { get; set; }

    [Column("guestId")]
    public int GuestId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ResStatus { get; set; } = null!;

    [ForeignKey("GuestId")]
    [InverseProperty("Reservations")]
    public virtual Guest? Guest { get; set; } = null!;

    [InverseProperty("Reservation")]
    public virtual ICollection<ReservedTable> ReservedTables { get; set; } = new List<ReservedTable>();

    [ForeignKey("SittingId")]
    [InverseProperty("Reservations")]
    public virtual Sitting? Sitting { get; set; } = null!;

}

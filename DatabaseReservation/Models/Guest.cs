using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

[Table("Guest")]
public partial class Guest
{
    [Key]
    [Column("guestId")]
    public int GuestId { get; set; }

    [Column("guestFirstName")]
    [StringLength(50)]
    [Unicode(false)]
    public string GuestFirstName { get; set; } = null!;

    [Column("guestLastName")]
    [StringLength(50)]
    [Unicode(false)]
    public string GuestLastName { get; set; } = null!;

    [Column("guestEmail")]
    [StringLength(255)]
    [Unicode(false)]
    public string GuestEmail { get; set; } = null!;

    [Column("guestPhoneNumber")]
    public int GuestPhoneNumber { get; set; }

    [InverseProperty("Guest")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

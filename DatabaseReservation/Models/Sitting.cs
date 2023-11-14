using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class Sitting
{
    [Key]
    [Column("sittingId")]
    public int SittingId { get; set; }

    [Column("sittingType")]
    [StringLength(20)]
    [Unicode(false)]
    public string SittingType { get; set; } = null!;

    [Column("startDateTime", TypeName = "datetime")]
    public DateTime StartDateTime { get; set; }

    [Column("endDateTime", TypeName = "datetime")]
    public DateTime EndDateTime { get; set; }

    [Column("capacity")]
    public int Capacity { get; set; }

    [InverseProperty("Sitting")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [NotMapped]
    public string Description { get { return SittingType +" " + StartDateTime + " To " + EndDateTime.TimeOfDay; } }
}

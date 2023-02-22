using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EC05_C_sharp_EFC_DBfirst.Models.Entities;

[Index("Email", Name = "UQ__Customer__A9D105341AE4F7E1", IsUnique = true)]
public partial class Customer
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(13)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Customers")]
    public virtual Address Address { get; set; } = null!;
}

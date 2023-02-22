using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EC05_C_sharp_EFC_DBfirst.Models.Entities;

public partial class Address
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string StreetName { get; set; } = null!;

    [StringLength(6)]
    [Unicode(false)]
    public string PostalCode { get; set; } = null!;

    [StringLength(100)]
    public string City { get; set; } = null!;

    [InverseProperty("Address")]
    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}

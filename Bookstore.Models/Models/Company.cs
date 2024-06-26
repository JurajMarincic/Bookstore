﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models;

public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; }
    public string City { get; set; }
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}
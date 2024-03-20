﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Bookstore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}

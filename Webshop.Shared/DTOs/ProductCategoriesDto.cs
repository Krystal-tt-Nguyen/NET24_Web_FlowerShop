﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Shared.DTOs;

public class ProductCategoriesDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}

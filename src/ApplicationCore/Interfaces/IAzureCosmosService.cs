﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;
public interface IAzureCosmosService<T> where T : class
{
    public Task<bool> CreateAsync(T entity);
}

﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryLine : Repository<Line, int>, IRepositoryLine
    {
        public RepositoryLine(DbContext context) : base(context)
        {
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RepositoryUserType : Repository<UserType, int>, IRepositoryUserType
    {
        public RepositoryUserType(DbContext context) : base(context)
        {
        }
    }
}
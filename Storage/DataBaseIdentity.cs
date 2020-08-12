using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    public class DataBaseIdentity : IdentityDbContext<User>
    {
        public DataBaseIdentity(DbContextOptions<DataBaseIdentity> options)
        : base(options) { }

    }
}

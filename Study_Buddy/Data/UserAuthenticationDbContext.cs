using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Study_Buddy.Data
{
    public class UserAuthenticationDbContext : IdentityDbContext
    {
        public UserAuthenticationDbContext(DbContextOptions<UserAuthenticationDbContext> options)
            : base(options)
        {
        }
    }   
}


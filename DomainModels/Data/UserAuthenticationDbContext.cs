using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Data
{
    public class UserAuthenticationDbContext : IdentityDbContext
    {
        public UserAuthenticationDbContext(DbContextOptions<UserAuthenticationDbContext> options)
            : base(options)
        {
        }
    }   
}


﻿using Microsoft.EntityFrameworkCore;

namespace first_project.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; } = null!;
    }
}

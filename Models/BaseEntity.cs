﻿using SQLite;

namespace CarListApp.Maui.Models
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

﻿namespace Eva.Infra.EntityFramework.DbContextes
{
    /// <summary>
    /// This interface encapsulates EvaDbContext class
    /// </summary>
    public interface IEvaDbContext
    {
        public EvaDbContext EvaDbContext { get; set; }
    }
}

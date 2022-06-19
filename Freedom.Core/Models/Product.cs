using Freedom.Frontend.Models.BaseEntitySqlite;
using SQLite;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freedom.Core.Models
{
    //[SQLite.Table("Product")]
    public class Product : IBase
    {
        [PrimaryKey, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string Observacion { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_departments")]
    public class Department
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("name"), MaxLength(50)]
        public string Name { get; set; }

        [Required, Column("address"), MaxLength(50)]
        public string Address { get; set; }

        // Relation
        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; }

    }
}

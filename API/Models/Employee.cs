﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employee
    {
        [Key, Column("nik", TypeName = "nchar(5)")]
        public string NIK { get; set; }

        [Required, Column("first_name"), MaxLength(25)]
        public string FirstName { get; set; }

        [Column("last_name"), MaxLength(25)]
        public string LastName { get; set; }

        [Required, Column("phone"), MaxLength(25)]
        public string Phone { get; set; }

        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required, Column("salary")]
        public int Salary { get; set; }

        [Required, Column("gender")]
        public Gender Gender { get; set; }

        [Column("manager_id")]
        public string? ManagerId { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }

        // Relation
        [JsonIgnore]
        public Account? Account { get; set; }

        [ForeignKey("DepartmentId")]
        [JsonIgnore]
        public Department? Department { get; set; }

        [JsonIgnore]
        public ICollection<SPLK>? Splks { get; set; }

        [ForeignKey("ManagerId")]
        [JsonIgnore]
        public virtual Employee? Manager { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}

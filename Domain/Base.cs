using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Base
    {
        public Base()
        {
            DataInclusion = DateTime.Now;
            UserName = Environment.UserDomainName + "\\" + Environment.UserName;
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("DataInclusion" , TypeName = "DATETIME")]
        public DateTime DataInclusion { get; set; }

        [Column("UserName" , TypeName ="NVARCHAR(500)")]
        public string UserName { get; set; }
    }
}

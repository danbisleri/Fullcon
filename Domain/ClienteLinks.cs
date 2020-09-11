
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("ClienteLinks")]
    public class ClienteLinks : Base
    {
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Column("Link", TypeName = "Nvarchar(2000)")]
        public string link { get; set; }

        [Column("Usuario", TypeName = "Nvarchar(100)")]
        public string usuario { get; set; }

        [Column("Senha", TypeName = "Nvarchar(100)")]
        public string senha { get; set; }

        [Column("Nome", TypeName = "Nvarchar(100)")]
        public string nome { get; set; }


        [ForeignKey("IdCliente")]
        public virtual Clientes cliente { get; set; }
    }
}

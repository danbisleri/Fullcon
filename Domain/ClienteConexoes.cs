using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("ClienteConexoes")]
    public partial class ClienteConexoes : Base
    {
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Column("Descricao", TypeName = "Nvarchar(100)")]
        public string descricao { get; set; }

        [Column("IP", TypeName = "Nvarchar(500)")]
        public string ip { get; set; }

        [Column("Usuario", TypeName = "Nvarchar(100)")]
        public string usuario { get; set; }

        [Column("Senha", TypeName = "Nvarchar(100)")]
        public string senha { get; set; }

        [Column("Obs", TypeName = "Nvarchar(4000)")]
        public string obsConexao { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Clientes cliente { get; set; }
    }
}

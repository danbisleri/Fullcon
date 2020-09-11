using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("ClienteAnexos")]
    public class ClienteAnexos : Base
    {

        public int IdCliente { get; set; }

        [Column("Descricao", TypeName = "Nvarchar(100)")]
        public string descricao { get; set; }

        [Column("Arquivo",TypeName = "varbinary(MAX)")]
        public byte[] arquivo { get; set; }

        [Column("Tipo", TypeName = "Nvarchar(100)")]
        public string tipo { get; set; }

        [Column("Nome" , TypeName = "Nvarchar(100)")]
        public string nome { get; set; }

        [Column("Tamanho")]
        public int tamanho { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Clientes cliente { get; set; }



    }
}

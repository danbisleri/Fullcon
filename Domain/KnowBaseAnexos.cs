
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("KnowBaseAnexos")]
    public class KnowBaseAnexos : Base
    {

        public int IdKnowbase { get; set; }

        [Column("Descricao", TypeName = "Nvarchar(100)")]
        public string descricao { get; set; }

        [Column("Arquivo", TypeName = "varbinary(MAX)")]
        public byte[] arquivo { get; set; }

        [Column("Tipo", TypeName = "Nvarchar(100)")]
        public string tipo { get; set; }

        [Column("Nome", TypeName = "Nvarchar(100)")]
        public string nome { get; set; }

        [ForeignKey("IdKnowbase")]
        public virtual KnowBase knowbase { get; set; }
    }
}

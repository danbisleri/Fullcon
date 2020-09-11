using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("KnowBase")]
    public partial class KnowBase : Base 
    {
        //[Column("IdCliente")]
        //public int IdCliente { get; set; }

        //[Column("IdKnowProduto")]
        //public int IdKnowProduto { get; set; }

        //[Column("IdKnowLocais")]
        //public int IdKnowLocais { get; set; }

        [Column("Erro", TypeName = "Nvarchar(4000)")]
        public string erro { get; set; }

        [Column("Descricao", TypeName = "Nvarchar(500)")]
        public string descricao { get; set; }

        [Column("Solucao", TypeName = "Nvarchar(4000)")]

        public string solucao { get; set; }

        [Column("Obs", TypeName = "Nvarchar(4000)")]
        public string obs { get; set; }

        public virtual IList<KnowBaseAnexos> knowbaseanexo { get; set; }

        //[Column("Versao", TypeName = "Nvarchar(15)")]
        //public string versao { get; set; }

        //[ForeignKey("IdKnowLocais")]
        //public virtual KnowLocais knowlocais { get; set; }

        //[ForeignKey("IdKnowProduto")]
        //public virtual KnowProduto knowproduto { get; set; }

        //[ForeignKey("IdCliente")]
        //public virtual Clientes cliente { get; set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("KnowProduto")]
    public partial class KnowProduto : Base
    {
        [Column("Nome", TypeName = "Nvarchar(100)")]
        public string nome { get; set; }
        [Column("Versao", TypeName = "Nvarchar(15)")]
        public string versao { get; set; }

        //public virtual IList<KnowBase> knowbase { get; set; }
    }
}

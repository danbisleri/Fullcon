using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("KnowLocais")]
    public partial class KnowLocais : Base
    {
        [Column("Nome", TypeName = "Nvarchar(100)")]
        public string nome { get; set; }

        //public virtual IList<KnowBase> knowbase { get; set; }
    }
}

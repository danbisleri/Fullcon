using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("Clientes")]
    public  class Clientes : Base
    {
        [Column("Nome",TypeName = "Nvarchar(100)" )]
        public string nome { get; set; }
        [Column("Obs", TypeName ="Nvarchar(4000)")]
        public string obs { get; set; }

        public virtual IList<ClienteConexoes> clienteconexao { get; set; }

        public virtual IList<ClienteLinks> clientelinks { get; set; }

        public virtual IList<ClienteAnexos> clienteanexos { get; set; }

        public virtual IList<KnowBase> knowbase { get; set; }
    }
}

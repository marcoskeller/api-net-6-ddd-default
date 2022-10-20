using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{

    //Classe Pai do projeto que vai utilizar todas as mensagens
    [Table("TB_MESSAGE")]
    public class Message : Notifies
    {

        [Column("MSN_ID")]
        public int Id { get; set; }


        [Column("MSN_TITULO")]
        [MaxLength(255)]
        public string Titulo { get; set; }

        
        [Column("MSN_ATIVO")]
        public bool Ativo { get; set; }

        
        [Column("MSN_DATA_CADASTRO")]
        public DateTime? DataCadastro  { get; set; }


        [Column("MSN_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }


        /*Realizando a integração da tabela 'Message' com a Tabela 'ApplicationUser' */
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

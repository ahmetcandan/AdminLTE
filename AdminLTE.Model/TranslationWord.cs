using AdminLTE.Interface;

namespace AdminLTE.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TranslationWord")]
    public partial class TranslationWord : IEntity
    {
        public TranslationWord()
        {

        }

        public int TranslationWordId { get; set; }

        [Required]
        public int TranslationLanguageId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        public bool IsDeleted { get; set; }

        public virtual TranslationLanguage TranslationLanguage { get; set; }
    }
}

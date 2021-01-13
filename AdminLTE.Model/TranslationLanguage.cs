using AdminLTE.Interface;

namespace AdminLTE.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TranslationLanguage")]
    public partial class TranslationLanguage : IEntity
    {
        public TranslationLanguage()
        {
            TranslationWords = new HashSet<TranslationWord>();
        }

        public int TranslationLanguageId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<TranslationWord> TranslationWords { get; set; }
    }
}

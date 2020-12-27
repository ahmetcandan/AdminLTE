namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TranslationLanguage")]
    public partial class TranslationLanguage
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

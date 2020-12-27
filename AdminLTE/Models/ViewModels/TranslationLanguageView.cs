namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TranslationLanguageView
    {
        public TranslationLanguageView()
        {
            TranslationWords = new List<TranslationWordView>();
        }

        public int TranslationLanguageId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public virtual IList<TranslationWordView> TranslationWords { get; set; }
    }
}

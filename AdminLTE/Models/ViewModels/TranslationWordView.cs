namespace AdminLTE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TranslationWordView
    {
        public TranslationWordView()
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

        public string LanguageCode { get; set; }
    }
}

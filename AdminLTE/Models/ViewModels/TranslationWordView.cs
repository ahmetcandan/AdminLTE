namespace AdminLTE.Models
{
    using System.ComponentModel.DataAnnotations;

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

namespace AdminLTE.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class KeyTypeView
    {
        public KeyTypeView()
        {
            KeyValues = new List<KeyValueView>();
        }

        public int KeyTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public IList<KeyValueView> KeyValues { get; set; }
    }
}

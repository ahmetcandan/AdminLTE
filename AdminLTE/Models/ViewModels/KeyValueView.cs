namespace AdminLTE.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class KeyValueView
    {
        public KeyValueView()
        {

        }

        public int KeyValueId { get; set; }

        public int KeyTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Key { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public virtual KeyTypeView KeyType { get; set; }
    }
}

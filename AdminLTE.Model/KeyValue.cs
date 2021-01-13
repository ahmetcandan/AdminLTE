using AdminLTE.Interface;

namespace AdminLTE.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KeyValue")]
    public partial class KeyValue : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KeyValue()
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

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual KeyType KeyType { get; set; }
    }
}

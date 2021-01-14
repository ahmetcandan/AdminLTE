using AdminLTE.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Model
{
    public abstract class TracingModel : ITracingEntity
    {
        [Required]
        [StringLength(50)]
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

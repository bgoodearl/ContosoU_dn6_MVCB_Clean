
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.Lookups
{
    public abstract class LookupBaseWith2cKey
    {
        [MaxLength(2)]
        [Required]
        public string Code { get; set; }

        [Required]
        public short LookupTypeId { get; protected set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        //SubType (discriminator column _SubType) was not a visible model property with EF6
        public short SubType { get; protected set; }
    }
}

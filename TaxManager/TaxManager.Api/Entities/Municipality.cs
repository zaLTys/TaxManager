using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxManager.Api.Entities
{
    public class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public ICollection<TaxEntry> TaxEntries { get; set; }
            = new List<TaxEntry>();

        public Municipality(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Municipality()
        {
            
        }
    }

}


using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TaxManager.Core.Models
{
    public class Municipality
    {

        public Municipality(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}

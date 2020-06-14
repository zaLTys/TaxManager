namespace TaxManager.Api.Models
{
    public class MunicipalityDto
    {

        public MunicipalityDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}

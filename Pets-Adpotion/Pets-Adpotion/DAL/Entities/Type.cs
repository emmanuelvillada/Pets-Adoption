namespace Pets_Adpotion.DAL.Entities
{
    public class Type
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}

namespace Domain.Plants
{
    public class Plant
    {
        public PlantId Id { get; private set; }
        public string CommonName { get; private set; } = string.Empty;

        private Plant() { }
        public Plant(PlantId id, string commonName)
        {
            Id = id;
            CommonName = commonName;
        }
        public void Update(string commonName)
        {
            CommonName = commonName;
        }
    }
}

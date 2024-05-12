namespace ShoeSalesAPI.Models
{
    public class ShoeStoreDatabaseSettings
    {
    public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ShoesCollectionName { get; set; } = null!;
    }
}

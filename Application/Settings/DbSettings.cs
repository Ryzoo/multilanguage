namespace Application.Settings
{
    public class DbSettings
    {
        public const string Name = "DbSettings";
        
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
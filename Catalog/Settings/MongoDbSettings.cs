namespace Catalog.Settings {
    public class mongoDbSettings {
        public string Host { get; set; }

        public string Port { get; set; }

        public string ConnectionString { get{
            return $"mongodb://{Host}:{Port}";
        }}
    }
}
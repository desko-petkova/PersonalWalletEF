using System.Text.Json;


namespace PersonalWallet.Infrastructure
{
    public class FileStorage
    {
        private readonly string path;

        public FileStorage(string path)
        {
            this.path = path;
        }

        public WalletStorage Load()
        {
            if (!File.Exists(path)) {
                return new WalletStorage(); }

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<WalletStorage>(json);
            if (storage == null)
                throw new Exception("Deserialization returned null.");


            return storage;
                        
        }

        public void Save(WalletStorage storage)
        { 

            var json = JsonSerializer.Serialize(storage);
            
            File.WriteAllText(path, json);
        }
    }
}

    
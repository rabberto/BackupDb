namespace BackupDb.Models.File
{
    public class PhysicalFile
    {
        public PhysicalFile(string name, string path, string base64)
        {
            Name = name;
            Path = path;
            Base64 = base64;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Base64 { get; private set; }
    }
}

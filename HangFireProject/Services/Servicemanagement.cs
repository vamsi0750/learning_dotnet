namespace HangFireProject.Services
{
    public class Servicemanagement : IServicemanagement
    {
        public void GenerateMerchandise()
        {
            Console.WriteLine($"Generate Merchandise: Long running task {DateTime.Now.ToString("G")}");
        }

        public void SendMail()
        {
            Console.WriteLine($"Send Email: short running task {DateTime.Now.ToString("G")}");
        }

        public void SyncData()
        {
            Console.WriteLine($"Sync Data: short running task {DateTime.Now.ToString("G")}");
        }

        public void UpdateDatebase()
        {
            Console.WriteLine($"Update Databse: Long running task {DateTime.Now.ToString("G")}");
        }
    }
}

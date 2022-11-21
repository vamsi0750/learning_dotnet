namespace HangFireProject.Services
{
    public interface IServicemanagement
    {
        void SendMail();
        void UpdateDatebase();
        void GenerateMerchandise();
        void SyncData();
    }
}

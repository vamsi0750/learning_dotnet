namespace HangFireProject.Models
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long DriverNumber { get; set; }
        public long Status { get; set; }
    }
}

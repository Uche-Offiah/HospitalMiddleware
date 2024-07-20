namespace HospitalMiddleware.Model
{
    public class Hospital
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}

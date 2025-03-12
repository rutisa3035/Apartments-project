namespace Apartments.Models
{
    public class PatientPostModel
    {
        public string Name { get; set; }

        public int Phone_number { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public int Preferred_area { get; set; }
        public int ApartmentId { get; set; }
        public int BrokerId { get; set; }
        public string Password { get; set; }
    }
}

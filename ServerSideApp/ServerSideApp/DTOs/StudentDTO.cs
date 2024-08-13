namespace ServerSideApp.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string IsraeliID { get; set; }  

        public int CityId { get; set; }
    }
}

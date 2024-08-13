using ServerSideApp.DTOs;

namespace ServerSideApp.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentDTO>>> GetAllStudents();

        Task<ServiceResponse<StudentDTO>> GetStudentById(int id);

        Task<ServiceResponse<int>> CreateStudent(StudentDTO newStudent);

      
    }
}

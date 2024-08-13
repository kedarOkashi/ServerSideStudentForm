using ServerSideApp.DataContext;
using ServerSideApp.DTOs;

namespace ServerSideApp.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DbContext _dbContext;

        public StudentService(DbContext dbContext)
        {
            _dbContext= dbContext;
        }


        public async Task<ServiceResponse<List<StudentDTO>>> GetAllStudents()
        {
            var students = await _dbContext.GetAllStudents();
            return students;

        }

        public async Task<ServiceResponse<StudentDTO>> GetStudentById(int id)
        {
            var student = await _dbContext.GetStudentById(id);
            return student;
        }

        public async Task<ServiceResponse<int>> CreateStudent(StudentDTO newStudent)
        {
            var newStudentId = await _dbContext.CreateStudent(newStudent);
            return newStudentId;
        }
    }
}

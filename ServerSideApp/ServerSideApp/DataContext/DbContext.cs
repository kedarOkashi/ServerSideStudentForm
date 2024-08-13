using Microsoft.AspNetCore.Mvc;
using ServerSideApp.DTOs;
using ServerSideApp.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ServerSideApp.DataContext
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #region City Actions
        public async Task<ServiceResponse<List<CityDTO>>> GetAllCitys()
        {
            var dataTable = new DataTable();
            List<CityDTO> list = new List<CityDTO>();
            using (var connection = CreateConnection())
            {
                using (var command = new SqlCommand("SP_GetAllCitys", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }

                        foreach (DataRow row in dataTable.Rows)
                        {
                            CityDTO city = new CityDTO();
                            city.CityId = Convert.ToInt32(row["CityId"]);
                            city.CityName = row["CityName"].ToString();
                            list.Add(city);
                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        return new ServiceResponse<List<CityDTO>>()
                        {
                            Message = ex.Message,
                            Success = false
                        };
                    }
                }
            }
            return new ServiceResponse<List<CityDTO>>()
            {
                Data = list,
                Message = "succes to get citys.",
                Success = true
            };
        }

        #endregion

        #region Student Actions
        public async Task<ServiceResponse<List<StudentDTO>>> GetAllStudents()
        {
            var dataTable = new DataTable();
            List<StudentDTO> list = new List<StudentDTO>();
            using (var connection = CreateConnection())
            {
                using (var command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }

                        foreach (DataRow row in dataTable.Rows)
                        {
                            StudentDTO student = new StudentDTO();
                            student.Id = Convert.ToInt32(row["Id"]);
                            student.FirstName = row["FirstName"].ToString();
                            student.LastName = row["LastName"].ToString();
                            student.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
                            student.IsraeliID = row["IsraeliID"].ToString();
                            student.CityId = Convert.ToInt32(row["CityId"]);
                            list.Add(student);
                        }

                    }
                    catch (Exception ex)
                    {
                        // Write to log...
                        return new ServiceResponse<List<StudentDTO>>()
                        {
                            Message = ex.Message,
                            Success = false
                        };
                    }
                }
            }
            return new ServiceResponse<List<StudentDTO>>()
            {
                Data = list,
                Message = "succes to get students.",
                Success = true
            };
        }

        public async Task<ServiceResponse<StudentDTO>> GetStudentById(int id)
        {
            StudentDTO student = new StudentDTO();
            using (var connection = CreateConnection())
            {
                using (var command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                student.Id = reader.GetInt32("Id");
                                student.FirstName = reader.GetString("FirstName");
                                student.LastName = reader.GetString("LastName");
                                student.DateOfBirth = reader.GetDateTime("DateOfBirth");
                                student.IsraeliID = reader.GetString("IsraeliID");
                                student.CityId = reader.GetInt32("CityId");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // Write to log...
                        return new ServiceResponse<StudentDTO>()
                        {
                            Message = ex.Message,
                            Success = false
                        };
                    }

                }
            }
            return new ServiceResponse<StudentDTO>()
            {
                Data = student,
                Message = $"succes to get student with Id={student.Id}.",
                Success = true
            };

        }

        public async Task<ServiceResponse<int>> CreateStudent(StudentDTO newStudent)
        {
            // retrive the id of the new student after created. store procedure returns.
            int newStudentId;
            using (var connection = CreateConnection())
            {
                using (var command = new SqlCommand("SP_AddStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", newStudent.FirstName);
                    command.Parameters.AddWithValue("@LastName", newStudent.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", newStudent.DateOfBirth);
                    command.Parameters.AddWithValue("@IsraeliID", newStudent.IsraeliID);
                    command.Parameters.AddWithValue("@CityId", newStudent.CityId);
                    try
                    {
                        await connection.OpenAsync();
                        newStudentId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    }
                    catch (Exception ex)
                    {
                        // -1 for exception.
                        return new ServiceResponse<int> { Message = ex.Message, Success = false, Data = -1 };
                    }
                }
            }
            return new ServiceResponse<int>()
            {
                Data = newStudentId,
                Message = "Student has created successfully",
                Success = true
            };
        }

        #endregion
    }
}


using Microsoft.AspNetCore.Mvc;
using ServerSideApp.DataContext;
using ServerSideApp.DTOs;

namespace ServerSideApp.Services.CityService
{
    public class CityService : ICityService
    {

        private readonly DbContext _dbContext;

        
        public CityService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ServiceResponse<List<CityDTO>>> GetAllCitys()
        {
            var serviceResponseObj = await _dbContext.GetAllCitys();
            return serviceResponseObj;

        }
    }
}

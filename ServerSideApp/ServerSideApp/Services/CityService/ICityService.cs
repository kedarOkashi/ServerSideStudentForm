using Microsoft.AspNetCore.Mvc;
using ServerSideApp.DTOs;

namespace ServerSideApp.Services.CityService
{
    public interface ICityService
    {
        Task<ServiceResponse<List<CityDTO>>> GetAllCitys();
    }
}

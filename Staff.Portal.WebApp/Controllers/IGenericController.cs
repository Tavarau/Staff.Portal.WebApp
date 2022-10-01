using Staff.Portal.Models;

namespace Staff.Portal.WebApp.Controllers
{
    public interface IGenericController
    {
        public  Task<List<GenderModel>> GetGender();
        public Task<List<QualificationModel>> GetQualification();
    }
}

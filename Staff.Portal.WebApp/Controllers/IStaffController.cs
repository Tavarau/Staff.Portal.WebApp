using Staff.Portal.Models;
using System.Threading.Tasks;

namespace Staff.Portal.WebApp.Controllers
{
    public interface IStaffController
    {
        public Task<List<StaffModel>> GetStaffs(string emp);

        public Task<StaffModel> GetStaffDetail(string emp);

        public Task<bool> SaveStaff(string Option, StaffModel MyModel);

        public Task<bool> CheckEmploymentNumberIsUnique(string EmpNumber);
    }
}

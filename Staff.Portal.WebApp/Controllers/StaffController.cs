using Microsoft.Extensions.Options;
using Staff.Portal.Models;

namespace Staff.Portal.WebApp.Controllers;

public class StaffController : IStaffController
{


    public StaffController()
    {

    }
    public async Task<List<StaffModel>> GetStaffs(string emp)
    {

        HttpClient client = new();
        try
        {
            List<StaffModel> MyModel = new();
            HttpResponseMessage response = await client.GetAsync(APIController.URL + "Staff/GetStaffs?emp=" + emp + "");
            if (response.IsSuccessStatusCode)
            {
                MyModel = await response.Content.ReadAsAsync<List<StaffModel>>();
            }
            return MyModel;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            client.Dispose();
        }
    }

    public async Task<StaffModel> GetStaffDetail(string emp)
    {

        HttpClient client = new();
        try
        {
            StaffModel MyModel = new();
            HttpResponseMessage response = await client.GetAsync(APIController.URL + "Staff/GetStaffDetail?emp=" + emp + "");
            if (response.IsSuccessStatusCode)
            {
                MyModel = await response.Content.ReadAsAsync<StaffModel>();
            }
            return MyModel;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            client.Dispose();
        }
    }

    public async Task<bool> SaveStaff(string Option, StaffModel MyModel)
    {

        HttpClient client = new();
        try
        {

            HttpResponseMessage response = new();

            if (Option == "Delete")
            {
                response = await client.GetAsync(APIController.URL + "Staff/SaveStaff?Options=" + Option + "&EmpNumber=" + MyModel.employment_number + "");

            }
            else if (Option == "Add")
            {
                response = await client.GetAsync(APIController.URL + "Staff/SaveStaff?Options=" + Option + "&EmpNumber=" + MyModel.employment_number +
                   "&FirstName=" + MyModel.first_name + "&LastName=" + MyModel.last_name + "&DateofBirth=" +  MyModel.birth_date.Value.ToString("yyyy-MM-dd") + "&YearOfExperience=" + MyModel.years_work_experience + "&GenderID=" + MyModel.gender_id + "&QualificationID=" + MyModel.qualification_id + "");
            }

            else
            {
                response = await client.GetAsync(APIController.URL + "Staff/SaveStaff?Options=" + Option + "&EmpNumber=" + MyModel.employment_number + "&FirstName=" + MyModel.first_name + "&LastName=" + MyModel.last_name + "&DateofBirth=" + MyModel.birth_date + "");
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }
            return false;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            client.Dispose();
        }
    }



    public async Task<bool> CheckEmploymentNumberIsUnique(string Emp)
    {

        HttpClient client = new();
        try
        {
            HttpResponseMessage response = new();
            response = await client.GetAsync(APIController.URL + "Staff/CheckEmploymentNumberIsUnique?emp=" + Emp + "");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            client.Dispose();
        }
    }
}



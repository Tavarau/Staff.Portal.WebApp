using Staff.Portal.Models;

namespace Staff.Portal.WebApp.Controllers;

public class GenericController: IGenericController
{
  
    public GenericController()
    {

    }
    public async Task<List<GenderModel>> GetGender()
    {

         HttpClient client = new();
        try
        {
            List<GenderModel> MyGenderModel = new ();
            HttpResponseMessage response = await client.GetAsync(APIController.URL + "Generic/GetGender");
            if (response.IsSuccessStatusCode)
            {
                MyGenderModel = await response.Content.ReadAsAsync<List<GenderModel>>();
            }
            return MyGenderModel;

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

    public async Task<List<QualificationModel>> GetQualification()
    {
         HttpClient client = new ();
        try
        {
            List<QualificationModel> MyQualificationModel = new ();
            HttpResponseMessage response = await client.GetAsync(APIController.URL + "Generic/GetQualification");

            if (response.IsSuccessStatusCode)
            {
                MyQualificationModel = await response.Content.ReadAsAsync<List<QualificationModel>>();
            }
            return MyQualificationModel;

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

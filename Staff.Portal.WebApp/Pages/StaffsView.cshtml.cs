using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Staff.Portal.Models;
using Staff.Portal.WebApp.Controllers;
using System.Collections.Generic;

namespace Staff.Portal.WebApp.Pages;

public class StaffsViewModel : PageModel
{

    private readonly IStaffController _IStaffController;


    public string Successful = String.Empty;
    public string Message = String.Empty;
    [BindProperty]
    public StaffModel StaffDetails { get; set; } = null;
    [BindProperty]
    public StaffModel Staffs { get; set; } = new();
    [BindProperty]
    public IList<StaffModel> StaffList { get; set; }

    public StaffsViewModel(IGenericController MyGenericRepository, IStaffController iStaffController)
    {
        _IStaffController = iStaffController;
        StaffList = new List<StaffModel>();

        //Clear the Model.
        ModelState.Clear();
    }
    public async Task<IActionResult> OnGetAsync(string? id)
    {
       
        await GetStaffs();


        return Page();
    }
    public async Task<IActionResult> OnPostBtnSearchStaff_Click()
    {
        try
        {
            if (ModelState.IsValid)
                await GetStaffs();
        }
        catch (Exception ex)
        {
            ViewData["Message"] = ex.Message;
        }

        return Page();
    }
    public async Task<IActionResult> OnPostBtnUpdateStaff_Click()
    {
        try
        {
            Successful = "";
            if (ModelState.IsValid)
            {
                StaffValidator validationRules = new();


                ValidationResult validationResult = validationRules.Validate(StaffDetails);

                if (!validationResult.IsValid)
                {
                    Message += @"Record not updated <br \>";
                    foreach (ValidationFailure f in validationResult.Errors)
                        Message += @$"{f.ErrorMessage} <br \>";

                    Successful = "False";
                }
                else
                {

                    bool SaveSuccess = await _IStaffController.SaveStaff("Update", StaffDetails);

                    if (SaveSuccess)
                    {

                        Successful = "True";
                        Message = "Record successfully updated";

                    }
                    else
                    {
                        Successful = "False";
                        Message = "Record not saved";

                    }
                }

                await GetStaffs();
            }
        }
        catch (Exception ex)
        {
            ViewData["Message"] = ex.Message;

            Successful = "False";
        }

        return Page();
    }
    public async Task<IActionResult> OnPostBtnEditStaff_Click(string? id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //Update record 
                StaffDetails = await _IStaffController.GetStaffDetail(id);
            }


            await GetStaffs();


        }
        catch (Exception ex)
        {
            ViewData["Message"] = ex.Message;
        }

        return Page();
    }

    private async Task GetStaffs()
    {

        if (string.IsNullOrWhiteSpace(Staffs.employment_number))
            Staffs.employment_number = "";

        StaffList = await _IStaffController.GetStaffs(Staffs.employment_number);


    }

}

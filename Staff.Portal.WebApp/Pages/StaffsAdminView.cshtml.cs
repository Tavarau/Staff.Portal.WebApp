using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Staff.Portal.Models;
using Staff.Portal.WebApp.Controllers;
using System.Reflection.Metadata.Ecma335;
using System.Xml;

namespace Staff.Portal.WebApp.Pages;

public class StaffsAdminViewModel : PageModel
{
    private readonly IGenericController _IGenericRepository;
    private readonly IStaffController _IStaffController;
    public SelectList? Genders { get; set; }
    public SelectList? Qualification { get; set; }
    public string Successful = String.Empty;
    public string Message = String.Empty;
    [BindProperty]
    public StaffModel StaffDetails { get; set; } = new();
    [BindProperty]
    public StaffModel Staffs { get; set; } = new();
    [BindProperty]
    public IList<StaffModel>? StaffList { get; set; }

    public StaffsAdminViewModel(IGenericController MyGenericRepository, IStaffController iStaffController)
    {
        _IGenericRepository = MyGenericRepository;
        _IStaffController = iStaffController;

        StaffList = new List<StaffModel>();
    }
    public async Task<IActionResult> OnGetAsync()
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

    [Obsolete]
    public async Task<IActionResult> OnPostBtnAddStaff_Click()
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
                    Message += @"Record not saved <br \>";
                    foreach (ValidationFailure f in validationResult.Errors)
                        Message += @$"{f.ErrorMessage} <br \>";

                    Successful = "False";
                }
                else
                {
                    bool IsUnique = await _IStaffController.CheckEmploymentNumberIsUnique(StaffDetails.employment_number);

                    if (IsUnique)
                    {
                        bool SaveSuccess = await _IStaffController.SaveStaff("Add", StaffDetails);

                        if (SaveSuccess)
                        {

                            Successful = "True";
                            Message = "Record successfully saved";



                        }
                        else
                        {
                            Successful = "False";
                            Message = "Record not saved";
                        }
                    }
                    else
                    {
                        Successful = "False";
                        Message = "Employment number should be unique";
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

    public async Task<IActionResult> OnPostBtnDeleteStaff_Click(string? id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //Delete Record
                StaffModel _Staff = new();
                _Staff.employment_number = id;
                bool Success = await _IStaffController.SaveStaff("Delete", _Staff);

                if (Success)
                {
                    Message = "Record deleted successfully";
                    Successful = "True";
                }
                else
                {
                    Message = "Record not deleted";
                    Successful = "False";
                }

                await GetStaffs();


            }
        }
        catch (Exception ex)
        {
            ViewData["Message"] = ex.Message;
        }

        return Page();
    }

    private async Task GetStaffs()
    {
        List<GenderModel> MyGenderModel = await _IGenericRepository.GetGender();
        this.Genders = new SelectList(MyGenderModel, "gender_id", "gender_description");

        List<QualificationModel> MyQualificationModel = await _IGenericRepository.GetQualification();
        this.Qualification = new SelectList(MyQualificationModel, "qualification_id", "qualification_description");


        if (string.IsNullOrWhiteSpace(Staffs.employment_number))
            Staffs.employment_number = "";

        StaffList = await _IStaffController.GetStaffs(Staffs.employment_number);
    }
}
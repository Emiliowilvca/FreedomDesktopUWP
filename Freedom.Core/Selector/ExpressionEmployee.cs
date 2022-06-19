using Freedom.Frontend.Models.Bindable;
using Freedom.Utility.Models.FullDto;
using System;

namespace Freedom.Core
{
    public sealed class ExpressionEmployee
    {
        private static ExpressionEmployee _instance;

        public static ExpressionEmployee Instance()
        {
            if (_instance == null)
            {
                _instance = new ExpressionEmployee();
            }
            return _instance;
        }

        public Func<EmployeeDtoFull, EmployeeBind> ConvertToEmployeeBind = x => new EmployeeBind()
        {
            ActiveWorked = x.ActiveWorked,
            Address = x.Address,
            Barcode = x.Barcode,
            BirtDate = x.BirtDate,
            BloodType = x.BloodType,
            ChildCount = x.ChildCount,
            CityId = x.CityId,
            CivilStatus = x.CivilStatus,
            CommissionCollection = x.CommissionCollection,
            CommissionSales = x.CommissionSales,
            CompanyId = x.CompanyId,
            DrivingLicenceNum = x.DrivingLicenceNum,
            EMail = x.EMail,
            EmergencyContact1 = x.EmergencyContact1,
            EmergencyContact2 = x.EmergencyContact2,
            FatherName = x.FatherName,
            Id = x.Id,
            Identity = x.Identity,
            IpsEmployer = x.IpsEmployer,
            IpsWorked = x.IpsWorked,
            IsCommissionAgent = x.IsCommissionAgent,
            JobPostId = x.JobPostId,
            JobSectorId = x.JobSectorId,
            MotherName = x.MotherName,
            Name = x.Name,
            Nick = x.Nick,
            Obs = x.Obs,
            PassportNum = x.PassportNum,
            Phone = x.Phone,
            PhoneMobile = x.PhoneMobile,
            PlaceBirt = x.PlaceBirt,
            Profession = x.Profession,
            Salary = x.Salary,
            Sex = x.Sex,
            SpouceCI = x.SpouceCI,
            SpouceName = x.SpouceName,
            Supervisor = x.Supervisor,
            WorkStarDate = x.WorkStarDate
        };
    }
}
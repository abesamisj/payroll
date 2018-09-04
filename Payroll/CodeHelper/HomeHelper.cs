using Payroll.Code;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.CodeHelper
{
    public class HomeHelper : EmployeeBL
    {
        public void CreateEmployee(EmployeeListModel viewModel)
        {
            UserPersonalInformation toDB = new UserPersonalInformation();
            toDB.Active = 1;
            toDB.Address = viewModel.Address;
            toDB.EmployeeId = viewModel.EmployeeId;
            toDB.FirstName = viewModel.FirstName;
            toDB.LastName = viewModel.LastName;
            toDB.Position = viewModel.Position;
            toDB.BasicPay = viewModel.BasicPay;
            InsertEmployee(toDB);
        }

        public void UpdateEmployee(EmployeeListModel viewModel)
        {

            int active = 0;
            if (viewModel.Active)
            {
                active = 1;
            }
            UserPersonalInformation toDB = new UserPersonalInformation();
            toDB.Active = active;
            toDB.Address = viewModel.Address;
            toDB.EmployeeId = viewModel.EmployeeId;
            toDB.FirstName = viewModel.FirstName;
            toDB.LastName = viewModel.LastName;
            toDB.Position = viewModel.Position;
            toDB.BasicPay = viewModel.BasicPay;
            toDB.UserPersonalInformationId = viewModel.UserPersonalInformationID;

            UpdateEmployee(toDB);
        }

        public EmployeeListModel GetEmployee(int id)
        {
            EmployeeListModel viewModel = new EmployeeListModel();
            UserPersonalInformation fromDB = GetEmployeesById(id);
            DepartmentBL department = new DepartmentBL();
            bool active = false;

            if (fromDB.Active > 0)
            {
                active = true;
            }

            if (fromDB != null)
            {
                viewModel = new EmployeeListModel();
                viewModel.Active = active;
                viewModel.Address = fromDB.Address;
                viewModel.EmployeeId = fromDB.EmployeeId;
                viewModel.FirstName = fromDB.FirstName;
                viewModel.LastName = fromDB.LastName;
                viewModel.Position = fromDB.Position;
                viewModel.BasicPay = fromDB.BasicPay;
                viewModel.UserPersonalInformationID = fromDB.UserPersonalInformationId;
                if (fromDB.DepartmentId > 0)
                {
                    viewModel.Department = department.GetDepartmentById(fromDB.DepartmentId).DepartmentName;
                }
            }

            return viewModel;
        }

        public List<AssignedEmployeeIncomesViewModel> AssignIncome(int id, out string fullName)
        {
            List<AssignedEmployeeIncomesViewModel> assignEmployeeIncomeViewModelList = new List<AssignedEmployeeIncomesViewModel>();

            IncomeBL incomeBL = new IncomeBL();
            Income income = new Income();
            AssingedEmployeeIncomeBL assingedEmployeeIncomeBL = new AssingedEmployeeIncomeBL();


            AssignedEmployeeIncomesViewModel assignEmployeeIncomeViewModel = new AssignedEmployeeIncomesViewModel();


            UserPersonalInformation employee = GetEmployeesById(id);
            fullName = employee.FirstName + " " + employee.LastName;

            List<AssignedEmployeeIncome> fromDB = assingedEmployeeIncomeBL.GetAssignedEmployeeIncomes(id);

            if (fromDB != null)
            {
                foreach (var item in fromDB)
                {
                    assignEmployeeIncomeViewModel = new AssignedEmployeeIncomesViewModel();
                    assignEmployeeIncomeViewModel.Name = fullName;
                    assignEmployeeIncomeViewModel.AssignedEmployeeIncomeId = item.AssignedEmployeeIncomeID;
                    //assignEmployeeIncomeViewModel.CustomIncomeAmount = item.CustomAmount;
                    assignEmployeeIncomeViewModel.IncomeAmount = item.IncomeAmount;
                    assignEmployeeIncomeViewModel.IncomeId = item.IncomeId;
                    income = incomeBL.GetIncomesById(item.IncomeId);
                    assignEmployeeIncomeViewModel.IncomeName = income.IncomeName;
                    assignEmployeeIncomeViewModel.UserPersonalInformationId = item.UserPersonalInformationID;

                    assignEmployeeIncomeViewModelList.Add(assignEmployeeIncomeViewModel);
                }

            }

            return assignEmployeeIncomeViewModelList;
        }

        public List<AssignedEmployeeDeductionsViewModel> AssignDeduction(int id, out string fullName)
        {
            List<AssignedEmployeeDeductionsViewModel> assignEmployeeDeductionsViewModelList = new List<AssignedEmployeeDeductionsViewModel>();

            DeductionBL deductionBL = new DeductionBL();
            Deduction deduction = new Deduction();
            AssignedEmployeeDeductionBL assingedEmployeeDeductionBL = new AssignedEmployeeDeductionBL();


            AssignedEmployeeDeductionsViewModel assignEmployeeDeductionViewModel = new AssignedEmployeeDeductionsViewModel();


            UserPersonalInformation employee = GetEmployeesById(id);
            fullName = employee.FirstName + " " + employee.LastName;

            List<AssignedEmployeeDeduction> fromDB = assingedEmployeeDeductionBL.GetAssignedEmployeeDeductions(id);

            if (fromDB != null)
            {
                foreach (var item in fromDB)
                {
                    assignEmployeeDeductionViewModel = new AssignedEmployeeDeductionsViewModel();
                    assignEmployeeDeductionViewModel.Name = fullName;
                    assignEmployeeDeductionViewModel.AssignedEmployeeDeductionsId = item.AssignedEmployeeDeductionID;
                    //assignEmployeeDeductionViewModel.CustomIncomeAmount = item.CustomAmount;
                    assignEmployeeDeductionViewModel.DeductionAmount = item.DeductionAmount;
                    assignEmployeeDeductionViewModel.DeductionId = item.DeductionId;
                    deduction = deductionBL.GetDeductionById(item.DeductionId);
                    assignEmployeeDeductionViewModel.DeductionName = deduction.DeductionName;
                    assignEmployeeDeductionViewModel.UserPersonalInformationId = item.UserPersonalInformationID;

                    assignEmployeeDeductionsViewModelList.Add(assignEmployeeDeductionViewModel);
                }

            }

            return assignEmployeeDeductionsViewModelList;
        }
    }
}
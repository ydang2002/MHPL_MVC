using DCT1205.Entity;
using DCT1205.Models;
using DCT1205.persistence;
using DCT1205.Services;
using DCT1205.Services.implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DCT1205.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeSevice _employeeService;
        private IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeSevice employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                ImageUrl = employee.ImageUrl,
                DOB = employee.DOB,
                Gender = employee.Gender,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                City = employee.City,
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateEmployeeViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    Postcode = model.Postcode,
                    Designation = model.Designation,
                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {

                    var uploadDir = @"images";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                    await _employeeService.CreateAsSync(employee);
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        /*[HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var model = _employeeService.GetById(id);
            _employeeService.UpdateAsSync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    Postcode = model.Postcode,
                    Designation = model.Designation,
                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {

                    var uploadDir = @"images/employees";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                    await _employeeService.UpdateAsSync(employee);
                    return RedirectToAction("Index");
                }

            }
            return View();
        }*/

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EditEmployeeViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                Phone = employee.Phone,
                Postcode = employee.Postcode,
                Designation = employee.Designation,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeViewModel model) 
        {
            var employee = _employeeService.GetById(model.Id);
            if (employee == null) { return NotFound(); }

            employee.Id = model.Id;
            employee.EmployeeNo = model.EmployeeNo;
            employee.FirstName = model.FirstName;
            employee.MiddleName = model.MiddleName;
            employee.LastName = model.LastName;
            employee.FullName = model.FullName;
            employee.Gender = model.Gender;
            employee.Email = model.Email;
            employee.DOB = model.DOB;
            employee.DateJoined = model.DateJoined;
            employee.NationalInsuranceNo = model.NationalInsuranceNo;
            employee.PaymentMethod = model.PaymentMethod;
            employee.StudentLoan = model.StudentLoan;
            employee.UnionMember = model.UnionMember;
            employee.Address = model.Address;
            employee.City = model.City;
            employee.Phone = model.Phone;
            employee.Postcode = model.Postcode;
            employee.Designation = model.Designation;

            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {

                var uploadDir = @"images";
                var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                var extension = Path.GetExtension(model.ImageUrl.FileName);
                var webRootPath = _webHostEnvironment.WebRootPath;
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                await _employeeService.UpdateAsSync(employee);
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new DeleteEmployeeViewModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmployeeNo = employee.EmployeeNo
            };

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
               await _employeeService.DeleteAsSync(model.Id);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new DetailEmployeeViewModel
            {

                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                Phone = employee.Phone,
                Email = employee.Email,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                ImageUrl = employee.ImageUrl,
                Postcode = employee.Postcode
            };
            return View(model);
        }
    }
}

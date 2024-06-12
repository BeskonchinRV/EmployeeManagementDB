using EmployeeManagementDB.Models.DBModels;

namespace EmployeeManagementDB.ViewModel
{
    public class HomeViewModel
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Status { get; set; }
        public string? RoleName { get; set; }
        public string? Login { get; set; }
    }
}

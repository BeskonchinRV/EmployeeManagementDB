namespace EmployeeManagementDB.Models.Panel
{
    public class ItemPanel
    {
        public string Controller = "Home";
        public string Action { get; set; }
        public string Label { get; set; }
        public ItemPanel(string controller, string action, string label)
        {
            Controller = controller;
            Action = action;
            Label = label;
        }
    }
}

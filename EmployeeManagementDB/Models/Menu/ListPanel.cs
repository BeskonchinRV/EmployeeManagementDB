namespace EmployeeManagementDB.Models.Panel
{
    public class ListPanel
    {
        public List<ItemPanel> Panel = new List<ItemPanel>()
        {
            new ItemPanel("Home","Index","Главная"),
            new ItemPanel("Employees", "Index", "Сотрудники"),
            new ItemPanel("Organizations", "Index", "Организации"),
            new ItemPanel("Authorization", "Authorization", "Авторизация"),
        };
        public List<ItemPanel> UserPanel = new List<ItemPanel>()
        {
            new ItemPanel("Home","Index","Главная"),
            new ItemPanel("Organizations", "Index", "Организации"),
            new ItemPanel("Employees", "Index", "Сотрудники"),
            new ItemPanel("Authorization", "Logout", "Выйти"),
        };
        public List<ItemPanel> AdminPanel = new List<ItemPanel>()
        {
            new ItemPanel("Home","Index","Главная"),
            new ItemPanel("Organizations", "Index", "Организации"),
            new ItemPanel("Employees", "Index", "Сотрудники"),
            new ItemPanel("UserAccounts", "Index", "Аккаунты"),
            new ItemPanel("Authorization", "Logout", "Выйти"),
        };
    }
}

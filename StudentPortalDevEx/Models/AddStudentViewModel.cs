namespace StudentPortalDevEx.Models
{
    public class AddStudentViewModel
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Subscribed { get; set; }

    }
}

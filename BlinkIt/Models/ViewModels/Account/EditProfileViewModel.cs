namespace BlinkIt.Models.ViewModels.Account
{
    public class EditProfileViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? File { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}

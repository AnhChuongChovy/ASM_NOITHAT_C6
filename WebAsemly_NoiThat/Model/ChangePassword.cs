namespace WebAsemly_NoiThat.Model
{
    public class ChangePassword
    {
        public int AccountId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

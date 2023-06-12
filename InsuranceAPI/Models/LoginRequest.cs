using System.Text.Json.Serialization;

namespace InsuranceAPI.Models {
    public class LoginRequest {
        [JsonInclude]
        public string Username { get; set; } = null!;
        [JsonInclude]
        public string Password { get; set; } = null!;

        public LoginRequest(string username, string password) {
            this.Username = username;
            this.Password = password;
        }
    }
}

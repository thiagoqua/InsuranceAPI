using System.Text.Json.Serialization;

namespace InsuranceAPI.Models {
    public class LoginRequest {
        [JsonInclude]
        public string username;
        [JsonInclude]
        public string password;

        public LoginRequest(string username, string password) {
            this.username = username;
            this.password = password;
        }
    }
}

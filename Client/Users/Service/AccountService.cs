using System.Net.Http.Json;

namespace Client.Users.Service;

public class AccountService(HttpClient client) : IAccountService
{
    public async Task<HttpResponseMessage> Login(LoginModel login)
    {
        return await client.PostAsJsonAsync("api/account/login", login);
    }
}

public interface IAccountService
{
    Task<HttpResponseMessage> Login(LoginModel login);
}
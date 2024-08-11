using System.Net.Http.Json;

namespace BootstrapClient.Users;

public class AccountService(HttpClient client, ILocalStorageService localStorageService)
{
    public async Task Login(LoginModel login)
    {
        var response = await client.PostAsJsonAsync("api/account/login", login);
        
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>() ?? throw new Exception("Invalid response from server");
            await localStorageService.SetItemAsync("user", user);
            CurrentUser = user;
            Console.WriteLine(user.Username + " " + user.Token);
        }
        else
            throw new ArgumentException(response.Content.ReadAsStringAsync().Result);
    }
    
    public async Task Register(RegisterModel register)
    {
        var response = await client.PostAsJsonAsync("api/account/register", register);
        
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>() ?? throw new Exception("Invalid response from server");
            await localStorageService.SetItemAsync("user", user);
            CurrentUser = user;
            Console.WriteLine(user.Username + " " + user.Token);
        }
        else
            throw new ArgumentException(response.Content.ReadAsStringAsync().Result);
    }

    private UserDto? _currentUser;

    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync("user");
        CurrentUser = null;
    }

    public event Action? OnChange;

    public UserDto? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnChange?.Invoke();
        }
    }
}
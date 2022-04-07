using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ToDoApi.Models;
using ToDoApi.Settings;
using static System.DateTime;

namespace ToDoApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;
    private readonly string _jwtSecret;

    public UsersService(IOptions<UsersSettings> usersSettings)
    {
        var mongoClient = new MongoClient(usersSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(usersSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>(usersSettings.Value.UsersCollectionName);
        _jwtSecret = usersSettings.Value.JwtSecret;
    }

    public async Task<List<User>> GetAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }

    public async Task<User?> GetAsync(string id)
    {
        return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
    }

    public async Task UpdateAsync(string id, User user)
    {
        await _usersCollection.ReplaceOneAsync(u => u.Id == id, user);
    }

    public async Task DeleteAsync(string id)
    {
        await _usersCollection.DeleteOneAsync(user => user.Id == id);
    }

    public async Task<string?> Authenticate(LoginCredentials credentials)
    {
        string email = credentials.Email;
        string password = credentials.Password;
        var user = await _usersCollection.FindAsync(x => x.Email == email && x.Password == password);
        if (user is null)
        {
            return null;
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes("chrbei8h9f3iuonf3ufdoujoidjowdoidjoicnwdwewdrw");
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, email)
            }),
            Expires = UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}
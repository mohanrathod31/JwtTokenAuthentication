using BasicJWTAuth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BasicJWTAuth.Repository
{
    public class JWTManagerRepository :IJWTManagerRepository
    {
		Dictionary<string, string> UsersRecords = new Dictionary<string, string>
	{
		{ "user1","password1"},
		{ "user2","password2"},
		{ "user3","password3"},
	};

		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration)
		{
			this.iconfiguration = iconfiguration;
		}
		public Tokens Authenticate(Users users)
		{

			var json = System.IO.File.ReadAllText(@"C:\Users\rathomo\source\repos\BasicAuthenticationProj\BasicAuthenticationProj/CheckLists.json");

			dynamic jsonObject = JsonConvert.DeserializeObject<User>(json);

			List<UserModel> userlist = jsonObject.users;

			if (!(userlist.Any(a => a.userid == users.Name) && userlist.Any(a => a.password == users.Password)))
			{
				return null;
			}

			string role = getUserRole(users.Name);

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);


			var tokenDescriptor = new SecurityTokenDescriptor
			{
			Subject = new ClaimsIdentity(new Claim[]
			  {
			     new Claim(ClaimTypes.Name, users.Name),
				 new Claim(ClaimTypes.Role, role)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}

        public string getUserRole(string username)
        {
			var json = System.IO.File.ReadAllText(@"C:\Users\rathomo\source\repos\BasicAuthenticationProj\BasicAuthenticationProj/CheckLists.json");

			dynamic jsonObject = JsonConvert.DeserializeObject<User>(json);

			List<UserModel> userlist = jsonObject.users;

			string role = "";

			foreach(var user in userlist)
            {
				if (user.userid == username)
					role = user.role;
            }

			return role;
		}
    }
}

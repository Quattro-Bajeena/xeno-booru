using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.Services
{

	public enum AuthenticationLevel { Admin, Moderator};

	public class AuthenticationService
	{
		private readonly string authentication_key = "authenticated";
		//private const string admin_key = "admin_authenticated";
		//private const string moderator_key = "moderator_authenticated";
		//private readonly Dictionary<AuthenticationLevel, string> authStrings = new()
		//{
		//	{AuthenticationLevel.Admin, admin_key },
		//	{AuthenticationLevel.Moderator, moderator_key }
		//};

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
		private readonly IOptions<AppConfig> _config;

		public AuthenticationService(IHttpContextAccessor context, ITempDataDictionaryFactory tempDataDictionaryFactory, IOptions<AppConfig> config)
		{
			_httpContextAccessor = context;
			_config = config;
			_tempDataDictionaryFactory = tempDataDictionaryFactory;
		}


		public bool CheckAuthentication(string action)
		{
			_config.Value.AuthenticationRequired.TryGetValue(action, out bool authRequired);
			if (authRequired == false)
            {
				return true;
            }

			var httpContext = _httpContextAccessor.HttpContext;
			var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

			bool authorized = Convert.ToBoolean(httpContext.Session.GetInt32(authentication_key) ?? 0);


			if(authorized == false)
			{
				tempData["AuthFailure"] = true;
			}
			return authorized;
		}

		public static bool CheckAuthFailure(ITempDataDictionary tempData)
		{
			return tempData["AuthFailure"] != null && (bool)tempData["AuthFailure"] == true;
		}

		public bool Authenticate(string password)
		{
			var httpContext = _httpContextAccessor.HttpContext;

			bool authorized = false;
			if (_config.Value.Passwords.Contains(password))
			{
				httpContext.Session.SetInt32(authentication_key, 1);
				authorized = true;
			}
			else
			{
				httpContext.Session.SetInt32(authentication_key, 0);
			}
			return authorized;
		}

		public string GetIp()
		{
			var httpContext = _httpContextAccessor.HttpContext;
			var request = httpContext.Request;

			string remoteIpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
			if (request.Headers.ContainsKey("X-Forwarded-For"))
				remoteIpAddress = request.Headers["X-Forwarded-For"];

			return remoteIpAddress;
		}

	}
}

using CoreLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class Recaptcha : IRecaptcha
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _accessor;
		public Recaptcha(IConfiguration configuration, IHttpContextAccessor accessor)
		{
			_configuration = configuration;
			_accessor = accessor;
		}
		public async Task<bool> IsSatisfy()
		{
			var SecretKey = _configuration.GetSection("GoogleReCAPTCHA")["SecretKey"];
			var response = _accessor.HttpContext.Request.Form["g-recaptcha-response"];
			var http = new HttpClient();
			var resault = await http.PostAsync(
				$" https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={response}", null);
			if (resault.IsSuccessStatusCode)
			{
				var googleResponse =
					JsonConvert.DeserializeObject<RecaptchaResponseModel>(await resault.Content.ReadAsStringAsync());
				//var res = googleResponse.Success;
				return googleResponse.Success;
			}
			return false;
		}
	}
	public class RecaptchaResponseModel
	{
		[JsonProperty("success")]
		public bool Success { get; set; }
		[JsonProperty("challenge_ts")]
		public DateTimeOffset Challenge_Ts { get; set; }
		[JsonProperty("hostname")]
		public string HostName { get; set; }
	}
}

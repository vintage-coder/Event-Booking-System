


using Google.Apis.Auth;

namespace EBSystem.Authentication.API.Helpers
{
    public class GoogleHelper
    {

		private readonly IConfiguration _configuration;
		private readonly IConfigurationSection _goolgeSettings;
		public GoogleHelper(IConfiguration configuration)
        {
			_configuration = configuration;

			_goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
		}
		
		public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
		{
			try
			{
				var settings = new GoogleJsonWebSignature.ValidationSettings()
				{
					Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
				};

				var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
				return payload;
			}
			catch (Exception ex)
			{
				//log an exception
				return null;
			}
		}

	}
}

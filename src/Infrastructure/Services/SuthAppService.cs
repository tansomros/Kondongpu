using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Infrastructure.Services
{
	public class SuthAppService : ISuthAppService
	{
		private readonly HttpClient _suthAppHttpClient;
		private readonly HttpClient _publicIdentityHttpClient;

		public SuthAppService(IHttpClientFactory httpClientFactory)
		{
			_suthAppHttpClient = httpClientFactory.CreateClient("SUTH-App");
			_publicIdentityHttpClient = httpClientFactory.CreateClient("SUTH-Public-Identity");
		}

        public Task NotifyCallCheckup(string hospitalNumber, string checkup, string location, int roomService)
        {
            throw new NotImplementedException();
        }
    }
}

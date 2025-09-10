namespace SchoolApiGW.Helper
{
    public class ClientIdHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientIdHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.User.Identity?.IsAuthenticated == true)
            {
                var clientId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (!string.IsNullOrEmpty(clientId) && !request.Headers.Contains("X-Client-Id"))
                {
                    request.Headers.Add("X-Client-Id", clientId);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}

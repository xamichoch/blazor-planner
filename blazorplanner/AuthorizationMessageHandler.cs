﻿using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Blazored.LocalStorage;
using System;

namespace blazor_planner
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storage;

        public AuthorizationMessageHandler(ILocalStorageService storage)
        {
            _storage = storage;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                var token = await _storage.GetItemAsStringAsync("access_token");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            Console.WriteLine("Authorization Message Handler Called");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

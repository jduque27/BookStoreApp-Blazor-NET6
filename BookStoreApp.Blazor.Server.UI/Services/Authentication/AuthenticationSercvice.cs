﻿using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationSercvice : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationSercvice(
            IClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider
        )
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);
            //store Token
            localStorage.SetItemAsync("accessToken", response.Token);

            //Change auth statepf app
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}

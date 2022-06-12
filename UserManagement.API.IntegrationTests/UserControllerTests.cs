using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserManagement.API.Commands;
using UserManagement.API.Models;
using UserManagement.API.Models.Core;
using UserManagement.API.Queries;
using UsserManagement.Infrastructure;
using Xunit;

namespace UserManagement.API.IntegrationTests
{
    public class UserControllerTests : IClassFixture<TestFixture>, IDisposable
    {
        private readonly HttpClient client;
        private readonly TestFixture fixture;

        public UserControllerTests(TestFixture fixture)
        {
            this.client = fixture.Client;
            this.fixture = fixture;
        }

        private async Task RemoveUsers()
        {
            using (var scope = this.fixture.TestServer.Host.Services.CreateScope())
            {
                using (var conntext = scope.ServiceProvider.GetRequiredService<UserManagementContext>())
                {
                    string sql = @"delete from ""UserManagement"".""User"" u where u.""Username"" in ('Test', 'UpdateTest', 'RetrieveTest', 'DeleteTest')";

                    await conntext.Database.ExecuteSqlRawAsync(sql);
                }
            }
        }

        [Fact]
        public async Task GetList()
        {
            //Arrange
            var query = new GetUsersQuery();

            //Act
            var users = await this.client.GetUsers(query);

            //Assert
            Assert.NotNull(users);
        }

        [Fact]
        public async Task Create()
        {
            //Arrange
            var user = new CreateUserCommand()
            {
                Username = "Test",
                Email = "test@test.com",
                Password = "12345678"
            };

            //Act
            var response = await this.client.CreateUser(user);

            //Assert
            Assert.NotEqual(default, response.Id);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var user = new CreateUserCommand()
            {
                Username = "RetrieveTest",
                Email = "test@test.com",
                Password = "12345678"
            };
            var response = await this.client.CreateUser(user);

            //Act
            var retrieved = await this.client.GetUser(response.Id);

            //Assert
            Assert.NotNull(retrieved);
            Assert.Equal(user.Username, retrieved.Username);
            Assert.Equal(user.Email, retrieved.Email);
        }

        [Fact]
        public async Task Edit()
        {
            //Arrange
            var user = new CreateUserCommand()
            {
                Username = "UpdateTest",
                Email = "test@test.com",
                Password = "12345678"
            };
            var response = await this.client.CreateUser(user);
            var editCommand = new EditUserCommand()
            {
                Id = response.Id,
                Email = "test123@test.com"
            };

            //Act
            var result = await this.client.EditUser(editCommand);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task Delete()
        {
            //Create
            var user = new CreateUserCommand()
            {
                Username = "DeleteTest",
                Email = "test@test.com",
                Password = "12345678"
            };
            var response = await this.client.CreateUser(user);

            //Act
            var result = await this.client.DeleteUser(response.Id);

            //Assert
            Assert.True(result);
        }

        public async void Dispose()
        {
            await RemoveUsers();
        }
    }

    internal static class UserHttpClientExtensions
    {
        public static async Task<IEnumerable<UserView>> GetUsers(this HttpClient client, GetUsersQuery query)
        {
            var response = await client.GetAsync(UserLinks.GetList);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<UserView>>();
        }

        public static async Task<UserView> GetUser(this HttpClient client, long id)
        {
            var response = await client.GetAsync(UserLinks.GetUser(id));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<UserView>();
        }

        public static async Task<IdentityResponse> CreateUser(this HttpClient client, CreateUserCommand model)
        {
            var response = await client.PostAsJsonAsync(UserLinks.Create, model);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IdentityResponse>();
        }

        public static async Task<IdentityResponse> EditUser(this HttpClient client, EditUserCommand model)
        {
            var response = await client.PutAsJsonAsync(UserLinks.Update(model.Id), model);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IdentityResponse>();
        }

        public static async Task<bool> DeleteUser(this HttpClient client, long id)
        {
            var response = await client.DeleteAsync(UserLinks.Delete(id));

            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }

    internal static class UserLinks
    {
        public static string Root => "users";

        public static string GetList => Root;
        public static string GetUser(long id) => $"{Root}/{id}";
        public static string Create => Root;
        public static string Update(long id) => $"{Root}/{id}";
        public static string Delete(long id) => $"{Root}/{id}";
    }
}

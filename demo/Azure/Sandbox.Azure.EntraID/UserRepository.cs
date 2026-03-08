namespace Sandbox.Azure.EntraID
{
    using Microsoft.Graph;
    using Microsoft.Graph.Models;

    internal class UserRepository
    {
        private static readonly string[] SelectedFields = ["Id", "DisplayName", "GivenName", "Surname", "CreatedDateTime"];

        private readonly GraphServiceClient client;

        public UserRepository(GraphServiceClient client)
        {
            this.client = client;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                var user = await this.client.Users[id].GetAsync(opt =>
                {
                    opt.QueryParameters.Select = SelectedFields;
                });

                return user;
            }
            catch (ServiceException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var final = new List<User>();

                var users = await this.client.Users.GetAsync(opt =>
                {
                    opt.QueryParameters.Select = SelectedFields;
                });

                final.AddRange(users.Value);

                while (users.OdataNextLink != null)
                {
                    users = await this.client.Users.WithUrl(users.OdataNextLink).GetAsync();
                    final.AddRange(users.Value);
                }

                return final;
            }
            catch (ServiceException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                await this.client.Users[user.Id].PatchAsync(user);
            }
            catch (ServiceException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await this.client.Users.PostAsync(user);
            }
            catch (ServiceException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await this.client.Users[id].DeleteAsync();
            }
            catch (ServiceException ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}

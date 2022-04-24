using ChatApp.Domain.Interfaces;
using Microsoft.Azure.Cosmos;

namespace ChatApp.Data.Repositories;

public class UserRepository : CosmosRepository<Domain.Models.Users.User>, IUserRepository
{
    public UserRepository(CosmosClient cosmosClient) : base(cosmosClient) { }

    public override string DatabaseId => "UserDatabase";
    public override string ContainerId => "UserContainer";
}

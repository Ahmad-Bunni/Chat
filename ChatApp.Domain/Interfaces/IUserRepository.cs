using ChatApp.Domain.Models.Users;

namespace ChatApp.Domain.Interfaces;

public interface IUserRepository : ICosmosRepository<User> { }

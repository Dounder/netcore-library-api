using Core.Entities;

namespace Api.Interfaces;

public interface IAuthorsRepository
{
    // Crud methods
    Task<IEnumerable<object>> GetAuthors();
    Task<Author> GetAuthorById(Guid id);
    Task<Guid> CreateAuthor(Author author);
    ValueTask UpdateAuthor(Author author);
    Task<bool> DeleteAuthor(Guid id);
    // General methods
    Task<bool> IfAuthorExist(Guid id);
}

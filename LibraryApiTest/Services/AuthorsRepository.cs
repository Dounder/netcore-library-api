using Api.Interfaces;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly AppDbContext context;

    public AuthorsRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<object>> GetAuthors() => await context.Authors.ToListAsync();

    // SingleOrDefaultAsync() throws an exception if more than one match were found
    public async Task<Author> GetAuthorById(Guid id) => await context.Authors.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Guid> CreateAuthor(Author author)
    {
        var authorExist = await context.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);

        if (authorExist != null) return Guid.Empty; // If author exists return an empty guid

        context.Add(author);
        await context.SaveChangesAsync();
        return author.Id;
    }

    public async ValueTask UpdateAuthor(Author author)
    {
        context.Authors.Update(author);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAuthor(Guid id)
    {
        var author = await GetAuthorById(id);

        if (author == null) return false;

        author.Enabled = false;
        context.Authors.Update(author);
        await context.SaveChangesAsync();
        return true;
    }

    // General methods
    public async Task<bool> IfAuthorExist(Guid id) => await context.Authors.FirstOrDefaultAsync(x => x.Id == id) != null;

}

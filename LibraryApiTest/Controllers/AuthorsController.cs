using Api.DTOs;
using Api.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAuthorsRepository repository;

    public AuthorsController(IMapper mapper, IAuthorsRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetAuthors() => Ok(await repository.GetAuthors());

    [HttpGet("{id}", Name = "GetAuthorById")]
    public async Task<ActionResult<object>> GetAuthorById(Guid id)
    {
        var author = await repository.GetAuthorById(id);

        if (author == null) return new NotFoundObjectResult(new { 
            StatusCode = 404, message = $"Author with id {id} was not found." });

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<object>> CreateAuthor([FromBody] CreateAuthorDto createAuthor)
    {
        var author = mapper.Map<Author>(createAuthor);
        var authorId = await repository.CreateAuthor(author);
        var authorDto = mapper.Map<AuthorDto>(author);

        return authorId != Guid.Empty
            // If author doesn't exist return 201
            ? new CreatedAtRouteResult("GetAuthorById", new { id = authorId }, authorDto) 
            // If author exists return bad request with represantive message
            : new BadRequestObjectResult(new { StatusCode = 400, message = "The author already exists" });
    }

    [HttpPut("{id}", Name = "UpdateAuthor")]
    public async Task<ActionResult<object>> UpdateAuthor(Guid id, [FromBody] CreateAuthorDto updateAuthor)
    {
        var authorDB = await repository.GetAuthorById(id);

        if (authorDB == null) return new NotFoundObjectResult(new { 
            StatusCode = 404, message = $"Author with id {id} was not found." });

        authorDB = mapper.Map(updateAuthor, authorDB);
        await repository.UpdateAuthor(authorDB);
        var authorDto = mapper.Map<AuthorDto>(authorDB);

        return new OkObjectResult(new { 
            StatusCode = 200, message = "Author updated succesfully", author = authorDto });
    }

    [HttpDelete("{id}", Name = "DeleteAuthor")]
    public async Task<ActionResult> DeleteAuthor(Guid id)
    {
        var result = await repository.DeleteAuthor(id);

        return result
            ? Ok(new { StatusCode = 200, message = "Author deleted succesfully." })
            : new NotFoundObjectResult(new { StatusCode = 404, message = $"Author with id {id} was not found." });
    }
}

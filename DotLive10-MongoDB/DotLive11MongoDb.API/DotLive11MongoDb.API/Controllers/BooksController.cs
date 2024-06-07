using DotLive11MongoDb.API.Core.Entities;
using DotLive11MongoDb.API.Infrastructure.Persistence;
using DotLive11MongoDb.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotLive11MongoDb.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly MongoConfig _config;
    public BooksController(IOptions<MongoConfig> options)
    {
        _config = options.Value;
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model)
    {
        var book = new Book(model.Title, model.Author);

        var collection = GetCollection();
        collection.InsertOne(book);

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var collection = GetCollection();

        var books = collection.Find(new BsonDocument()).ToList();

        return Ok(books);
    }

    [HttpPost("{id}/reviews")]
    public IActionResult Post(string id, CreateBookReviewModel model)
    {
        var bookReview = new BookReview(model.Rating, model.Comment, model.Username);

        var filter = Builders<Book>.Filter.And(
            Builders<Book>.Filter.Eq(o => o.Id, id)
        );

        var definition = Builders<Book>.Update.Push(o => o.Reviews, bookReview);

        var collection = GetCollection();

        collection.UpdateOne(filter, definition);

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var collection = GetCollection();

        var result = collection.Find(o => o.Id.Equals(id)).FirstOrDefault();

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    private IMongoCollection<Book> GetCollection()
    {
        var client = new MongoClient(_config.ConnectionString);
        var database = client.GetDatabase(_config.Database);

        var collection = database.GetCollection<Book>("books");

        return collection;
    }
}

using System;
using System.Text.Json;
using DotLive10.API.Entities;
using DotLive10.API.Persistence;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Serilog;

namespace DotLive10.API.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductsController : ControllerBase
	{
        private readonly DotLive10DbContext _context;
		private readonly Serilog.ILogger _logger;
		private readonly TelemetryClient _telemetryClient;

        public ProductsController(DotLive10DbContext context)//, TelemetryClient telemetryClient)
		{
            _context = context;
			//_telemetryClient = telemetryClient;

			_logger = Log.ForContext<ProductsController>();
        }

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var product = _context.Products.SingleOrDefault(p => p.Id == id);

			if (product == null)
				return NotFound();

			return Ok(product);
		}

		[HttpPost]
		public IActionResult Post(Product product)
		{
			//using var operation = _telemetryClient.StartOperation<RequestTelemetry>("Serialize");

			var json = JsonSerializer.Serialize(product);

			//Thread.Sleep(500);

			//_telemetryClient.StopOperation(operation);

			//_telemetryClient.TrackEvent("CreatingProduct", new Dictionary<string, string> { { "payload", json } });

			_context.Products.Add(product);
			_context.SaveChanges();

			_logger.ForContext("CreatedBy", "Luis").ForContext("Payload", json).Information("Creating product");

			// throw new InvalidOperationException("Banco quebrou");

			return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
		}
	}
}


﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueApp1.Server.Models;

namespace VueApp1.Server.Controllers
{
    [Route("Person")]
    public class PersonController : Controller
    {
        public RepoContext _context;

        public PersonController(RepoContext context)
        {
            _context = context;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index()
            => Json(await _context.Persons.ToListAsync());

        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
            => Json(await _context.Persons.FindAsync(id));

        [HttpPut, Route("")]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            if (person.Id == 0)
            {
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
                return Created();
            }

            var record = await _context.Persons.FindAsync(person.Id)
                ?? throw new("Person not found");

            record.FirstName = person.FirstName;
            record.LastName = person.LastName;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Person person)
        {
            person.Id = id;
            return await Put(person);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            person.Id = 0;
            return await Put(person);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var person = await _context.Persons.FindAsync(id)
                ?? throw new("Person not found");
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

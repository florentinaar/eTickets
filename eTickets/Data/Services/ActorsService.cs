using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);

             _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }

      

        public async Task<Actor> UpdateAsync(Actor newActor)
        {

            //_context.Entry(actor).CurrentValues.SetValues(newActor);
            _context.Update(newActor);

            await _context.SaveChangesAsync();

            return newActor;
        }
    }
}

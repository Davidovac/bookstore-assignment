using System.Security.Policy;
using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Publisher = BookstoreApplication.Models.Publisher;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository
    {
        private AppDbContext _context;

        public PublishersRepository(AppDbContext context)
        {
            _context = context;
        }

        public Publisher? GetById(int id)
        {
            return _context.Publishers.Find(id);
        }

        public List<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public Publisher Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

        public Publisher Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
            return publisher;
        }


        public void Delete(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
        }
    }
}

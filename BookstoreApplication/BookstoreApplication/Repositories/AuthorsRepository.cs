using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository
    {
        private AppDbContext _context;

        public AuthorsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Author? GetById(int id)
        {
            return _context.Authors.Find(id);
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        public Author Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return author;
        }


        public void Delete(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}

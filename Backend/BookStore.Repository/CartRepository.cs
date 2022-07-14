using BookStore.Models.DataModels;
using BookStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        /*public List<Cart> GetCartItems(string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return query.ToList();
        }*/

        public ListResponse<Cart> GetCartList(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Cart> carts = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }

        public ListResponse<Cart> GetCartListall(int UserId)
        {

            var query = _context.Carts.Include(c => c.Book).Where(c => c.Userid == UserId).AsQueryable();

            int totalReocrds = query.Count();
            List<Cart> carts = query.ToList();

            return new ListResponse<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }

        /*
        public ListResponse<Cart> GetCartItems(int id, int pageindex = 1, int pagesize = 10)
        {
            var query = _context.Carts.Include(c => c.Book).Where(c => c.Userid.Equals(id)).AsQueryable();
            var total = query.Count();
            List<Cart> carts = query.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new ListResponse<Cart>()
            {
                TotalRecords = total,
                Results = carts
            };
        }*/

        public Cart GetCarts(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddCart(Cart category)
        {
            var entry = _context.Carts.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Cart UpdateCart(Cart category)
        {
            var entry = _context.Carts.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}

using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly BookDbContext _context;
        public LoaiRepository(BookDbContext context)
        {
            _context = context;
        }
        public LoaiVM Add(LoaiModel loai)
        {
            var _Loai = new Loai
            {
                TenLoai = loai.TenLoai,
            };
            _context.Add(_Loai);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = _Loai.MaLoai,
                TenLoai = _Loai.TenLoai
            };
        }

        public void Delete(int id)
        {
           var _Loai = _context.Loais.SingleOrDefault(e => e.MaLoai == id);
            if(_Loai != null)
            {
                _context.Remove(_Loai);
                _context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            var Loais = _context.Loais.Select(e => new LoaiVM
            {
                MaLoai = e.MaLoai,
                TenLoai = e.TenLoai
            });
            return Loais.ToList();
        }

        public LoaiVM GetById(int id)
        {
            var Loai = _context.Loais.SingleOrDefault(e => e.MaLoai == id);
            if (Loai != null)
            {
                return new LoaiVM
                {
                    MaLoai = Loai.MaLoai,
                    TenLoai = Loai.TenLoai
                };
            }
            return null;
        }

        public void Update(LoaiVM loai)
        {
            var _Loai = _context.Loais.SingleOrDefault(e => e.MaLoai == loai.MaLoai);
            _Loai.TenLoai = loai.TenLoai;
            _context.SaveChanges();
        }
    }
}

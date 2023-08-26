using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiNetCore6.Repositories
{
    public interface IHangHoaRepository
    {
         List<HangHoaModel> GetAll(string? search, double? from, double? to, string? sortBy, int page = 1);
    }
}

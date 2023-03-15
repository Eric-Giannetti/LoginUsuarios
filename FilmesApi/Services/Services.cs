using AutoMapper;
using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Services
{
    public class Services
    {
        internal AppDbContext _context;
        internal IMapper _mapper;
        public Services(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

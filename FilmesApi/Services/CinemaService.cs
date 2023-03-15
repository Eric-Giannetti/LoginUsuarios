using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesApi.Data;
using FilmesAPI.Models;
using System.Collections.Generic;
using System.Linq;
using FluentResults;

namespace FilmesApi.Services
{
    public class CinemaService
    {
        internal AppDbContext _context;
        internal IMapper _mapper;
        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {

            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperarCinemas()
        {
            List<Cinema> cinemas;
                cinemas = _context.Cinemas.ToList();

            if (cinemas != null)
            {
                List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
                return readDto;
            }
            return null;
        }

        public ReadCinemaDto RecuperarCinemaPorNome(string? nome)
        {
            List<Cinema> cinemas;
            if (nome == null)
            {
                cinemas = _context.Cinemas.ToList();
            }
            else
            {
                cinemas = _context
                .Cinemas.Where(cinema => cinema.Nome == nome).ToList();
            }

            if (cinemas != null)
            {
                ReadCinemaDto readDto = _mapper.Map<ReadCinemaDto>(cinemas);
                return readDto;
            }
            return null;
        }

        public ReadCinemaDto RecuperarCinemaPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

                return cinemaDto;
            }
            return null;
        }

        internal Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
                return Result.Fail("Cinema não encontrado");

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return Result.Ok();
        }

        internal Result DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
                return Result.Fail("Cinema não encontrado");
            _context.Remove(cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
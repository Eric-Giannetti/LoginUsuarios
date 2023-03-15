using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Services
{
    public class GerenteService
    {
        internal AppDbContext _context;
        internal IMapper _mapper;
        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto gerenteDto)
        {

            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public List<ReadGerenteDto> RecuperarGerentes()
        {
            List<Gerente> gerentes;
            gerentes = _context.Gerentes.ToList();

            if (gerentes != null)
            {
                List<ReadGerenteDto> readDto = _mapper.Map<List<ReadGerenteDto>>(gerentes);
                return readDto;
            }
            return null;
        }

        public ReadGerenteDto RecuperarGerentePorNome(string? nome)
        {
            List<Gerente> gerentes;
            if (nome == null)
            {
                gerentes = _context.Gerentes.ToList();
            }
            else
            {
                gerentes = _context
                .Gerentes.Where(gerente => gerente.Nome == nome).ToList();
            }

            if (gerentes != null)
            {
                ReadGerenteDto readDto = _mapper.Map<ReadGerenteDto>(gerentes);
                return readDto;
            }
            return null;
        }

        public ReadGerenteDto RecuperarGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return gerenteDto;
            }
            return null;
        }

        internal Result AtualizaGerente(int id, UpdateGerenteDto gerenteDto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
                return Result.Fail("Gerente não encontrado");

            _mapper.Map(gerenteDto, gerente);
            _context.SaveChanges();
            return Result.Ok();
        }

        internal Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
                return Result.Fail("Gerente não encontrado");
            _context.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}

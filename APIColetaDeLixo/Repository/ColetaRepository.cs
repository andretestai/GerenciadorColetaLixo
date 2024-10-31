using APIColetaDeLixo.DataContext;
using APIColetaDeLixo.Interfaces.Repository;
using APIColetaDeLixo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIColetaDeLixo.Repository
{
    public class ColetaRepository : IColetaRepository
    {
        private readonly Context _context;

        public ColetaRepository(Context context)
        {
            _context = context;
        }

        public async Task<ColetaModel> AddColeta(ColetaModel coleta)
        {
            try
            {
                await _context.Coleta.AddAsync(coleta);
                await _context.SaveChangesAsync();
                return coleta;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar coleta: " + ex.Message);
            }
        }

        public async Task<bool> DeleteColeta(int id)
        {
            try
            {
                var coleta = await _context.Coleta.FindAsync(id);
                if (coleta != null)
                {
                    _context.Coleta.Remove(coleta);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir coleta: " + ex.Message);
            }
        }

        public async Task<List<ColetaModel>> GetAllColetas()
        {
            try
            {
                return await _context.Coleta.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar coletas: " + ex.Message);
            }
        }

        public async Task<ColetaModel> GetColeta(int id)
        {
            try
            {
                return await _context.Coleta.Where(at=> at.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar coleta: " + ex.Message);
            }
        }

        public async Task<List<ColetaModel>> GetColetaByUsuario(int usuarioId)
        {
            try
            {
                return await _context.Coleta.Where(c => c.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar coletas por usuário: " + ex.Message);
            }
        }

        public async Task<ColetaModel> UpdateColeta(ColetaModel coleta)
        {
            try
            {
                var validar = await _context.Coleta.FindAsync(coleta.Id);
                if (validar != null)
                {
                    validar.DataColeta = coleta.DataColeta;
                    validar.Local = coleta.Local;
                    validar.UsuarioId = coleta.UsuarioId;
                    await _context.SaveChangesAsync();
                    return validar!;
                }
                return validar!;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar coleta: " + ex.Message);
            }
        }
    }

}

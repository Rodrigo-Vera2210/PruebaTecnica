using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PruebaTecnica.DAL.Repositories.Contrato;
using PruebaTecnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
		private readonly PruebaTecnicaDbContext _db;

		public GenericRepository(PruebaTecnicaDbContext db) { _db = db; }
        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> expression = null)
        {
			try
			{
				IQueryable<TModel> queryModelo = expression == null ? _db.Set<TModel>() : _db.Set<TModel>().Where(expression);
				return queryModelo;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel modelo = await _db.Set<TModel>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

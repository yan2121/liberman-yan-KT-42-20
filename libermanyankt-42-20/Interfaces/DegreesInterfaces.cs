﻿
using libermanyankt_42_20.Database;
using libermanyankt_42_20.Filters.PrepodDegreeFilters;
using libermanyankt_42_20.Models;
using Microsoft.EntityFrameworkCore;


namespace libermanyankt_42_20.Interfaces
{
    public interface IDegreesService
    {
        public Task<Prepod[]> GetPrepodsByDegreeAsync(PrepodDegreeFilter filter, CancellationToken cancellationToken);
    }

    public class DegreeService : IDegreesService
    {
        private readonly PrepodDbContext _dbContext;
        public DegreeService(PrepodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Prepod[]> GetPrepodsByDegreeAsync(PrepodDegreeFilter filter, CancellationToken cancellationToken = default)
        {
            var degrees = _dbContext.Set<Prepod>().Where(w => w.Degree.Name_degree == filter.Name_degree).ToArrayAsync(cancellationToken);

            return degrees;
        }
    }
}

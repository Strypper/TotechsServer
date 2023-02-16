﻿using Intranet.Contract;
using Intranet.Entities;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class SubFeatureRepository : BaseRepository<SubFeature>, ISubFeatureRepository
    {
        public SubFeatureRepository(IntranetContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task DeleteAll(CancellationToken cancellationToken = default)
        {
            var subFeatures = await FindAll().ToListAsync(cancellationToken);
            _dbSet.RemoveRange(subFeatures);
        }
    }
}

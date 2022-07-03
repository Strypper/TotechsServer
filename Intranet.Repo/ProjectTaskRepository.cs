﻿using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(IntranetContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task DeleteAll(CancellationToken cancellationToken = default)
        {
            var projectTasks = await FindAll().ToListAsync(cancellationToken);
            _dbSet.RemoveRange(projectTasks);
        }
    }
}
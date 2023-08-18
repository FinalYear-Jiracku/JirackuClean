﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Common;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;
        private bool disposed;
        public IUserRepository UserRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public ISprintRepository SprintRepository { get; }
        public IStatusRepository StatusRepository { get; }
        public IIssueRepository IssueRepository { get; }
        public ISubIssueRepository SubIssueRepository { get; }
        public IColumnRepository ColumnRepository { get; }
        public INoteRepository NoteRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IPageRepository PageRepository { get; }
        public IEventRepository EventRepository { get; }
        public UnitOfWork(ApplicationDbContext dbContext, 
                          IUserRepository userRepository, 
                          IProjectRepository projectRepository,
                          ISprintRepository sprintRepository, 
                          IStatusRepository statusRepository,
                          IIssueRepository issueRepository,
                          ISubIssueRepository subIssueRepository,
                          IColumnRepository columnRepository,
                          INoteRepository noteRepository,
                          ICommentRepository commentRepository,
                          IPageRepository pageRepository,
                          IEventRepository eventRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            ProjectRepository = projectRepository;
            SprintRepository = sprintRepository;
            StatusRepository = statusRepository;
            IssueRepository = issueRepository;
            SubIssueRepository = subIssueRepository;
            ColumnRepository = columnRepository;
            NoteRepository = noteRepository;
            CommentRepository = commentRepository;
            PageRepository = pageRepository;
            EventRepository = eventRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }
    }
}

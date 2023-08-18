using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;

namespace TaskServices.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
        IUserRepository UserRepository { get; }
        IProjectRepository ProjectRepository { get; }
        ISprintRepository SprintRepository { get; }
        IStatusRepository StatusRepository { get; }
        IIssueRepository IssueRepository { get; }
        ISubIssueRepository SubIssueRepository { get; }
        IColumnRepository ColumnRepository { get; }
        INoteRepository NoteRepository { get; }
        ICommentRepository CommentRepository { get; }
        IPageRepository PageRepository { get; }
        IEventRepository EventRepository { get; }   
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
    }
}

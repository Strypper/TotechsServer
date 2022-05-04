using System;

namespace Intranet.Entities.Entities
{
    public class TodoTask : BaseEntity
    {
        public DateTime   DateCreated  { get; set; }
        public string     Title        { get; set; }
        public string     Description  { get; set; }
        public string     ImageTaskUrl { get; set; }
        public TaskStatus Status       { get; set; }
        public User       Author       { get; set; }
    }

    public enum TaskStatus
    {
        Suggestion, Todo, Doing, Testing, Done
    }
}
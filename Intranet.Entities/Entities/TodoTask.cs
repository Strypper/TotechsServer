using System;

namespace Intranet.Entities;

public class TodoTask : BaseEntity<int>
{
    public DateTime DateCreated { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageTaskUrl { get; set; } = default!;
    public TaskStatus Status { get; set; }
    public User Author { get; set; }= default!;
}

public enum TaskStatus
{
    Suggestion, Todo, Doing, Testing, Done
}
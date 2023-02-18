using System;

namespace Intranet.DataObject;

public class TodoTaskDTO : BaseDTO<int>
{
    public DateTime DateCreated { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageTaskUrl { get; set; } = default!;
    public TaskStatusDTO Status { get; set; }
    public UserDTO Author { get; set; } = default!;
}

public enum TaskStatusDTO
{
    Suggestion, Todo, Doing, Testing, Done
}
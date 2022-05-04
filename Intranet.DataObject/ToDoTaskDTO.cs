using System;

namespace Intranet.DataObject
{
    public class TodoTaskDTO : BaseDTO
    {
        public DateTime      DateCreated  { get; set; }
        public string        Title        { get; set; }
        public string        Description  { get; set; }
        public string        ImageTaskUrl { get; set; }
        public TaskStatusDTO Status       { get; set; }
        public UserDTO       Author       { get; set; }
    }

    public enum TaskStatusDTO
    {
        Suggestion, Todo, Doing, Testing, Done
    }
}
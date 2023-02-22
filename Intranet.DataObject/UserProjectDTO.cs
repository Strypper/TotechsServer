using System.Collections.Generic;

namespace Intranet.DataObject;


public class UserProjectDTO : BaseDTO<int>
{
    public UserDTO User { get; set; } = default!;
    public ProjectDTO Project { get; set; } = default!;
}

public class CreateUpdateUserProjectDTO
{
    public string UserGuid { get; set; } = default!;
    public int ProjectId { get; set; }
}

public class CreateProjectWithMultipleUsers : BaseDTO<int>
{
    public ICollection<UserDTO> Members { get; set; } = new HashSet<UserDTO>();
    public string ProjectName { get; set; } = default!;
    public string Clients { get; set; } = default!;
    public string About { get; set; } = default!;
    public int TechLead { get; set; }
}

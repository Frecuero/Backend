using System;
using System.Collections.Generic;

namespace BackendAPI.Models;

public partial class PhoneBook
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ContactTypeId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Comments { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public bool? Status { get; set; }

    public virtual TypeContact? ContactType { get; set; }
}

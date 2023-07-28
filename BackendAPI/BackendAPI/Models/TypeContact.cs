using System;
using System.Collections.Generic;

namespace BackendAPI.Models;

public partial class TypeContact
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PhoneBook> PhoneBooks { get; set; } = new List<PhoneBook>();
}

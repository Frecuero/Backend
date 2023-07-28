using BackendAPI.Models;

namespace BackendAPI.DTOs
{
    public class PhoneBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int ContactTypeId { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Comments { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public bool? Status { get; set; }

        public virtual TypeContactDTO? ContactType { get; set; }
    }
}

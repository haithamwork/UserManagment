using System;
using System.Collections.Generic;

namespace TerminalManagement.Domain.Entities
{
    public class Issuer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public ICollection<TerminalProfile> TerminalProfiles { get; set; } = new List<TerminalProfile>();
    }
}


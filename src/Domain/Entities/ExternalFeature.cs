using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Entities;
public class ExternalFeature : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public int PropertyId { get; set; }
    public Property Property { get; set; } = default!;
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BaseStatus.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

public class BaseStatus
{
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string Notes { get; set; }
}
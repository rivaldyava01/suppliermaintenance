namespace SupplierMaintenance.Client.Services.BackEnd;

public class BackEndOptions
{
    public const string SectionKey = nameof(BackEnd);

    public string BaseUrl { get; set; } = default!;
}

namespace SupplierMaintenance.InfraStructure.Persistence.Common.Constants;

public class CommonColumnTypes
{
    public static string Nvarchar(int length)
    {
        return $"nvarchar({length})";
    }
}

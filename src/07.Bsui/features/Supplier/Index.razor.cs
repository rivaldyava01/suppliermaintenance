namespace SupplierMaintenance.Bsui.Features.Supplier;

public partial class Index
{
    //private readonly List<ProvincesModel> _provinces = new();
    ////private readonly CreatePurchaseOrderModel _model = new();

    //protected async Task ReloadProvice()
    //{
    //    var responseResult = await _provinceService.GetListProvicesAsync();

    //    if (responseResult.Result is not null)
    //    {
    //        foreach (var province in responseResult.Result.Items)
    //        {
    //            _provinces.Add(new ProvincesModel
    //            {
    //                Id = province.Id,
    //                Name = province.Name
    //            });
    //        }

    //    }
    //}

}

public class ProvincesModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

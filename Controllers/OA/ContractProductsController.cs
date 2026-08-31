#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using MGUIBAAPI.Models.OA;
using GUIStd.BLL.OA.Private;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 合約產品/服務控制器
    /// </summary>
    [Route("oa/[controller]")]
    public class ContractProductsController : GUIAppAuthController
    {
        #region " 商業邏輯層屬性 "

        private BlOA20 BlOA20 => new BlOA20(ClientContent);

        #endregion

        #region " 產品/服務查詢 "

        /// <summary>
        /// 取得合約的產品/服務列表
        /// </summary>
        [HttpGet("list/{compId}/{contractId}")]
        public IEnumerable<MdOA20ProductInfo> GetContractProducts(string compId, string contractId)
        {
            var _products = BlOA20.GetProducts(compId, contractId);
            if (_products == null)
                return Enumerable.Empty<MdOA20ProductInfo>();

            return _products.Select(p => new MdOA20ProductInfo
            {
                productId = p.ProductId,
                productName = p.ProductName,
                productCategory = p.ProductCategory,
                salesAmount = p.SalesAmount,
                externalCost = p.ExternalCost,
                warrantyStartDate = p.WarrantyStartDate,
                warrantyEndDate = p.WarrantyEndDate,
                maintenanceStartDate = p.MaintenanceStartDate,
                maintenanceEndDate = p.MaintenanceEndDate,
                rentalStartDate = p.RentalStartDate,
                rentalEndDate = p.RentalEndDate,
                expectedMaintenanceAmount = p.ExpectedMaintenanceAmount,
                currentPM = p.CurrentPM
            }).ToList();
        }

        /// <summary>
        /// 依據產品ID取得單筆產品資料
        /// </summary>
        [HttpGet("{compId}/{contractId}/{productId}")]
        public MdOA20ProductInfo GetContractProduct(string compId, string contractId, string productId)
        {
            var _products = BlOA20.GetProducts(compId, contractId);
            var _product = _products?.FirstOrDefault(p => p.ProductId == productId);
            if (_product == null)
                return null;

            return new MdOA20ProductInfo
            {
                productId = _product.ProductId,
                productName = _product.ProductName,
                productCategory = _product.ProductCategory,
                salesAmount = _product.SalesAmount,
                externalCost = _product.ExternalCost,
                warrantyStartDate = _product.WarrantyStartDate,
                warrantyEndDate = _product.WarrantyEndDate,
                maintenanceStartDate = _product.MaintenanceStartDate,
                maintenanceEndDate = _product.MaintenanceEndDate,
                rentalStartDate = _product.RentalStartDate,
                rentalEndDate = _product.RentalEndDate,
                expectedMaintenanceAmount = _product.ExpectedMaintenanceAmount,
                currentPM = _product.CurrentPM
            };
        }

        /// <summary>
        /// 取得產品/服務下拉輔助
        /// </summary>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public MdOA20ProductP GetProductHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            // 註：此端點預留介面，目前回傳空 paging；如需實際查詢請補上對應 BLL 方法。
            return new MdOA20ProductP
            {
                codes = new List<MdOA20ProductInfo>(),
                paging = new MdPagingInfo { totalRows = 0 }
            };
        }

        /// <summary>
        /// 取得產品服務類別下拉
        /// </summary>
        [HttpGet("categories/{compId}")]
        public List<MdCodeOption> GetProductCategories(string compId)
        {
            return new List<MdCodeOption> {
                new MdCodeOption { value = "SW", label = "套裝軟體" },
                new MdCodeOption { value = "HW", label = "硬體" },
                new MdCodeOption { value = "RE", label = "租用" },
                new MdCodeOption { value = "TS-H", label = "技術服務(時數型)" },
                new MdCodeOption { value = "TS-N", label = "技術服務(非時數型)" },
                new MdCodeOption { value = "MA", label = "維護" },
                new MdCodeOption { value = "CU", label = "訂製" },
                new MdCodeOption { value = "OT", label = "其他" }
            };
        }

        #endregion

        #region " 產品/服務維護 "

        /// <summary>
        /// 新增資料
        /// </summary>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdContractProduct_i obj)
        {
            if (obj == null)
                return HttpContext.Response.InsertFailed(new Exception("請提供產品資料"));

            var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_i {
                OA2101 = obj.CompId ?? string.Empty,
                OA2102 = obj.ContractId ?? string.Empty,
                OA2103 = obj.ProductId ?? string.Empty,
                OA2104 = obj.SalesAmount,
                OA2105 = obj.ExternalCostAmount,
                OA2106 = obj.WarrantyStartDate ?? string.Empty,
                OA2107 = obj.WarrantyEndDate ?? string.Empty,
                OA2108 = obj.ExpectedMaintenanceAmount,
                OA2109 = obj.CurrentPM ?? string.Empty,
                OA2110 = obj.ProductCategory ?? string.Empty,
                OA2111 = obj.ProductName ?? string.Empty,  // 修正 BUG-002
                OA2112 = obj.MaintenanceStartDate ?? string.Empty,
                OA2113 = obj.MaintenanceEndDate ?? string.Empty,
                OA2114 = obj.RentalStartDate ?? string.Empty,
                OA2115 = obj.RentalEndDate ?? string.Empty
            };

            try
            {
                var _result = BlOA20.InsertProduct(_data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.InsertFailed(new Exception(_result.Message));

                return HttpContext.Response.InsertSuccess(1, responseData: obj.ProductId);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        [HttpPut("{compId}/{contractId}/{productId}")]
        public MdApiMessage Update(string compId, string contractId, string productId, [FromBody] MdContractProduct_u obj)
        {
            // 註：MdContractProduct_u 不含鍵值欄位，鍵值僅在 URL 路徑，故不適用 UpdateFailedWhenKeyNotSame。
            if (obj == null)
                return HttpContext.Response.UpdateFailed(new Exception("請提供產品資料"));

            var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_u {
                // 修正 BUG-002：補 ProductName (OA2111) 與 ProductCategory (OA2110) 的寫入
                OA2104 = obj.SalesAmount,
                OA2105 = obj.ExternalCostAmount,
                OA2106 = obj.WarrantyStartDate ?? string.Empty,
                OA2107 = obj.WarrantyEndDate ?? string.Empty,
                OA2108 = obj.ExpectedMaintenanceAmount,
                OA2109 = obj.CurrentPM ?? string.Empty,
                OA2110 = obj.ProductCategory ?? string.Empty,
                OA2111 = obj.ProductName ?? string.Empty,
                OA2112 = obj.MaintenanceStartDate ?? string.Empty,
                OA2113 = obj.MaintenanceEndDate ?? string.Empty,
                OA2114 = obj.RentalStartDate ?? string.Empty,
                OA2115 = obj.RentalEndDate ?? string.Empty
            };

            try
            {
                var _result = BlOA20.UpdateProduct(compId, contractId, productId, _data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.UpdateFailed(new Exception(_result.Message));

                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        [HttpDelete("{compId}/{contractId}/{productId}")]
        public MdApiMessage Delete(string compId, string contractId, string productId)
        {
            try
            {
                var _result = BlOA20.DeleteProduct(compId, contractId, productId, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.DeleteFailed(new Exception(_result.Message));

                return HttpContext.Response.DeleteSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 批次作業 "

        /// <summary>
        /// 批次新增產品/服務
        /// </summary>
        [HttpPost("batch/{compId}/{contractId}")]
        public MdApiMessage BatchInsertProducts(string compId, string contractId, [FromBody] List<MdContractProduct_i> products)
        {
            if (products == null || products.Count == 0)
                return HttpContext.Response.InsertFailed(new Exception("請提供產品清單"));

            var _errors = new List<string>();
            var _successCount = 0;

            try
            {
                foreach (var _product in products)
                {
                    var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_i {
                        OA2101 = compId,
                        OA2102 = contractId,
                        OA2103 = _product.ProductId ?? string.Empty,
                        OA2104 = _product.SalesAmount,
                        OA2105 = _product.ExternalCostAmount,
                        OA2106 = _product.WarrantyStartDate ?? string.Empty,
                        OA2107 = _product.WarrantyEndDate ?? string.Empty,
                        OA2108 = _product.ExpectedMaintenanceAmount,
                        OA2109 = _product.CurrentPM ?? string.Empty,
                        OA2110 = _product.ProductCategory ?? string.Empty,
                        OA2111 = _product.ProductName ?? string.Empty,  // 修正 BUG-002
                        OA2112 = _product.MaintenanceStartDate ?? string.Empty,
                        OA2113 = _product.MaintenanceEndDate ?? string.Empty,
                        OA2114 = _product.RentalStartDate ?? string.Empty,
                        OA2115 = _product.RentalEndDate ?? string.Empty
                    };

                    var _result = BlOA20.InsertProduct(_data, ControlName);
                    if (_result.Success)
                        _successCount++;
                    else
                        _errors.Add($"{_product.ProductId}: {_result.Message}");
                }

                if (_errors.Count > 0)
                    return HttpContext.Response.InsertFailed(new Exception(
                        $"批次新增產品/服務部分失敗。成功 {_successCount} 筆，失敗 {products.Count - _successCount} 筆。"));

                return HttpContext.Response.InsertSuccess(_successCount);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 重新計算產品/服務的預計維護費
        /// </summary>
        [HttpPost("{compId}/{contractId}/recalc-maintenance")]
        public MdApiMessage RecalculateMaintenance(string compId, string contractId)
        {
            try
            {
                // 註：實際重算邏輯應由 BLL 提供；目前端點介面對齊 PATTERN。
                return HttpContext.Response.UpdateSuccess(0);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }

    #region " 輔助 DTO "

    /// <summary>
    /// 合約產品/服務資訊
    /// </summary>
    public class MdOA20ProductInfo
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productCategory { get; set; }
        public decimal salesAmount { get; set; }
        public decimal externalCost { get; set; }
        public string warrantyStartDate { get; set; }
        public string warrantyEndDate { get; set; }
        public string maintenanceStartDate { get; set; }
        public string maintenanceEndDate { get; set; }
        public string rentalStartDate { get; set; }
        public string rentalEndDate { get; set; }
        public decimal expectedMaintenanceAmount { get; set; }
        public string currentPM { get; set; }
    }

    /// <summary>
    /// 合約產品/服務分頁結果
    /// </summary>
    public class MdOA20ProductP
    {
        public List<MdOA20ProductInfo> codes { get; set; }
        public MdPagingInfo paging { get; set; }
    }

    /// <summary>
    /// 分頁資訊
    /// </summary>
    public class MdPagingInfo
    {
        public int currentPage { get; set; }
        public int rowsPerPage { get; set; }
        public int totalRows { get; set; }
    }

    /// <summary>
    /// 通用代碼選項（value/label）
    /// </summary>
    public class MdCodeOption
    {
        public string value { get; set; }
        public string label { get; set; }
    }

    #endregion
}

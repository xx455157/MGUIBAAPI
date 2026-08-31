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
    /// 產品/服務控制器（OA21）
    /// </summary>
    [Route("oa/[controller]")]
    public class ProductsController : GUIAppAuthController
    {
        #region " 商業邏輯層屬性 "

        private BlOA21 BlOA21 => new BlOA21(ClientContent);

        #endregion

        #region " 產品/服務查詢 "

        /// <summary>
        /// 依合約取得產品/服務清單
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <returns>產品/服務清單</returns>
        [HttpGet("by-contract/{compId}/{contractId}")]
        public List<GUIStd.DAL.OA.Models.Private.OA21.MdOA21> GetProductsByContract(string compId, string contractId)
        {
            return (BlOA21.GetProductsByContract(compId ?? string.Empty, contractId) ?? Enumerable.Empty<GUIStd.DAL.OA.Models.Private.OA21.MdOA21>()).ToList();
        }

        /// <summary>
        /// 依合約取得產品/服務統計
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <returns>產品統計</returns>
        [HttpGet("stats/{compId}/{contractId}")]
        public MdProductStats GetProductStats(string compId, string contractId)
        {
            var _products = BlOA21.GetProductsByContract(compId ?? string.Empty, contractId);
            var _list = (_products ?? Enumerable.Empty<GUIStd.DAL.OA.Models.Private.OA21.MdOA21>()).ToList();
            return new MdProductStats
            {
                TotalProducts = _list.Count,
                TotalSalesAmount = _list.Sum(p => p.SalesAmount),
                TotalExternalCost = _list.Sum(p => p.ExternalCost)
            };
        }

        #endregion

        #region " 產品/服務維護 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">產品/服務資料</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdContractProduct_i obj)
        {
            if (obj == null)
                return HttpContext.Response.InsertFailed(new Exception("請提供產品/服務資料"));

            var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_i
            {
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
                OA2112 = obj.MaintenanceStartDate ?? string.Empty,
                OA2113 = obj.MaintenanceEndDate ?? string.Empty,
                OA2114 = obj.RentalStartDate ?? string.Empty,
                OA2115 = obj.RentalEndDate ?? string.Empty
            };

            try
            {
                var _result = BlOA21.Insert(_data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.InsertFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.InsertSuccess(1, responseData: _result.Result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="productId">產品ID</param>
        /// <param name="obj">產品/服務資料</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{compId}/{contractId}/{productId}")]
        public MdApiMessage Update(string compId, string contractId, string productId, [FromBody] MdContractProduct_u obj)
        {
            // 註：MdContractProduct_u 不含鍵值欄位，鍵值僅在 URL 路徑，故不適用 UpdateFailedWhenKeyNotSame。
            if (obj == null)
                return HttpContext.Response.UpdateFailed(new Exception("請提供產品/服務資料"));

            var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_u
            {
                OA2104 = obj.SalesAmount,
                OA2105 = obj.ExternalCostAmount,
                OA2106 = obj.WarrantyStartDate ?? string.Empty,
                OA2107 = obj.WarrantyEndDate ?? string.Empty,
                OA2108 = obj.ExpectedMaintenanceAmount,
                OA2109 = obj.CurrentPM ?? string.Empty,
                OA2112 = obj.MaintenanceStartDate ?? string.Empty,
                OA2113 = obj.MaintenanceEndDate ?? string.Empty,
                OA2114 = obj.RentalStartDate ?? string.Empty,
                OA2115 = obj.RentalEndDate ?? string.Empty
            };

            try
            {
                var _result = BlOA21.Update(compId ?? string.Empty, contractId, productId, _data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.UpdateFailed(ex: new Exception(_result.Message));

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
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="productId">產品ID</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{compId}/{contractId}/{productId}")]
        public MdApiMessage Delete(string compId, string contractId, string productId)
        {
            try
            {
                var _result = BlOA21.Delete(compId ?? string.Empty, contractId, productId, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.DeleteFailed(ex: new Exception(_result.Message));

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
        /// 批次更新產品/服務
        /// </summary>
        /// <param name="products">產品/服務清單</param>
        /// <returns>批次更新結果</returns>
        [HttpPost("batch-update")]
        public MdApiMessage BatchUpdateProducts([FromBody] List<MdContractProduct_u> products)
        {
            if (products == null || products.Count == 0)
                return HttpContext.Response.UpdateFailed(new Exception("請提供產品/服務資料"));

            var _results = new List<object>();
            var _hasError = false;

            try
            {
                foreach (var product in products)
                {
                    var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_u
                    {
                        OA2104 = product.SalesAmount,
                        OA2105 = product.ExternalCostAmount,
                        OA2106 = product.WarrantyStartDate ?? string.Empty,
                        OA2107 = product.WarrantyEndDate ?? string.Empty,
                        OA2108 = product.ExpectedMaintenanceAmount,
                        OA2109 = product.CurrentPM ?? string.Empty
                    };

                    var _result = BlOA21.Update(string.Empty, string.Empty, product.ProductId, _data, ControlName);
                    _results.Add(new
                    {
                        productId = product.ProductId,
                        success = _result.Success,
                        message = _result.Message
                    });

                    if (!_result.Success)
                        _hasError = true;
                }

                if (_hasError)
                    return HttpContext.Response.UpdateFailed(new Exception("批次更新產品/服務存在失敗項目"));

                return HttpContext.Response.UpdateSuccess(1, responseData: _results);
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
    /// 產品/服務統計
    /// </summary>
    public class MdProductStats
    {
        /// <summary>產品數量</summary>
        public int TotalProducts { get; set; }

        /// <summary>總銷售金額</summary>
        public decimal TotalSalesAmount { get; set; }

        /// <summary>總外包成本</summary>
        public decimal TotalExternalCost { get; set; }
    }

    #endregion
}

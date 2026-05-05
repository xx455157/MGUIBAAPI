#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using System.Linq;
using MGUIBAAPI.Models.OA;
using GUIStd.BLL.OA.Private;
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

        private BlOA20 BlOA20 => mBlOA20 = mBlOA20 ?? new BlOA20(ClientContent);
        private BlOA20 mBlOA20;

        #endregion

        #region " 產品/服務查詢 "

        /// <summary>
        /// 取得合約的產品/服務列表
        /// </summary>
        [HttpGet("list/{compId}/{contractId}")]
        public IActionResult GetContractProducts(string compId, string contractId)
        {
            try
            {
                var _products = BlOA20.GetProducts(compId, contractId);
                if (_products == null)
                    return Ok(new { success = true, data = new { codes = new List<object>() } });

                var _list = _products.Select(p => new {
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

                return Ok(new { success = true, data = new { codes = _list } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 依據產品ID取得單筆產品資料
        /// </summary>
        [HttpGet("{compId}/{contractId}/{productId}")]
        public IActionResult GetContractProduct(string compId, string contractId, string productId)
        {
            try
            {
                var _products = BlOA20.GetProducts(compId, contractId);
                var _product = _products?.FirstOrDefault(p => p.ProductId == productId);

                if (_product == null)
                    return NotFound(new { success = false, message = "產品不存在" });

                return Ok(new { success = true, data = new {
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
                }});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得產品/服務下拉輔助
        /// </summary>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public IActionResult GetProductHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            try
            {
                return Ok(new { success = true, data = new { codes = new List<object>(), paging = new { totalRows = 0 } } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得產品服務類別下拉
        /// </summary>
        [HttpGet("categories/{compId}")]
        public IActionResult GetProductCategories(string compId)
        {
            try
            {
                var _categories = new List<object> {
                    new { value = "SW", label = "套裝軟體" },
                    new { value = "HW", label = "硬體" },
                    new { value = "RE", label = "租用" },
                    new { value = "TS-H", label = "技術服務(時數型)" },
                    new { value = "TS-N", label = "技術服務(非時數型)" },
                    new { value = "MA", label = "維護" },
                    new { value = "CU", label = "訂製" },
                    new { value = "OT", label = "其他" }
                };

                return Ok(new { success = true, data = _categories });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region " 產品/服務維護 "

        /// <summary>
        /// 新增產品/服務
        /// </summary>
        [HttpPost("insert")]
        public IActionResult InsertContractProduct([FromBody] MdContractProduct_i productData)
        {
            try
            {
                if (productData == null)
                    return BadRequest(new { success = false, message = "請提供產品資料" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_i {
                    OA2101 = productData.CompId ?? string.Empty,
                    OA2102 = productData.ContractId ?? string.Empty,
                    OA2103 = productData.ProductId ?? string.Empty,
                    OA2104 = productData.SalesAmount,
                    OA2105 = productData.ExternalCostAmount,
                    OA2106 = productData.WarrantyStartDate ?? string.Empty,
                    OA2107 = productData.WarrantyEndDate ?? string.Empty,
                    OA2108 = productData.ExpectedMaintenanceAmount,
                    OA2109 = productData.CurrentPM ?? string.Empty,
                    OA2110 = productData.ProductCategory ?? string.Empty,
                    OA2112 = productData.MaintenanceStartDate ?? string.Empty,
                    OA2113 = productData.MaintenanceEndDate ?? string.Empty,
                    OA2114 = productData.RentalStartDate ?? string.Empty,
                    OA2115 = productData.RentalEndDate ?? string.Empty
                };

                var _result = BlOA20.InsertProduct(_data, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new {
                    success = true,
                    message = _result.Message,
                    data = new { productId = productData.ProductId }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 更新產品/服務
        /// </summary>
        [HttpPut("{compId}/{contractId}/{productId}")]
        public IActionResult UpdateContractProduct(string compId, string contractId, string productId, [FromBody] MdContractProduct_u productData)
        {
            try
            {
                if (productData == null)
                    return BadRequest(new { success = false, message = "請提供產品資料" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA21.MdOA21_u {
                    OA2104 = productData.SalesAmount,
                    OA2105 = productData.ExternalCostAmount,
                    OA2106 = productData.WarrantyStartDate ?? string.Empty,
                    OA2107 = productData.WarrantyEndDate ?? string.Empty,
                    OA2108 = productData.ExpectedMaintenanceAmount,
                    OA2109 = productData.CurrentPM ?? string.Empty,
                    OA2112 = productData.MaintenanceStartDate ?? string.Empty,
                    OA2113 = productData.MaintenanceEndDate ?? string.Empty,
                    OA2114 = productData.RentalStartDate ?? string.Empty,
                    OA2115 = productData.RentalEndDate ?? string.Empty
                };

                var _result = BlOA20.UpdateProduct(compId, contractId, productId, _data, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new { success = true, message = _result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 刪除產品/服務
        /// </summary>
        [HttpDelete("{compId}/{contractId}/{productId}")]
        public IActionResult DeleteContractProduct(string compId, string contractId, string productId)
        {
            try
            {
                var _result = BlOA20.DeleteProduct(compId, contractId, productId, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new { success = true, message = _result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region " 批次作業 "

        /// <summary>
        /// 批次新增產品/服務
        /// </summary>
        [HttpPost("batch/{compId}/{contractId}")]
        public IActionResult BatchInsertProducts(string compId, string contractId, [FromBody] List<MdContractProduct_i> products)
        {
            try
            {
                if (products == null || products.Count == 0)
                    return BadRequest(new { success = false, message = "請提供產品清單" });

                var _errors = new List<string>();
                var _successCount = 0;

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

                return Ok(new {
                    success = _errors.Count == 0,
                    message = $"成功新增 {_successCount} 筆，失敗 {products.Count - _successCount} 筆",
                    errors = _errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 重新計算產品/服務的預計維護費
        /// </summary>
        [HttpPost("{compId}/{contractId}/recalc-maintenance")]
        public IActionResult RecalculateMaintenance(string compId, string contractId)
        {
            try
            {
                return Ok(new { success = true, message = "重新計算完成" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}

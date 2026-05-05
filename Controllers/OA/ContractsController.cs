#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using System.Linq;
using GUICore.Web.Extensions;
using MGUIBAAPI.Models.OA;
using GUIStd.BLL.OA.Private;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 營收合約控制器
    /// </summary>
    [Route("oa/[controller]")]
    public class ContractsController : GUIAppAuthController
    {
        #region " 商業邏輯層屬性 "

        private BlOA20 BlOA20 => mBlOA20 = mBlOA20 ?? new BlOA20(ClientContent);
        private BlOA20 mBlOA20;

        #endregion

        #region " 合約主檔查詢 "

        /// <summary>
        /// 取得合約分頁資料
        /// </summary>
        [HttpPost("pages/{pageNo}")]
        public IActionResult GetContracts([FromBody] MdContract_q queryParams, [DARange(1, int.MaxValue)] int pageNo)
        {
            try
            {
                var _result = BlOA20.GetData(
                    queryParams.CompId ?? string.Empty,
                    queryParams.CustomerId ?? string.Empty,
                    string.Empty,
                    queryParams.ContractStatus ?? string.Empty,
                    queryParams.QueryText ?? string.Empty,
                    ControlName,
                    pageNo
                );

                if (_result == null)
                    return Ok(new { success = true, data = new { codes = new List<object>(), paging = new { totalRows = 0, rowsPerPage = 0, currentPage = pageNo } } });

                return Ok(new { success = true, data = _result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得合約單筆資料
        /// </summary>
        [HttpGet("{compId}/{contractId}")]
        public IActionResult GetContract(string compId, string contractId)
        {
            try
            {
                var _contract = BlOA20.GetRow(compId, contractId);
                if (_contract == null)
                    return NotFound(new { success = false, message = "合約不存在" });

                var _products = BlOA20.GetProducts(compId, contractId);

                return Ok(new { success = true, data = new {
                    contract = _contract,
                    products = _products?.Select(p => new {
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
                    })
                }});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 合約輔助查詢（分頁）
        /// </summary>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public IActionResult GetContractHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            try
            {
                var _result = BlOA20.GetData(compId, string.Empty, string.Empty, string.Empty, queryText, ControlName, pageNo);
                if (_result == null)
                    return Ok(new { success = true, data = new { codes = new List<object>(), paging = new { totalRows = 0 } } });

                return Ok(new { success = true, data = _result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得客戶別合約統計
        /// </summary>
        [HttpGet("stats/{compId}/{customerId}")]
        public IActionResult GetContractStats(string compId, string customerId)
        {
            try
            {
                var _result = BlOA20.GetData(compId, customerId, string.Empty, string.Empty, string.Empty, ControlName, 1);
                if (_result == null)
                    return Ok(new { success = true, data = new MdContractStats() });

                var _contracts = _result.Codes?.ToList() ?? new List<GUIStd.DAL.OA.Models.Private.OA20.MdOA20>();
                return Ok(new { success = true, data = new MdContractStats {
                    TotalContracts = _result.Paging?.TotalRows ?? _contracts.Count,
                    TotalAmount = _contracts.Sum(c => c.ContractAmountTax)
                }});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region " 合約主檔維護 "

        /// <summary>
        /// 新增合約
        /// </summary>
        [HttpPost("insert")]
        public IActionResult InsertContract([FromBody] MdContract_i contractData)
        {
            try
            {
                if (contractData == null)
                    return BadRequest(new { success = false, message = "請提供合約資料" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA20.MdOA20_i {
                    OA2001 = contractData.CompId ?? string.Empty,
                    OA2002 = contractData.ContractId ?? string.Empty,
                    OA2003 = contractData.CustomerId ?? string.Empty,
                    OA2004 = contractData.NewOldCustomer ?? "N",
                    OA2005 = contractData.ContractEndDate ?? string.Empty,
                    OA2006 = contractData.ContractType ?? "M",
                    OA2007 = contractData.ContractAmount,
                    OA2008 = contractData.ContractAmountTax,
                    OA2009 = contractData.ExternalCostBudget,
                    OA2010 = contractData.ContractStatus ?? "A",
                    OA2011 = contractData.Remark ?? string.Empty,
                    OA2012 = contractData.ExtendControlDate ?? string.Empty,
                    OA2013 = DateTime.Now.ToString("yyyy/MM/dd"),
                    OA2014 = contractData.CurrentSales ?? ClientContent.SystemUserId,
                    OA2015 = contractData.ContractFileUrl ?? string.Empty
                };

                var _result = BlOA20.Insert(_data, null, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new { success = true, data = _result.Result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 更新合約
        /// </summary>
        [HttpPut("{compId}/{contractId}")]
        public IActionResult UpdateContract(string compId, string contractId, [FromBody] MdContract_u contractData)
        {
            try
            {
                if (contractData == null)
                    return BadRequest(new { success = false, message = "請提供合約資料" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA20.MdOA20_u {
                    OA2005 = contractData.ContractEndDate ?? string.Empty,
                    OA2006 = contractData.ContractType ?? "M",
                    OA2007 = contractData.ContractAmount,
                    OA2008 = contractData.ContractAmountTax,
                    OA2009 = contractData.ExternalCostBudget,
                    OA2010 = contractData.ContractStatus ?? "A",
                    OA2011 = contractData.Remark ?? string.Empty,
                    OA2012 = contractData.ExtendControlDate ?? string.Empty,
                    OA2014 = contractData.CurrentSales ?? ClientContent.SystemUserId,
                    OA2015 = contractData.ContractFileUrl ?? string.Empty
                };

                var _result = BlOA20.Update(compId, contractId, _data, ControlName);

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
        /// 刪除合約
        /// </summary>
        [HttpDelete("{compId}/{contractId}")]
        public IActionResult DeleteContract(string compId, string contractId)
        {
            try
            {
                var _result = BlOA20.Delete(compId, contractId, ControlName);

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

        #region " 合約狀態管理 "

        /// <summary>
        /// 更新合約狀態
        /// </summary>
        [HttpPatch("{compId}/{contractId}/status")]
        public IActionResult UpdateContractStatus(string compId, string contractId, [FromBody] MdContractStatusUpdate statusUpdate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(statusUpdate?.NewStatus))
                    return BadRequest(new { success = false, message = "請提供新狀態" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA20.MdOA20_u {
                    OA2010 = statusUpdate.NewStatus
                };

                var _result = BlOA20.Update(compId, contractId, _data, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new { success = true, message = "狀態更新成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 展期合約（自動續約）
        /// </summary>
        [HttpPost("{compId}/{contractId}/extend")]
        public IActionResult ExtendContract(string compId, string contractId, [FromBody] MdContractExtend extendData)
        {
            try
            {
                if (extendData == null)
                    return BadRequest(new { success = false, message = "請提供展期資料" });

                var _data = new GUIStd.DAL.OA.Models.Private.OA20.MdOA20_u {
                    OA2005 = extendData.NewEndDate ?? string.Empty,
                    OA2012 = DateTime.Now.AddYears(extendData.ExtendYears).ToString("yyyy/MM/dd"),
                    OA2011 = extendData.Remark ?? string.Empty
                };

                var _result = BlOA20.Update(compId, contractId, _data, ControlName);

                if (!_result.Success)
                    return BadRequest(new { success = false, message = _result.Message });

                return Ok(new { success = true, message = "展期成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region " Back Log / AR 報表 "

        /// <summary>
        /// 取得 Back Log 清單
        /// </summary>
        [HttpGet("backlog/{compId}")]
        public IActionResult GetBackLog(string compId, [FromQuery] string salesId, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                return Ok(new { success = true, data = new List<object>() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得 AR(應收帳款) 清單
        /// </summary>
        [HttpGet("ar/{compId}")]
        public IActionResult GetARList(string compId, [FromQuery] string salesId)
        {
            try
            {
                return Ok(new { success = true, data = new List<object>() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 取得客戶維護中合約清單
        /// </summary>
        [HttpGet("expiring/{compId}")]
        public IActionResult GetExpiringContracts(string compId, [FromQuery] int expireDays = 30)
        {
            try
            {
                return Ok(new { success = true, data = new List<object>() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region " 報表 "

        /// <summary>
        /// 合約營收統計報表
        /// </summary>
        [HttpGet("report/revenue/{compId}")]
        public IActionResult GetRevenueReport(string compId, [FromQuery] string startDate, [FromQuery] string endDate,
            [FromQuery] string salesId, [FromQuery] string customerId)
        {
            try
            {
                return Ok(new { success = true, data = new object() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 資金預測報表
        /// </summary>
        [HttpGet("report/cashflow/{compId}")]
        public IActionResult GetCashFlowForecast(string compId, [FromQuery] int months = 12)
        {
            try
            {
                return Ok(new { success = true, data = new object() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}

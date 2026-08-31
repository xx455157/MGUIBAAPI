#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewIV.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.DAL.AllNewIV.Models.Private.vIVM14;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.IV
{
    /// <summary>
    /// vIVM14 銷項發票登錄
    /// </summary>
    [Route("iv/private/[controller]")]
    public class vIVM14Controller : GUIAppAuthController
    {
        private BlIVM14 BlIVM14 => new BlIVM14(ClientContent);

        /// <summary>
        /// 畫面預設資料（公司別、發票格式、課稅別、發票歸類、異動別等 el-select 選項）
        /// </summary>
        [HttpGet("page")]
        public MdIVM14_h GetUIData() => BlIVM14.GetUIData();

        /// <summary>
        /// 依發票日期取得營業稅率（SINI SalesTax）
        /// </summary>
        [HttpGet("salesTax/{invoiceDate}")]
        public MdIVM14SalesTax_h GetSalesTax(string invoiceDate)
        {
            return new MdIVM14SalesTax_h
            {
                SalesTaxRate = BlIVM14.GetSalesTaxRate(invoiceDate)
            };
        }

        /// <summary>
        /// 取得分頁查詢資料（Q 畫面列表）
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdIVM14QueryList_p GetQueryData([DARange(1, int.MaxValue)] int pageNo,[FromBody] MdIVM14Query queryParams,int rowsPerPage = 0)
        {
            return BlIVM14.GetQueryData(queryParams ?? new MdIVM14Query(), ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 公司別最後申報日
        /// </summary>
        [HttpGet("declareDate/{companyId}")]
        public MdIVM14DeclareDate_h GetLastDeclareDate(string companyId)
        {
            return new MdIVM14DeclareDate_h
            {
                LastDeclareDate = BlIVM14.GetLastDeclareDate(companyId)
            };
        }

        /// <summary>
        /// 原發票 blur 查詢（對應 WinForms GetB3120）
        /// </summary>
        [HttpGet("originalInvoice/{companyId}/{originalInvoiceNo}")]
        public MdB31OriginalInvoice_h GetOriginalInvoice(
            string companyId,
            string originalInvoiceNo,
            [FromQuery] string invoiceDate = null)
        {
            return BlIVM14.GetOriginalInvoice(companyId, originalInvoiceNo, invoiceDate);
        }

        /// <summary>
        /// 取得銷項發票明細（B31 主檔 + B04 明細列）
        /// </summary>
        [HttpGet("detail/{companyId}/{invoiceDate}/{invoiceNo}")]
        public MdIVM14Detail_h GetDetail(string companyId, string invoiceDate, string invoiceNo) =>
            BlIVM14.GetDetail(companyId, invoiceDate, invoiceNo);

        /// <summary>
        /// 零稅率規定輔助（對應 dlgZeroSelect htxtB3127）
        /// </summary>
        [HttpGet("zeroTaxHelp/pages/{pageNo}")]
        public MdIVM14ZeroTaxHelp_p GetZeroTaxHelp(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string queryText = null,
            int rowsPerPage = 0) =>
            BlIVM14.GetZeroTaxHelpPage(queryText, pageNo, ref rowsPerPage);

        /// <summary>
        /// 零稅率規定是否存在（對應 dlgZeroSelect htxtB3127_Validating）
        /// </summary>
        [HttpGet("zeroTaxRule/exists/{topic}")]
        public bool IsZeroTaxRuleExist(string topic) => BlIVM14.IsZeroTaxRuleExist(topic);

        /// <summary>
        /// B32 自動配號（對應 frmIVM14d.AutoGetInvoiceNoFromB32）
        /// </summary>
        [HttpPost("autoInvoiceNo")]
        public MdApiMessage GetAutoInvoiceNo([FromBody] MdIVM14AutoInvoice_w obj)
        {
            try
            {
                var _data = BlIVM14.GetAutoInvoiceNo(obj ?? new MdIVM14AutoInvoice_w());
                return HttpContext.Response.InsertSuccess(affectedRows: 0, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex, "");
            }
        }

        /// <summary>
        /// 新增存檔前資料庫檢核（B32 字軌、重複、序時序號）
        /// </summary>
        [HttpPost("validate")]
        public MdApiMessage ValidateInsert([FromBody] MdIVM14_w obj)
        {
            try
            {
                var _hint = BlIVM14.ValidateInsert(obj);
                return HttpContext.Response.InsertSuccess(affectedRows: 0, responseData: _hint);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex,"");
            }
        }

        /// <summary>
        /// 新增銷項發票
        /// </summary>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdIVM14_w obj)
        {
            try
            {
                var _data = BlIVM14.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(affectedRows: 1, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除銷項發票（B31 + B04 + B08）
        /// </summary>
        [HttpDelete("delete/{companyId}/{invoiceDate}/{invoiceNo}")]
        public MdApiMessage Delete(string companyId,string invoiceDate,string invoiceNo,
            [FromQuery] string lockedTicketNo = null)
        {
            try
            {
                var _result = BlIVM14.ProcessDelete(companyId, invoiceDate, invoiceNo, lockedTicketNo);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 修改銷項發票（B31 + B04，對應 frmIVM14d.PerformDBUdpateAction）
        /// </summary>
        [HttpPut("update/{companyId}/{invoiceDate}/{invoiceNo}")]
        public MdApiMessage Update(string companyId,string invoiceDate,string invoiceNo,
            [FromBody] MdIVM14_w obj,[FromQuery] string editLevel = "full",[FromQuery] string lockedTicketNo = null)
        {
            try
            {
                var _data = BlIVM14.ProcessUpdate(
                    companyId,
                    invoiceDate,
                    invoiceNo,
                    obj,
                    editLevel,
                    lockedTicketNo);
                return HttpContext.Response.UpdateSuccess(affectedRows: 1, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 作廢重開（對應 frmIVM14d.cmdInvoiceVoid）
        /// </summary>
        [HttpPost("voidReopen/{companyId}/{invoiceDate}/{invoiceNo}")]
        public MdApiMessage VoidReopen(string companyId,string invoiceDate,string invoiceNo,
            [FromBody] MdIVM14VoidReopen_w obj)
        {
            try
            {
                var _data = BlIVM14.ProcessVoidReopen(companyId, invoiceDate, invoiceNo, obj);
                return HttpContext.Response.UpdateSuccess(affectedRows: 1, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 電子發票註銷（對應 frmIVM14d.cmdClearMark）
        /// </summary>
        [HttpPost("clearMark/{companyId}/{invoiceDate}/{invoiceNo}")]
        public MdApiMessage ClearMark(string companyId, string invoiceDate, string invoiceNo)
        {
            try
            {
                var _data = BlIVM14.ProcessClearMark(companyId, invoiceDate, invoiceNo);
                return HttpContext.Response.UpdateSuccess(affectedRows: 1, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 電子發票匯入 — 補齊公司／館／部門（對齊 dlgImportInvoice.GUIDataRowBound）
        /// </summary>
        [HttpPost("import/prepare")]
        public MdIVM14ImportPrepare_h PrepareImport([FromBody] MdIVM14ImportPrepare_w obj) =>
            BlIVM14.PrepareImport(obj ?? new MdIVM14ImportPrepare_w());

        /// <summary>
        /// 電子發票匯入存檔
        /// </summary>
        [HttpPost("import")]
        public MdApiMessage Import([FromBody] MdIVM14ImportSave_w obj)
        {
            try
            {
                var _data = BlIVM14.ProcessImport(obj ?? new MdIVM14ImportSave_w());
                if (!_data.Result)
                    return HttpContext.Response.InsertFailed(new Exception(_data.Message));
                return HttpContext.Response.InsertSuccess(affectedRows: _data.SavedCount, responseData: _data);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }
    }
}

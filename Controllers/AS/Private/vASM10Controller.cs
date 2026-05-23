#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM02;
using GUIStd.Models;
using System;
using GUIStd;
using GUIStd.DAL.AllNewAS.Models.Private.vASM10;
using GUIStd.DAL.AllNewGUI.Models;
using System.Linq;



#endregion


namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM10 固定資產折舊計算 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM10Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM10 BlASM10 => new BlASM10(ClientContent);
        private BlAA BlAA => new BlAA(ClientContent);
        private BlAC BlAC => new BlAC(ClientContent);
        private BlAG BlAG => new BlAG(ClientContent);
        private BlSINI BlSINI=> new BlSINI(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢Q頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/pageq")]
        public MdASM10_Qh GetQPageHelp()
        {
            return BlASM10.GetQPageHelp();
        }

        /// <summary>
        /// 明細D頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/paged/{companyId}")]
        public MdASM10_Dh GetDPageHelp(string companyId)
        {
            return BlASM10.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 查詢固定資產折舊清單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>查詢結果（含分頁資訊）</returns>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM10_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM10_QueryParams queryParams, int rowsPerPage = 0)
        {
            // 建立查詢條件
            var _query = new MdASM10_q
            {
                AC01_Value = companyId,
                AC06_S = queryParams.DepreciationYearMonthStart ?? "",
                AC06_E = queryParams.DepreciationYearMonthEnd ?? "",
                AC04_S = queryParams.DepreciationNoStart ?? "",
                AC04_E = queryParams.DepreciationNoEnd ?? "",
                AC07_S = queryParams.AssetNoStart ?? "",
                AC07_E = queryParams.AssetNoEnd ?? "",
                AA04_Text = queryParams.AssetName ?? "",
                AA26_Value = queryParams.DepreciationAccount ?? "",
                Topic_Value = queryParams.DeptGroup ?? "",
                AD17_S = queryParams.Department ?? "",
                AD17_E = queryParams.Department ?? ""
            };

            // 調用 BLL 層查詢
            return BlASM10.Q_GetList(_query, pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// 查詢財產編號是否存在其他異動資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="assetNo">財產編號</param>
        /// <returns>是否被使用</returns>
        [HttpGet("isExisTXRecord/{companyId}/{assetNo}")]
        public bool IsExisTXRecord(string companyId, string assetNo)
        {
            return BlASM10.IsExisTXRecord(companyId, assetNo);
        }

        /// <summary>
        /// 取得購入單的所有明細
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別</param>
        /// <param name="txType">異動別</param>
        /// <param name="purchaseNo">購入單號</param>
        /// <returns>固定資產購入明細資料模型</returns>
        [HttpGet("ac/details/{companyId}/{docType}/{txType}/{purchaseNo}")]
        public MdApiMessage GetACDetails(string companyId, string docType, string txType, string purchaseNo)
        {
            try
            {
                var _data = BlASM10.D_GetDetail(companyId, docType, txType, purchaseNo);
                
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsSuccess"),
                    _data
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsFailed"),
                    ex
                );
            }
        }

        /// <summary>
        /// D 畫面計算折舊費
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="depreciationYearMonth">折舊年月（格式：YYYYMM）</param>
        /// <returns>折舊費計算結果</returns>
        [HttpPost("calculateDepreciation/{companyId}/{depreciationYearMonth}")]
        public MdApiMessage CalculateDepreciation(string companyId, string depreciationYearMonth)
        {
            try
            {
                var _data = BlASM10.CalculateDepreciation(companyId, depreciationYearMonth);
                
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PanelDescpt_Depreciation") + Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CalculationSuccess"),
                    new { assets = _data }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PanelDescpt_Depreciation") + Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CalculationFailed"),
                    ex
                );
            }
        }

        /// <summary>
        /// D 畫面取得折舊單明細資料（表頭與資產清單）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'05' = 折舊）</param>
        /// <param name="depreciationNo">折舊單號</param>
        /// <returns>表頭與資產明細</returns>
        [HttpGet("getData/{companyId}/{docType}/{txType}/{depreciationNo}")]
        public MdApiMessage D_GetDetail(string companyId, string docType, string txType, string depreciationNo)
        {
            try
            {
                var _details = BlASM10.D_GetDetail(companyId, docType, txType, depreciationNo)?.ToList();
                if (_details == null || _details.Count == 0)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "查無資料");

                var _first = _details[0];
                var _depreciationDate = _first.AC06 ?? "";
                var _depreciationYearMonth = _depreciationDate.Length >= 6 ? _depreciationDate.Substring(0, 6) : "";

                var _header = new
                {
                    companyId = _first.AC01,
                    depreciationDate = _depreciationDate,
                    depreciationYearMonth = _depreciationYearMonth,
                    depreciationNo = _first.AC04,
                    ticketNo = _first.AC26 ?? "",
                    voucherNo = "",
                    makeSlipNo = _first.AC26 ?? ""
                };

                var _assets = _details.Select(d => new
                {
                    assetNo = d.AC07,
                    purchaseDate = d.AC08,
                    assetName = d.AA04 ?? d.AA05,
                    departmentCode = (string)null,
                    departmentName = (string)null,
                    deptGroupCode = (string)null,
                    deptGroupName = (string)null,
                    depreciationAccountCode = d.AA21,
                    depreciationAccountName = d.A1505 ?? "",
                    depreciationAmount = d.AC11 ?? 0,
                    AC06 = d.AC06,
                    AC07 = d.AC07,
                    AC08 = d.AC08,
                    AA04 = d.AA04,
                    AA05 = d.AA05,
                    AE08 = d.AA21,
                    AE09 = (string)null,
                    AE10 = d.AC11
                }).ToList();

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { header = _header, assets = _assets }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Fail") ?? "取得資料失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// 檢查是否存在重複的折舊記錄
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'05' = 折舊）</param>
        /// <param name="depreciationDate">折舊日期（格式：YYYYMMdd）</param>
        /// <returns>重複的折舊記錄列表（包含 AC04 折舊單號）</returns>
        [HttpGet("checkDuplicate/{companyId}/{docType}/{txType}/{depreciationDate}")]
        public MdApiMessage CheckDuplicateDepreciation(string companyId, string docType, string txType, string depreciationDate)
        {
            try
            {
                var _duplicateRecords = BlASM10.CheckDuplicateDepreciation(companyId, docType, txType, depreciationDate);
                
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { duplicateRecords = _duplicateRecords }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CheckDuplicateFailed") ?? "檢查重複記錄失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// 刪除折舊記錄（復原 AA 表的折舊累計，刪除 AD 和 AC 表記錄）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'05' = 折舊）</param>
        /// <param name="depreciationNo">折舊單號</param>
        /// <returns>操作結果</returns>
        [HttpDelete("delete/{companyId}/{docType}/{txType}/{depreciationNo}")]
        public MdApiMessage DeleteDepreciation(string companyId, string docType, string txType, string depreciationNo)
        {
            try
            {
                int _result = BlASM10.DeleteDepreciation(companyId, docType, txType, depreciationNo);
                
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 存檔折舊記錄（新增 AC、AD，更新 AA 折舊累計）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'05' = 折舊）</param>
        /// <param name="request">存檔請求（含表頭與資產明細）</param>
        /// <returns>操作結果（含折舊單號）</returns>
        [HttpPost("save/{companyId}/{docType}/{txType}")]
        public MdApiMessage SaveDepreciation(string companyId, string docType, string txType, [FromBody] MdASM10_SaveRequest request)
        {
            try
            {
                if (request == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                var _result = BlASM10.SaveDepreciation(request, docType, txType);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { depreciationNo = _result.DepreciationNo }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_SaveFailed") ?? "存檔失敗",
                    ex
                );
            }
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion


    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.Models;
using System;
using GUIStd;
using GUIStd.DAL.AllNewAS.Models.Private.vASM14;
using System.Linq;



#endregion


namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM14 固定資產平殘值續折舊計算 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM14Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM14 BlASM14 => new BlASM14(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢Q頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/pageq")]
        public MdASM14_Qh GetQPageHelp()
        {
            return BlASM14.GetQPageHelp();
        }

        /// <summary>
        /// 明細D頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/paged/{companyId}")]
        public MdASM14_Dh GetDPageHelp(string companyId)
        {
            return BlASM14.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 財產選取查詢（D 畫面財產選取對話框）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="queryParams">查詢參數模型（MdASM14_SelectAssetsParams）</param>
        /// <returns>可選取的財產清單</returns>
        [HttpPost("selectAssets/{companyId}")]
        public MdApiMessage D_GetAssetsList(string companyId, [FromBody] MdASM14_SelectAssetsParams queryParams)
        {
            try
            {
                var _list = BlASM14.D_GetAssetsList(companyId ?? "", queryParams ?? new MdASM14_SelectAssetsParams())?.ToList() ?? new List<MdASM14_SelectAsset>();
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { assets = _list }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsFailed") ?? "取得資料失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// D 畫面取得明細資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="extendDate">處理日期</param>
        /// <param name="extendNo">處理單號</param>
        /// <returns>表頭與資產明細（header + assets）</returns>
        [HttpGet("getData/{companyId}/{extendDate}/{extendNo}")]
        public MdApiMessage D_GetDetail(string companyId, string extendDate, string extendNo)
        {
            try
            {
                var (_header, _assets) = BlASM14.D_GetDetail(companyId ?? "", extendNo ?? "");
                if (_header == null || _assets == null || _assets.Count == 0)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "查無資料");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { header = _header, assets = _assets }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsFailed") ?? "取得資料失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// 查詢平殘值續折舊清單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>查詢結果（含分頁資訊）</returns>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM14_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM14_QueryParams queryParams, int rowsPerPage = 0)
        {
            return BlASM14.Q_GetList(companyId ?? "", queryParams ?? new MdASM14_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }



        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 刪除折舊記錄（復原 AA 表的折舊累計，刪除 AD 和 AC 表記錄）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'05' = 折舊）</param>
        /// <param name="extendNo">處理單號</param>
        /// <returns>操作結果</returns>
        [HttpDelete("delete/{companyId}/{docType}/{txType}/{extendNo}")]
        public MdApiMessage DeleteExtendDepreciation(string companyId, string docType, string txType, string extendNo)
        {
            try
            {
                int _result = BlASM14.DeleteExtendDepreciation(companyId, docType, txType, extendNo);

                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 修改平殘值續折舊 - 先刪除原 AC 還原 AA，再依畫面上資產重新新增
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'11' = 平殘值續折舊）</param>
        /// <param name="obj">平殘值續折舊存檔請求（含 extendDate、extendNo、assets）</param>
        /// <returns>操作結果（含處理單號）</returns>
        [HttpPut("update/{companyId}/{docType}/{txType}")]
        public MdApiMessage UpdateExtendDepreciation(string companyId, string docType, string txType, [FromBody] MdASM14_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM14_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM14.UpdateExtendDepreciation(companyId, docType, txType, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "修改失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料修改成功!",
                    new { extendNo = _result.ExtendNo }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_SaveFailed") ?? "修改失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// 存檔平殘值續折舊 - 新增 AC、更新 AA
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別（'2' = 內部憑證）</param>
        /// <param name="txType">異動別（'11' = 平殘值續折舊）</param>
        /// <param name="obj">平殘值續折舊存檔請求 </param>
        /// <returns>操作結果（含處理單號）</returns>
        [HttpPost("insert/{companyId}/{docType}/{txType}")]
        public MdApiMessage SaveExtendDepreciation(string companyId, string docType, string txType, [FromBody] MdASM14_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM14_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM14.SaveExtendDepreciation(companyId, docType, txType, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { extendNo = _result.ExtendNo }
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


    }
}

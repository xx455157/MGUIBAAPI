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
using GUIStd.DAL.AllNewAS.Models.Private.vASM16;
using System.Linq;



#endregion


namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM16 固定資產調撥 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM16Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM16 BlASM16=> new BlASM16(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢Q頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/pageq")]
        public MdASM16_Qh GetQPageHelp()
        {
            return BlASM16.GetQPageHelp();
        }

        /// <summary>
        /// 明細D頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/paged/{companyId}")]
        public MdASM16_Dh GetDPageHelp(string companyId)
        {
            return BlASM16.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 財產選取查詢（D 畫面財產選取對話框）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="queryParams">查詢參數模型</param>
        /// <returns>可選取的財產清單</returns>
        [HttpPost("selectAssets/{companyId}")]
        public MdApiMessage D_GetAssetsList(string companyId, [FromBody] MdASM16_SelectAssetsParams queryParams)
        {
            try
            {
                var _assetsList = BlASM16.D_GetAssetsList(companyId ?? "", queryParams ?? new MdASM16_SelectAssetsParams())?.ToList() ?? new List<MdASM16_SelectAsset>();
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { assets = _assetsList }
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
        /// 查詢固定資產調撥清單（Q 畫面）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="obj">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>查詢結果（含分頁資訊）</returns>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM16_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM16_QueryParams obj, int rowsPerPage = 0)
        {
            return BlASM16.Q_GetList(companyId, obj ?? new MdASM16_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 固定資產調撥新增存檔（寫入 AD、調整 AB）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="obj">表頭與資產明細</param>
        /// <returns>操作結果（含調撥單號）</returns>
        [HttpPost("insert/{companyId}")]
        public MdApiMessage SaveTransferAllocation(string companyId, [FromBody] MdASM16_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM16_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM16.SaveTransferAllocation(companyId, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { transferNo = _result.TransferNo }
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

        /// <summary>
        /// 固定資產調撥修改存檔（還原舊 AB、刪除舊 AD 後依畫面重寫）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="obj">表頭與資產明細</param>
        /// <returns>操作結果（含調撥單號）</returns>
        [HttpPut("update/{companyId}")]
        public MdApiMessage UpdateTransferAllocation(string companyId, [FromBody] MdASM16_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM16_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM16.UpdateTransferAllocation(companyId, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { transferNo = _result.TransferNo }
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

        /// <summary>
        /// 刪除固定資產調撥（還原 AB、刪除 AD）
        /// </summary>
        [HttpDelete("delete/{companyId}/{transferNo}")]
        public MdApiMessage DeleteTransferAllocation(string companyId, string transferNo)
        {
            try
            {
                var _result = BlASM16.DeleteTransferAllocation(
                    companyId ?? "",
                    transferNo ?? "");

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "刪除失敗");

                return HttpContext.Response.DeleteSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion


    }
}

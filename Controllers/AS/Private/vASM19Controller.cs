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
using GUIStd.DAL.AllNewAS.Models.Private.vASM19;



#endregion


namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM19 盤點資料建立 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM19Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM19 BlASM19 => new BlASM19(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢Q頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/pageq")]
        public MdASM19_Qh GetQPageHelp()
        {
            return BlASM19.GetQPageHelp();
        }

        /// <summary>
        /// 明細D頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/paged/{companyId}")]
        public MdASM19_Dh GetDPageHelp(string companyId)
        {
            return BlASM19.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 檢查盤點單號是否已存在
        /// </summary>
        [HttpGet("checkInventoryNoExists/{companyId}/{inventoryNo}")]
        public MdApiMessage CheckInventoryNoExists(string companyId, string inventoryNo)
        {
            try
            {
                bool _isDuplicate = BlASM19.IsInventoryNoExists(companyId ?? "", inventoryNo ?? "");
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { isDuplicate = _isDuplicate }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CheckFailed") ?? "檢查失敗",
                    ex
                );
            }
        }

        /// <summary>
        /// vASM19 D 畫面：取得盤點單明細（對應 WinForms n_ASM19.D_GetDetail）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="inventoryNo">盤點單號</param>
        /// <param name="inventoryDate">盤點日期 YYYYMMDD（選填，有則一併篩選）</param>
        [HttpGet("getData/{companyId}/{inventoryNo}")]
        public MdApiMessage D_GetDetail(string companyId, string inventoryNo, [FromQuery] string inventoryDate = null)
        {
            try
            {
                var (_header, _assets) = BlASM19.D_GetInventoryDetail(companyId ?? "", inventoryNo ?? "", inventoryDate ?? "");
                if (_assets == null || _assets.Count == 0)
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
        /// 財產選取查詢
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="queryParams">查詢參數模型（MdASM19_SelectAssetsParams）</param>
        /// <returns>可選取的財產清單</returns>
        [HttpPost("selectAssets/{companyId}")]
        public MdApiMessage D_GetAssetsList(string companyId, [FromBody] MdASM19_SelectAssetsParams queryParams)
        {
            try
            {
                var _list = BlASM19.D_GetAssetsList(companyId ?? "", queryParams ?? new MdASM19_SelectAssetsParams())?.ToList() ?? new List<MdASM19_SelectAsset>();
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
        /// 查詢財產盤點清單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>查詢結果（含分頁資訊）</returns>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM19_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM19_QueryParams queryParams, int rowsPerPage = 0)
        {
            return BlASM19.Q_GetList(companyId ?? "", queryParams ?? new MdASM19_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 存檔盤點資料（新增 AM 表）
        /// </summary>
        [HttpPost("insert/{companyId}")]
        public MdApiMessage SaveInventory(string companyId, [FromBody] MdASM19_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM19_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var (_success, _message, _inventoryNo) = BlASM19.SaveInventory(obj);

                if (!_success)
                    return Response.SendFailed(_message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { inventoryNo = _inventoryNo }
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
        /// 修改盤點資料
        /// </summary>
        [HttpPut("update")]
        public MdApiMessage UpdateInventory([FromBody] MdASM19_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM19_SaveHeader();

                var (_success, _message, _inventoryNo) = BlASM19.UpdateInventory(obj);

                if (!_success)
                    return Response.SendFailed(_message ?? "資料更新失敗!");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_UpdateOK") ?? "資料修改成功!",
                    new { inventoryNo = _inventoryNo }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_UpdateFail") ?? "資料更新失敗!",
                    ex
                );
            }
        }

        /// <summary>
        /// 刪除盤點單（刪除 AM 表該盤點單之明細，依 AM01+AM02+AM03）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="inventoryNo">盤點單號</param>
        /// <param name="inventoryDate">盤點日期 YYYYMMDD</param>
        [HttpDelete("delete/{companyId}/{inventoryNo}/{inventoryDate}")]
        public MdApiMessage Delete(string companyId, string inventoryNo, string inventoryDate)
        {
            try
            {
                int _result = BlASM19.Delete(companyId, inventoryNo, inventoryDate);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

    }
}

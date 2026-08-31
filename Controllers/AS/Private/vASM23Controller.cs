#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM23;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM23 盤點結果登錄
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASM23Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM23 BlASM23 => mBlASM23 = mBlASM23 ?? new BlASM23(this.ClientContent);
        private BlASM23 mBlASM23;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// Q 畫面輔助資料（公司別清單）
        /// </summary>
        /// <returns>vASM23 Q 畫面輔助資料模型物件</returns>
        [HttpGet("help/pageq")]
        public MdASM23_h GetQPageHelp() => BlASM23.GetQPageHelp();

        /// <summary>
        /// 查詢盤點結果登錄清單（分頁）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數模型（MdASM23_QueryParams）</param>
        /// <param name="rowsPerPage">每頁筆數（0 表示使用系統預設）</param>
        /// <returns>盤點結果登錄清單與分頁資訊</returns>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM23_QueryResult Q_GetList(
            string companyId, int pageNo, [FromBody] MdASM23_QueryParams queryParams, int rowsPerPage = 0)
        {
            return BlASM23.Q_GetList(companyId ?? "", queryParams ?? new MdASM23_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// D 畫面：取得盤點單明細
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="inventoryNo">盤點單號</param>
        /// <param name="inventoryDate">盤點日期 YYYYMMDD</param>
        /// <returns>API 回應（lines 明細陣列）</returns>
        [HttpGet("detail/{companyId}/{inventoryNo}/{inventoryDate}")]
        public MdApiMessage D_GetDetail(string companyId, string inventoryNo, string inventoryDate)
        {
            try
            {
                var _list = BlASM23.D_GetDetail(companyId ?? "", inventoryNo ?? "", inventoryDate ?? "");
                if (_list == null || _list.Count == 0)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "查無資料");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    new { lines = _list });
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsFailed") ?? "取得資料失敗",
                    ex);
            }
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 複盤結果批次存檔
        /// </summary>
        /// <param name="request">複盤存檔請求模型（MdASM23_ReviewRequest）</param>
        /// <returns>API 回應</returns>
        [HttpPut("review")]
        public MdApiMessage UpdateReview([FromBody] MdASM23_ReviewRequest request)
        {
            try
            {
                var _header = request.Header;
                var _rows = BlASM23.UpdateReview(_header.CompanyId, _header.InventoryNo, request.Lines);
                return HttpContext.Response.UpdateSuccess(_rows);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// Excel 匯入複盤結果
        /// </summary>
        /// <param name="request">匯入請求模型（MdASM23_ImportRequest）</param>
        /// <returns>API 回應（含成功/失敗筆數）</returns>
        [HttpPost("import")]
        public MdApiMessage Import([FromBody] MdASM23_ImportRequest request)
        {
            try
            {
                var _result = BlASM23.ImportBatch(request);
                if (_result.Result)
                    return HttpContext.Response.SendSuccess(_result.Message);
                else
                    return HttpContext.Response.SendFailed(_result.Message);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}

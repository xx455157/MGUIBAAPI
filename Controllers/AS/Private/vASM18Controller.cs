#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM18;
using GUIStd.Models;
using System.Linq;
using GUICore.Web.Extensions;
using GUIStd;
using System;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM18 固定資產屬性變更 控制器
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASM18Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM18 BlASM18 => new BlASM18(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢 Q 頁面預設資料（公司別、資產屬性等）
        /// </summary>
        [HttpGet("help/pageq")]
        public MdASM18_Qh GetQPageHelp()
        {
            return BlASM18.GetQPageHelp();
        }

        /// <summary>
        /// 明細D頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/paged/{companyId}")]
        public MdASM18_Dh GetDPageHelp(string companyId)
        {
            return BlASM18.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 查詢固定資產屬性變更清單（Q 畫面，分頁）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM18_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM18_QueryParams queryParams, int rowsPerPage = 0)
        {
            return BlASM18.Q_GetList(companyId, queryParams ?? new MdASM18_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// D 畫面－大批選擇對話框查詢資產（資產類別、資產屬性、帳面為零）
        /// </summary>
        [HttpPost("batch/assets/query/{companyId}")]
        public MdApiMessage GetBatchAssetList(string companyId,[FromBody] MdASM18_BatAssetQueryParams queryParams)
        {
            try
            {
                var _assetsList = BlASM18.GetBatchAssetList(companyId ?? "", queryParams ?? new MdASM18_BatAssetQueryParams())?.ToList() ?? new List<MdASM18_GetAssetList>();
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
        /// D 畫面取得明細資料。
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="changeDate">變更日期 yyyyMMdd</param>
        /// <param name="changeNo">變更單號</param>
        [HttpGet("getData/{companyId}/{changeDate}/{changeNo}")]
        public MdApiMessage D_GetDetail(string companyId, string changeDate, string changeNo)
        {
            try
            {
                var (_header, _assets) = BlASM18.D_GetDetail(companyId ?? "", changeDate ?? "", changeNo ?? "");
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
        /// D 畫面新增存檔
        /// </summary>
        [HttpPost("insert/{companyId}/{docType}/{txType}")]
        public MdApiMessage SavePropertyChange(string companyId, string docType, string txType, [FromBody] MdASM18_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM18_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM18.SavePropertyChange(companyId, docType, txType, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { changeNo = _result.ChangeNo }
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
        /// D 畫面修改存檔
        /// </summary>
        [HttpPut("update/{companyId}/{docType}/{txType}")]
        public MdApiMessage UpdatePropertyChange(string companyId, string docType, string txType, [FromBody] MdASM18_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM18_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM18.UpdatePropertyChange(companyId, docType, txType, obj);

                if (!_result.Success)
                    return Response.SendFailed(_result.Message ?? "存檔失敗");

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "資料存檔成功!",
                    new { changeNo = _result.ChangeNo }
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
        /// D 畫面刪除
        /// </summary>
        [HttpDelete("delete/{companyId}/{docType}/{txType}/{changeNo}")]
        public MdApiMessage DeletePropertyChange(string companyId, string docType, string txType, string changeNo)
        {
            try
            {
                int _result = BlASM18.DeletePropertyChange(companyId, docType, txType, changeNo);

                if (_result <= 0)
                    return Response.SendFailed(
                        Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "查無資料"
                    );

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

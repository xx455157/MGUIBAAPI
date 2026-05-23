#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM09;
using GUIStd.Models;
using GUICore.Web.Extensions;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM09 固定資產年限變更
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASM09Controller : GUIAppAuthController
    {
        private BlASM09 BlASM09 => new BlASM09(ClientContent);

        /// <summary>
        /// Q 畫面輔助資料。
        /// <returns>系統參數代碼模型集合物件</returns>
        /// </summary>
        [HttpGet("help/pageq")]
        public MdASM09_Qh GetQPageHelp()
        {
            return BlASM09.GetQPageHelp();
        }

        /// <summary>
        /// D 畫面輔助資料。
        /// <returns>系統參數代碼模型集合物件</returns>
        /// </summary>
        [HttpGet("help/paged/{companyId}")]
        public MdASM09_Dh GetDPageHelp(string companyId)
        {
            return BlASM09.GetDPageHelp(companyId);
        }

        /// <summary>
        /// Q 清單分頁查詢。
        /// <param name="companyId">公司代號</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數模型物件</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>分頁查詢結果模型物件</returns>
        /// </summary>
        [HttpPost("pageQGetList/{companyId}/pages/{pageNo}")]
        public MdASM09_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM09_QueryParams queryParams, int rowsPerPage = 0)
        {
            return BlASM09.Q_GetList(companyId, queryParams ?? new MdASM09_QueryParams(), pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// D 明細資料查詢。
        /// <param name="companyId">公司代號</param>
        /// <param name="changeDate">變更日期</param>
        /// <param name="changeNo">變更單號</param>
        /// <returns>明細資料查詢結果模型物件</returns>
        /// </summary>
        [HttpGet("getData/{companyId}/{changeDate}/{changeNo}")]
        public MdApiMessage D_GetDetail(string companyId, string changeDate, string changeNo)
        {
            try
            {
                var (_header, _assets) = BlASM09.D_GetDetail(companyId ?? "", changeDate ?? "", changeNo ?? "");
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
        /// D 新增存檔。
        /// <param name="companyId">公司代號</param>
        /// <param name="docType">單據類型</param>
        /// <param name="txType">交易類型</param>
        /// <param name="obj">新增存檔資料模型物件</param>
        /// <returns>新增存檔結果模型物件</returns>
        /// </summary>
        [HttpPost("insert/{companyId}/{docType}/{txType}")]
        public MdApiMessage SaveLifeYearChange(string companyId, string docType, string txType, [FromBody] MdASM09_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM09_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM09.SaveLifeYearChange(companyId, docType, txType, obj);

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
        /// D 修改存檔。
        /// <param name="companyId">公司代號</param>
        /// <param name="docType">單據類型</param>
        /// <param name="txType">交易類型</param>
        /// <param name="obj">修改存檔資料模型物件</param>
        /// <returns>修改存檔結果模型物件</returns>
        /// </summary>
        [HttpPut("update/{companyId}/{docType}/{txType}")]
        public MdApiMessage UpdateLifeYearChange(string companyId, string docType, string txType, [FromBody] MdASM09_SaveRequest obj)
        {
            try
            {
                if (obj == null)
                    return Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoData") ?? "無存檔資料");

                if (obj.Header == null)
                    obj.Header = new MdASM09_SaveHeader();
                obj.Header.CompanyId = companyId ?? obj.Header.CompanyId;

                var _result = BlASM09.UpdateLifeYearChange(companyId, docType, txType, obj);

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
        /// D 刪除整單。
        /// <param name="companyId">公司代號</param>
        /// <param name="docType">單據類型</param>
        /// <param name="txType">交易類型</param>
        /// <param name="changeNo">變更單號</param>
        /// <returns>刪除整單結果模型物件</returns>
        /// </summary>
        [HttpDelete("delete/{companyId}/{docType}/{txType}/{changeNo}")]
        public MdApiMessage DeleteLifeYearChange(string companyId, string docType, string txType, string changeNo)
        {
            try
            {
                int _result = BlASM09.DeleteLifeYearChange(companyId, docType, txType, changeNo);

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
    }
}

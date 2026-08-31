#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUICore.Web.Attributes;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM01;
using GUIStd.Models;
using GUIStd;
using Microsoft.Extensions.Logging;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM01 固定資產整批匯入 控制器
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>vASM01 固定資產整批匯入商業邏輯</summary>
        private BlASM01 BlASM01 => mBlASM01 = mBlASM01 ?? new BlASM01(this.ClientContent);
        private BlASM01 mBlASM01;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得匯入頁 UI 初始化資料（公司別、欄位對應、必須對應欄位、折舊方式等）
        /// </summary>
        /// <param name="companyId">公司別；空白時回傳基礎資料並預設帶首個公司別的相依清單</param>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdASM01_h GetUiDate([FromQuery] string companyId = "")
        {
            return string.IsNullOrEmpty(companyId)
                ? BlASM01.GetUiDate()
                : BlASM01.GetUiDate(companyId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 整批匯入固定資產（寫入 AA、AC、AD、AB，並更新 AE）
        /// </summary>
        /// <param name="request">匯入請求（公司別、導入日期、單號、明細列）</param>
        /// <returns>操作結果；成功時回傳 importNo、savedCount、skippedCount</returns>
        [HttpPost("import")]
        public MdApiMessage Import([FromBody] MdASM01_ImportRequest request)
        {
            try
            {
                var _result = BlASM01.ImportBatch(request);
                if (_result.Infos.Length > 0)
                    Logging.Logger.LogWarning($"{Environment.NewLine}{_result.Infos.ToString()}");
                // 業務邏輯驗證失敗（無資料、欄位錯誤等）
                if (!_result.Result)
                {
                    return HttpContext.Response.SendFailed(_result.Message);
                }
                return HttpContext.Response.SendSuccess(
                    _result.Message,
                    new { importNo = _result.ImportNo, savedCount = _result.SavedCount, skippedCount = _result.SkippedCount });
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion
    }
}

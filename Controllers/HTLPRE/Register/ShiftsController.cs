#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Config
{
    /// <summary>
    /// 班別資料控制器
    /// </summary>
    [Route("htlpre/Config/[controller]")]
    public class ShiftsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTSH BlHTSH => new BlHTSH(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得班別資料
        /// </summary>
        /// <param name="bkDate">會計日期（格式: YYYYMMDD）</param>
        /// <param name="typeCode">類型代碼</param>
        /// <param name="workstation">工作站/機台</param>
        /// <returns>班別代碼</returns>
        [HttpGet("GetShift")]
        public string Get([FromQuery] string bkDate, [FromQuery] string typeCode, [FromQuery] string workstation)
        {
            return BlHTSH.GetHotelShift(bkDate, typeCode, workstation);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增班別設定
        /// </summary>
        /// <param name="obj">班別資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        /// <remarks>
        /// 請求格式（JSON）：
        /// {
        ///     "SH02": "20251030",  // 會計日期
        ///     "SH03": "",          // 時間代碼（可選）
        ///     "SH04": "O",         // 類型代碼
        ///     "SH05": "03",        // 固定為 '03'（班別類型）
        ///     "SH08": "A",         // 班別代碼
        ///     "SH09": "STATION01", // 工作站/機台
        ///     "SH001": 0           // 序號（可選）
        /// }
        /// </remarks>
        [HttpPost()]
        public MdApiMessage AddShift([FromBody] MdHTSH obj)
        {
            try
            {
                // 基本驗證
                if (obj == null)
                {
                    return HttpContext.Response.InsertFailed(new ArgumentNullException(nameof(obj)), "班別資料不得為空");
                }

                // 確保 SH05 固定為 '03'（班別類型）
                obj.SH05 = "03";

                // 呼叫商業邏輯層執行新增
                int result = BlHTSH.ProcessInsert(obj);

                // 回應前端執行成功訊息
                return HttpContext.Response.InsertSuccess(result, "班別儲存成功");
            }
            catch (Exception ex)
            {
                // 回應前端執行失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }
        #endregion
    }
}

#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Register
{
    /// <summary>
    /// 班別資料控制器
    /// </summary>
    /// <remarks>
    /// 用於 vHTRGM09（客房平面圖）等前端頁面的班別資料查詢與異動。
    /// 
    /// API 端點：
    ///   - GET  htlpre/register/Shifts - 取得當前班別（參數：bkDate, typeCode, workstation）
    ///   - POST htlpre/register/Shifts - 新增/儲存班別設定
    /// </remarks>
    [Route("htlpre/register/[controller]")]
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
        /// 取得飯店班別
        /// </summary>
        /// <param name="bkDate">會計日期（格式: YYYYMMDD）</param>
        /// <param name="typeCode">類型代碼</param>
        /// <param name="workstation">工作站/機台</param>
        /// <returns>班別代碼（如 'A', 'B', 'C'...），若無資料則回傳空字串</returns>
        [HttpGet()]
        public string GetShift([FromQuery] string bkDate, [FromQuery] string typeCode, [FromQuery] string workstation)
        {
            try
            {
                // 防呆：若 bkDate 為空，使用當天日期
                if (string.IsNullOrWhiteSpace(bkDate))
                {
                    bkDate = DateTime.Now.ToString("yyyyMMdd");
                }

                // 呼叫商業邏輯層取得班別
                string shift = BlHTSH.GetHotelShift(bkDate, typeCode, workstation);
                return shift ?? string.Empty;
            }
            catch (Exception ex)
            {
                // 發生錯誤時記錄並回傳空字串
                Console.WriteLine($"[ShiftsController] GetShift error: {ex.Message}");
                return string.Empty;
            }
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
        ///     "SH04": "I",         // 類型代碼
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
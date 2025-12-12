using Microsoft.AspNetCore.Mvc;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using System;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.BLL.AllNewPY.Share;
using GUIStd.DAL.AllNewPY.Models.Share.SM;

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// PY系統假日設定控制器
    /// </summary>
    [ApiController]
    [Route("py/[controller]")]
    public class HolidayController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSM BlSM => mBlSM = mBlSM ?? new BlSM(this.ClientContent);
        private BlSM mBlSM;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得假日設定資料
        /// </summary>
        /// <returns>假日設定模型物件</returns>
        [HttpGet("help")]
        public MdHolidayConfigSettings GetHolidayConfig()
        {
            return BlSM.GetHolidayConfig();
        }

        /// <summary>
        /// 取得分頁休假日設定資料
        /// </summary>
        /// <param name="startDate">開始日期(YYYYMMDD)</param>
        /// <param name="endDate">結束日期(YYYYMMDD)</param>
        /// <param name="holidayType">假日類型 (B:國定假日、C:休息日、D:例假日) - 可選</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>分頁休假日設定資料模型物件</returns>
        [HttpPost("query/{startDate}/{endDate}/pages/{pageNo}")]
        public MdHolidaySchedules_p GetData(
            string startDate, string endDate,
            [FromQuery] string holidayType,
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlSM.GetData(startDate, endDate, holidayType, ControlName, pageNo);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 存檔假日設定資料 - 大批存檔與設定值       
        /// </summary>
        /// <param name="obj">存檔請求物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("save")]
        public MdApiMessage Save([FromBody] MdHolidayConfigSettings obj)
        {
            try
            {
                // 呼叫商業元件執行存檔作業
                int _result = BlSM.ProcessSave(obj);

                // 回應前端存檔成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端存檔失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改假日設定資料
        /// </summary>
        /// <param name="date">日期 (SM01)</param>
        /// <param name="obj">更新請求物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{date}")]
        public MdApiMessage Update(string date, [FromBody] MdHolidaySchedule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlSM.ProcessUpdate(date, obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除假日設定資料
        /// </summary>
        /// <param name="date">日期 (SM01)</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{date}")]
        public MdApiMessage Delete(string date)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlSM.ProcessDelete(date);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}

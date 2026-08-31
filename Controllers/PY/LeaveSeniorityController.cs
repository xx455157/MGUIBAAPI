#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】特休假年資核給天數控制器（ARTHPY.AG，vABTBM02；路由 py/leaveSeniority）
    /// </summary>
    [Route("py/leaveSeniority")]
    public class LeaveSeniorityController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlAG BlAG => new BlAG(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次之特休假年資核給天數
        /// </summary>
        /// <param name="leaveTypeCode">假別代碼（AG05）；必填</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數；0 表示由商業層決定</param>
        /// <returns>分頁資料模型物件</returns>
        [HttpPost("query/{leaveTypeCode}/pages/{pageNo}")]
        public MdLeaveSeniorities_p GetData(
            string leaveTypeCode,
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlAG.GetData(ControlName, pageNo, ref rowsPerPage, leaveTypeCode);
        }

        /// <summary>
        /// 取得單筆特休假年資核給天數
        /// </summary>
        /// <param name="serviceYears">服務年資（AG01）</param>
        /// <param name="hireMonthDayFrom">到職月日1（AG02）</param>
        /// <param name="hireMonthDayTo">到職月日2（AG03）</param>
        /// <param name="leaveTypeCode">假別代碼（AG05）</param>
        /// <returns>資料列模型物件</returns>
        [HttpGet("{serviceYears}/{hireMonthDayFrom}/{hireMonthDayTo}/{leaveTypeCode}")]
        public MdLeaveSeniority GetRow(
            decimal serviceYears,
            string hireMonthDayFrom,
            string hireMonthDayTo,
            string leaveTypeCode)
        {
            return BlAG.GetRow(serviceYears, hireMonthDayFrom, hireMonthDayTo, leaveTypeCode);
        }

        /// <summary>
        /// 判斷複合鍵是否已存在
        /// </summary>
        /// <param name="serviceYears">服務年資（AG01）</param>
        /// <param name="hireMonthDayFrom">到職月日1（AG02）</param>
        /// <param name="hireMonthDayTo">到職月日2（AG03）</param>
        /// <param name="leaveTypeCode">假別代碼（AG05）</param>
        /// <returns>已存在為 true</returns>
        [HttpGet("exists/{serviceYears}/{hireMonthDayFrom}/{hireMonthDayTo}/{leaveTypeCode}")]
        public bool IsExist(
            decimal serviceYears,
            string hireMonthDayFrom,
            string hireMonthDayTo,
            string leaveTypeCode)
        {
            return BlAG.IsExist(serviceYears, hireMonthDayFrom, hireMonthDayTo, leaveTypeCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">特休假年資核給天數資料列模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdLeaveSeniority obj)
        {
            try
            {
                var _n = BlAG.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_n);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料（路徑為原複合鍵；本文為 <see cref="MdLeaveSeniority"/>，更新 AG01、AG04）
        /// </summary>
        /// <param name="serviceYears">原服務年資（AG01）</param>
        /// <param name="hireMonthDayFrom">原到職月日1（AG02）</param>
        /// <param name="hireMonthDayTo">原到職月日2（AG03）</param>
        /// <param name="leaveTypeCode">原假別代碼（AG05）</param>
        /// <param name="obj">修改後資料列模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{serviceYears}/{hireMonthDayFrom}/{hireMonthDayTo}/{leaveTypeCode}")]
        public MdApiMessage Update(
            decimal serviceYears,
            string hireMonthDayFrom,
            string hireMonthDayTo,
            string leaveTypeCode,
            [FromBody] MdLeaveSeniority obj)
        {
            try
            {
                var _n = BlAG.ProcessUpdate(
                    serviceYears,
                    hireMonthDayFrom,
                    hireMonthDayTo,
                    leaveTypeCode,
                    obj);
                return HttpContext.Response.UpdateSuccess(_n);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="serviceYears">服務年資（AG01）</param>
        /// <param name="hireMonthDayFrom">到職月日1（AG02）</param>
        /// <param name="hireMonthDayTo">到職月日2（AG03）</param>
        /// <param name="leaveTypeCode">假別代碼（AG05）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{serviceYears}/{hireMonthDayFrom}/{hireMonthDayTo}/{leaveTypeCode}")]
        public MdApiMessage Delete(
            decimal serviceYears,
            string hireMonthDayFrom,
            string hireMonthDayTo,
            string leaveTypeCode)
        {
            try
            {
                var _n = BlAG.ProcessDelete(
                    serviceYears,
                    hireMonthDayFrom,
                    hireMonthDayTo,
                    leaveTypeCode);
                return HttpContext.Response.DeleteSuccess(_n);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}

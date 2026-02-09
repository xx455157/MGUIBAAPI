#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewPY;
using GUIStd.BLL.AllNewPY.Private; 
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 排班資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class ShiftsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlAO BlAO => new BlAO(ClientContent);
        private BlAQ BlAQ => new BlAQ(ClientContent);
        private BlShifts BlShifts => new BlShifts(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得員工排班資訊
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="date">排班日</param>
        /// <returns>排班資料泛型集合物件</returns>
        [HttpGet("{employeeId}/{date}")]
        public IEnumerable<MdShift> GetShifts(string employeeId, string date)
        {
            return BlAO.GetEmployeeShifts(employeeId, date);
        }

        /// <summary>
        /// 取得員工排班資訊(含打卡容許時間)
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="date">排班日</param>
        /// <returns>排班資料(含打卡容許時間)泛型集合物件</returns>
        [HttpGet("{employeeId}/{date}/includeRange")]
        public MdShiftInfo GetShiftsExtended(string employeeId, string date)
        {
            return BlShifts.GetEmployeeShiftsExtend(employeeId, date);
        }

        /// <summary>
        /// 取得員工工作日打卡資料
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="date">排班日</param>
        /// <returns>打卡資料泛型集合物件</returns>
        [HttpGet("punch/{employeeId}/{date}")]
        public IEnumerable<MdPunch> GetPunch(string employeeId, string date)
        {
            return BlAQ.GetEmployeePunchTime(employeeId, date);
        }

        /// <summary>
        /// 取得班別基本資料（未停用，包含考勤資料）
        /// </summary>
        /// <returns>班別基本資料（包含 AL、AN 資料和考勤資料）</returns>
        [HttpGet("basic")]
        public IEnumerable<MdShiftBasic> GetBasicShifts()
        {
            return BlShifts.GetBasicShifts();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 更新班別歸屬資料
        /// </summary>
        /// <param name="assignment">班別更換歸屬資料模型</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("updateAssignment")]
        public MdApiMessage UpdateShiftAssignment([FromBody] MdShiftAssignment assignment)
        {
            try
            {
                // 呼叫商業元件執行更新作業
                int _result = BlAQ.ProcessShiftChange(assignment);

                // 回應前端更新成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端更新失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}

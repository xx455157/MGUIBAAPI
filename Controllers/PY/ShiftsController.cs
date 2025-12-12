#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.BLL.AllNewPY.Private; 
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private;

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

        #endregion
    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.DeptAtts;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewPY.Models.Private.PersonalAttend;

#endregion

namespace MGUIBAAPI.Controllers.PY
{

    /// <summary>
    /// 部門出勤資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class PersonalAttendController : GUIAppAuthController
    {
        #region " 私用屬性 "
        private BlPersonalAttend BlPersonalAttend => new BlPersonalAttend(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢 個人考勤資料
        /// 
        /// </summary>
        /// <param name="startDate">查詢起日</param>
        /// <param name="endDate">查詢迄日</param>
        /// <returns></returns>
        [HttpGet("query/{startDate}/{endDate}/pages/{pageNo}")]
        public MdPersonalAttendReport_p GetData(string startDate, string endDate ,[DARange(1, int.MaxValue)] int pageNo, [FromQuery] string funcName)
        {
            return BlPersonalAttend.GetData(startDate, endDate, funcName, pageNo);
        }

        #endregion
    }
}

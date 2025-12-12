#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private.DeptAtts;
using System.Linq;
using GUIStd.BLL.OA.Private;
using GUIStd.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.PY
{

    /// <summary>
    /// 部門出勤資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class DeptAttsController : GUIAppAuthController
    {
        #region " 私用屬性 "
        private BlDeptAtts BlDeptAtts => new BlDeptAtts(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢 部門出勤資料
        /// 
        /// </summary>
        /// <param name="obj">查詢參數</param>
        /// <returns></returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdDeptAttsReport_p GetData([FromBody] MdDeptAttsQuery obj, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] string funcName)
        {
            return BlDeptAtts.GetData(obj, funcName, pageNo);
        }

        #endregion
    }
}

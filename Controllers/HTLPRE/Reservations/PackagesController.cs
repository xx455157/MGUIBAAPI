#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.BLL.AllNewHTL.Private;

#endregion
namespace MGUIBAAPI.Controllers.HTLPRE.Reservations
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("htlpre/reservations/[controller]")]
    public class PackagesController : GUIAppAuthController
    {

        /// <summary>
        /// 取得專案資料(未實作)
        /// </summary>
        /// <param name="startDate">查詢範圍-起</param>
        /// <param name="endDate">查詢範圍-迄</param>
        /// <param name="rvType">訂房類別(FIT/GIT)</param>
        /// <returns></returns>
        [HttpPost("query/{startDate}/{endDate}/{rvType}")]
        public object GetData(string startDate, string endDate, string rvType)
        {
            return null;
        }
    }
}

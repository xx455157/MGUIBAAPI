#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTRCR01;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTBQR09;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    ///  vHTBQR09程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTBQR09Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlHTBQR09 BlHTBQR09 => new BlHTBQR09(ClientContent);

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.HTLPRE;

        #endregion

        #region " 共用函式 - 查詢資料 "

        ///// <summary>
        ///// 取得vHTBQR09畫面輔助資料
        ///// </summary>
        //[HttpGet("page")]
        //public MdHTBQR09_h GetUIData() => BlHouseKeeping.GetUIDataRCR01();

        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdHTBQR09_q> obj)
        {
            // 建立報表
            obj.Query.IsCultureEN = (CurrentUILang == Common.CultureEN ? "1" : "0");
            var _info = await BlHTBQR09.GetReport(obj);

            // 回傳報表檔案
            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }


        #endregion

    }
}

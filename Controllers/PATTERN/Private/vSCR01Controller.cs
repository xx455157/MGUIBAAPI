#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.GUI.Private;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.DAL.GUI.Models.Private.vSCR01;

#endregion

namespace MGUIBAAPI.Controllers.Pattern.Private
{
    /// <summary>
    /// 【需經驗證】PTN使用日誌報表控制器
    /// </summary>
    [Route("pattern/private/[controller]")]
    public class vSCR01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlvSCR01 BlMain => new BlvSCR01(ClientContent);

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;

        #endregion

        #region " 共用函式 - 查詢資料 "



        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdSCR01_q> obj)
        {
            // 建立報表
            var _info = await BlMain.GetReport(obj);
            
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

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
using GUIStd.DAL.AllNewIV.Models.Private.vIVM16;
using GUIStd.BLL.AllNewIV.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.DAL.AllNewAS.Models.Private.Purchased;
using GUIStd.DAL.AllNewAS.Models.Private.vASR03;
using GUIStd.BLL.AllNewAS.Private;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
    /// <summary>
    /// 使用日誌報表控制器
    /// </summary>
    [Route("netgui/private/[controller]")]
    public class vSCR01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlvSCR0102 BlMain => new BlvSCR0102(ClientContent);

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        ///  取得畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata")]
        public MdSCR0102_h GetUIData()
        {
            return BlMain.GetUIData();
        }


        /// <summary>
        /// 取得程式資料（分頁）
        /// </summary>
        /// <param name="pageNo">查詢頁次（若 <= 0 表示取得全部資料）</param>
        /// <param name="systemId">系統代號 (A1003)</param>
        /// <param name="queryText">查詢文字（可選，模糊比對 A1001、A1002）</param>
        /// <returns>程式資料分頁模型物件</returns>
        [HttpGet("query/syspgms/pages/{pageNo}")]
        public MdSysPgm_p GetSysPgms([DARange(0, int.MaxValue)] int pageNo ,[FromQuery] string systemId="" ,[FromQuery] string queryText="")
        {
            return BlMain.GetSysPgms(pageNo, $"{ControlName}.GetSysPgms", systemId,  queryText);
        }

        /// <summary>
        /// 報表畫面資料(web)
        /// </summary>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("query/qlist/pages/{pageNo}")]
        public async Task<MdSCR0102_rpt_p> GetQueryList([FromBody] MdSCR0102_q queryParams, [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return await BlMain.GetQueryList(queryParams, ControlName, pageNo, rowsPerPage);
        }

        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdSCR0102_q> obj)
        {
            // 建立報表
            var _report = await BlMain.GetReport(obj);

            // 回傳報表檔案
            if (_report.Contents != null)
                return HttpContext.Response.SendFile(_report.Contents, _report.FileName);

            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_report.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_report.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }

        #endregion
    }
}

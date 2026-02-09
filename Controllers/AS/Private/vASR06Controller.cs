#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewAS.Models.Private.vASR06;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd.DAL.AllNewAS.Models.Private.Sold;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR06固定資產出售程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR06Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlASR06 BlASR06 => new BlASR06(ClientContent);

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("page")]
        public MdASR06_h GetUIData()
        {
            return BlASR06.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR06.GetSYSDefault(companies);
        }

        /// <summary>
        /// 報表畫面資料(web)
        /// </summary>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("Q_GetList/pages/{pageNo}")]
        public MdASR06_p Q_GetList([FromBody] MdSold_q queryParams, [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return BlASR06.Q_GetList(queryParams, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdSold_q> obj)
        {
            // 建立報表
            var _info = await BlASR06.GetReport(obj, false);

            // 回傳報表檔案
            if (_info.Contents != null)
            {
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);
            }
            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }

        #endregion

    }
}

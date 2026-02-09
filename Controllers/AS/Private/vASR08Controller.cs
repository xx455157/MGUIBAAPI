#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR08;
using GUIStd.DAL.AllNewAS.Models.Private.Capitalize;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR8固定資產資本化程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR08Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlASR08 BlASR08 => new BlASR08(ClientContent);

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
        public MdASR08_h GetUIData()
        {
            return BlASR08.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR08.GetSYSDefault(companies);
        }

        /// <summary>
        /// 報表畫面資料(web)
        /// </summary>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("Q_GetList/pages/{pageNo}")]
        public MdASR08_p Q_GetList([FromBody] MdCapitalize_q queryParams, [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return BlASR08.Q_GetList(queryParams, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdCapitalize_q> obj)
        {
            // 建立報表
            var _info = await BlASR08.GetReport(obj, false);

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

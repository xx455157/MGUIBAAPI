#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR23;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR23 盤點紀錄表（篩選＋Excel／PDF 下載）
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR23Controller : GUIAppAuthController
    {
        private BlASR23 BlASR23 => new BlASR23(ClientContent);

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別）
        /// </summary>
        [HttpGet("page")]
        public MdASR23_h GetUIData()
        {
            return BlASR23.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料（小數位）
        /// </summary>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR23.GetSYSDefault(companies);
        }

        /// <summary>
        /// 依篩選條件取得盤點單索引（供前端逐筆下載，一張盤點單一個 Excel／PDF）
        /// </summary>
        [HttpPost("report/inventorysheets")]
        public async Task<IActionResult> GetInventorySheetIndex([FromBody] MdPhysicalInventoryReport_q query)
        {
            var _rows = await BlASR23.GetInventorySheetIndexAsync(query ?? new MdPhysicalInventoryReport_q());
            return Ok(_rows);
        }

        /// <summary>
        /// 產生報表檔（盤點紀錄表 Excel／PDF）
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdPhysicalInventoryReport_q> obj)
        {
            var _info = await BlASR23.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR19;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR19 盤點表（篩選＋明細分頁＋Excel／PDF 下載）。
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR19Controller : GUIAppAuthController
    {
        private BlASR19 BlASR19 => new BlASR19(ClientContent);

        /// <summary>
        /// 報表的系統代號。
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別）。
        /// </summary>
        [HttpGet("page")]
        public MdASR19_h GetUIData()
        {
            return BlASR19.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料（小數位）。
        /// </summary>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR19.GetSYSDefault(companies);
        }

        /// <summary>
        /// 查詢盤點表明細（分頁）。
        /// </summary>
        [HttpPost("getList/pages/{pageNo}")]
        public async Task<MdInventorySheet_p> GetList(
            [FromBody] MdPhysicalInventorySheetReport_q queryParams,
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return await BlASR19.GetListAsync(queryParams ?? new MdPhysicalInventorySheetReport_q(), ControlName, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 依篩選條件取得盤點單索引（供前端逐筆下載，一張盤點單一個 Excel／PDF）。
        /// </summary>
        [HttpPost("report/inventorysheets")]
        public async Task<IActionResult> GetInventorySheetIndex([FromBody] MdPhysicalInventorySheetReport_q query)
        {
            var _rows = await BlASR19.GetInventorySheetIndexAsync(query ?? new MdPhysicalInventorySheetReport_q());
            return Ok(_rows);
        }

        /// <summary>
        /// 產生報表檔（盤點表 Excel／PDF）。
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdPhysicalInventorySheetReport_q> obj)
        {
            var _info = await BlASR19.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR24;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR24 固定資產清冊
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR24Controller : GUIAppAuthController
    {
        private BlASR24 BlASR24 => new BlASR24(ClientContent);

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別、報表代號清單）
        /// </summary>
        [HttpGet("page")]
        public async Task<MdASR24_h> GetUIData()
        {
            return await BlASR24.GetUIDataAsync();
        }

        /// <summary>
        /// 以公司別取得系統預設資料（小數位）
        /// </summary>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR24.GetSYSDefault(companies);
        }

        /// <summary>
        /// 報表代號協助（與 page 之 reports 相同）
        /// </summary>
        [HttpGet("report/help")]
        public async Task<IActionResult> GetReportHelp()
        {
            var _rows = await BlASR24.GetReportHelpAsync();
            return Ok(_rows);
        }

        /// <summary>
        /// 儲存欄位勾選與顯示順序（A49）
        /// </summary>
        [HttpPost("fields")]
        public IActionResult SaveFieldLayout([FromBody] MdASR24SaveFields_q body)
        {
            var _n = BlASR24.SaveFieldLayout(body);
            return Ok(new { affected = _n });
        }

        /// <summary>
        /// 欄位列（SINI ReportFiles_ASR24A；對應 Win GetSINIHelp）
        /// </summary>
        [HttpGet("fields/rows")]
        public async Task<IActionResult> GetFieldRows([FromQuery] string reportId, [FromQuery] string showBudget = "N")
        {
            var _rows = await BlASR24.GetFieldRowsAsync(new MdASR24FieldRows_q
            {
                ReportId = reportId,
                ShowBudget = showBudget,
            });
            return Ok(_rows);
        }

        /// <summary>
        /// 產生報表檔
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdASR24Report_q> obj)
        {
            var _info = await BlASR24.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

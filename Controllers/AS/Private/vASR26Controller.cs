#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR26;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR26 固定資產各月折舊報表
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR26Controller : GUIAppAuthController
    {
        private BlASR26 BlASR26 => new BlASR26(ClientContent);

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別、大部門、折舊科目、資產屬性）
        /// </summary>
        [HttpGet("page")]
        public MdASR26_h GetUIData()
        {
            return BlASR26.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料（小數位）
        /// </summary>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR26.GetSYSDefault(companies);
        }

        /// <summary>
        /// 產生報表檔（固定資產各月折舊 Excel）
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdMonthlyDepreciation_q> obj)
        {
            var _info = await BlASR26.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

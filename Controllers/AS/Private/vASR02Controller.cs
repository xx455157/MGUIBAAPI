#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR02;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR02 固定資產目錄程式資料控制器（篩選＋Excel 下載）
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR02Controller : GUIAppAuthController
    {
        private BlASR02 BlASR02 => new BlASR02(ClientContent);

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別、大部門）
        /// </summary>
        [HttpGet("page")]
        public MdASR02_h GetUIData()
        {
            return BlASR02.GetUIData();
        }

        /// <summary>
        /// 以公司別取得系統預設資料（小數位）
        /// </summary>
        [HttpPost("page/defaultsettingbycompany")]
        public MdDefaultSetting GetSYSDefault([FromBody] string[] companies)
        {
            return BlASR02.GetSYSDefault(companies);
        }

        /// <summary>
        /// 依公司別取得大部門清單（B74：B7401→B7402，名稱取 SINI DeptGroupName）
        /// </summary>
        /// <param name="companyId">公司別代碼</param>
        /// <returns>大部門輔助資料（id、name）</returns>
        [HttpGet("departments/{companyId}")]
        public IEnumerable<MdCode> GetDeptGroupsByCompany(string companyId)
        {
            return BlASR02.GetDeptGroupsByCompany(companyId);
        }

        /// <summary>
        /// 產生報表檔（固定資產目錄 Excel）
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdFixedAssetCatalog_q> obj)
        {
            var _info = await BlASR02.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

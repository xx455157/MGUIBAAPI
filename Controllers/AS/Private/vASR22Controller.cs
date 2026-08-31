#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR22;
using GUIStd.DAL.Base.Models.Reports;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR22 固定資產標籤列印
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASR22Controller : GUIAppAuthController
    {
        private BlASR22 BlASR22 => new BlASR22(ClientContent);

        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 查詢頁面預設資料（公司別）
        /// </summary>
        [HttpGet("page")]
        public MdASR22_h GetUIData()
        {
            return BlASR22.GetUIData();
        }

        /// <summary>
        /// 查詢筆數
        /// </summary>
        [HttpPost("getList/count")]
        public MdASR22_Count GetListCount([FromBody] MdASR22_q queryParams, int rowsPerPage = 0)
        {
            return BlASR22.GetListCount(queryParams ?? new MdASR22_q(), ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// 查詢標籤列印清單（分頁）
        /// </summary>
        [HttpPost("getList/pages/{pageNo}")]
        public MdASR22_p GetList([FromBody] MdASR22_q queryParams, [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return BlASR22.GetList(queryParams ?? new MdASR22_q(), ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 標籤列印 PDF（Word 套表）
        /// </summary>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdASR22Report_q> obj)
        {
            
            var _info = await BlASR22.GetReport(obj, false);

            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }
    }
}

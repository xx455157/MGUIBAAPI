#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASP12;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASP12 固定資產科目變更
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASP12Controller : GUIAppAuthController
    {
        private BlASP12 BlASP12 => new BlASP12(ClientContent);

        /// <summary>
        /// 取得 UI 初始化資料（查詢頁：公司別、資產科目類型選項）
        /// </summary>
        [HttpGet("query/uidata")]
        public MdASP12_h GetUIData()
        {
            return BlASP12.GetUIData();
        }

        /// <summary>
        /// 查詢財產清單（一次載入全部列）。
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        [HttpPost("query/list")]
        public MdASP12_p GetQueryList([FromBody] MdASP12_q queryParams)
        {
            return BlASP12.GetQueryList(queryParams ?? new MdASP12_q());
        }

    }
}

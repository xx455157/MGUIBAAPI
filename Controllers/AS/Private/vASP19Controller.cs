#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASP19;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASP19 固定資產狀態調整
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASP19Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASP19 BlASP19 => new BlASP19(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得查詢頁 UI 初始化資料（公司別、目前現況、新狀態）
        /// </summary>
        [HttpGet("query/uidata")]
        public MdASP19_h GetUIData()
        {
            return BlASP19.GetUIData();
        }

        /// <summary>
        /// 查詢財產清單（一次載入全部列）。
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        [HttpPost("query/list")]
        public MdASP19_p GetQueryList([FromBody] MdASP19_q queryParams)
        {
            return BlASP19.GetQueryList(queryParams ?? new MdASP19_q());
        }

        #endregion
    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTRGM09 經理報表系統 API 控制器
    /// </summary>
    [Route("htlpre/[controller]")]
    public class vHTRGM09Controller : GUIAppAuthController
    {
        // 注意：報表相關 API 已移至 ReportsController
        // 訂單詳情：GET htlpre/Reports/rooms/orderDetail/{visitNo}
    }
}

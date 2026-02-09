#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGL;

#endregion

namespace MGUIBAAPI.Controllers.GL
{
    /// <summary>
    /// 傳票資料控制器
    /// </summary>
    [Route("gl/[controller]")]
    public class VoucherController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlA24 BlA24 => new BlA24(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 檢查專案代號是否被傳票使用
        /// </summary>
        /// <param name="packageId">專案代號</param>
        /// <returns>是否存在</returns>
        [HttpGet("exists/package/{packageId}")]
        public bool IsExistsTrans(string packageId)
        {
            return BlA24.IsExist(packageId);
        }

        #endregion

    }
}

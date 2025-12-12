#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 支薪代碼資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class PayCodesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPC BlPC => new BlPC(ClientContent);
        private BlPF BlPF => new BlPF(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得支薪代碼輔助
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>支薪代碼輔助資料泛型集合物件</returns>
        [HttpGet("help")]
        public IEnumerable<MdPayCode> GetHelp([FromQuery] bool includeEmptyRow = false, [FromQuery] bool includeId = false)
        {
            return BlPC.GetHelp(includeEmptyRow, includeId);
        }

        [HttpGet("query/{id}")]
        public IEnumerable<MdPayment> GetData(string id)
		{
            return BlPF.GetData(id);
        }

        #endregion
    }
}

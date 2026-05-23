#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASQ01;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// ASQ01 固定資產購入 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASQ01Controller : GUIAppAuthController
    {

        #region " 私用屬性 "

        private BlASQ01 BlASQ01 => new BlASQ01(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 UI 初始化資料
        /// </summary>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdASQ01_h GetUIData()
        {
            return BlASQ01.GetUIData();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion

    }
}

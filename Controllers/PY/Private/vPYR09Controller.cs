#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYM03;
using GUIStd.Attributes;
using GUIStd.Models;
using AllNewGUIModels = GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewPY.Models.Private.PYR09;
using GUIStd.DAL.AllNewPY.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 基本薪資分析 資料控制器
    /// mPYM03_GetData 轉換
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYR09Controller : GUIAppAuthController
    {

        #region " 私用屬性 "

        /// <summary>
        /// 基本薪資分析 商業邏輯物件屬性
        /// </summary>
        private BlPYR09 BlPYR09 => mBlPYR09 = mBlPYR09 ?? new BlPYR09(ClientContent);
        private BlPYR09 mBlPYR09;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 UI 初始化資料
        /// </summary>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdPYR09_h GetUIData()
        {
            return BlPYR09.GetUIData();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}


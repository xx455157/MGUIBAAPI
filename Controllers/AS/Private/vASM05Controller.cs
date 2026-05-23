#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.DAL.AllNewAS.DAO;
using GUIStd.Models;
using System;
using GUIStd;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM05;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd.Attributes;
using System.Collections.Generic;
using GUIStd.DAL.AllNewAS.Models.Private.AssetSale;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM05 固定資產出售 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM05Controller : GUIAppAuthController
    {

        #region " 私用屬性 "

        private BlASM05 BlASM05 => new BlASM05(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 UI 初始化資料（查詢頁面用：公司別、大部門、部門、資產科目下拉選單）
        /// </summary>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdASM05_h GetUIData()
        {
            return BlASM05.GetUIData();
        }


        /// <summary>
        /// 取得明細頁面 UI 輔助資料（公司別下拉選單）
        /// </summary>
        /// <returns>明細頁面 UI 輔助資料（僅含 companies 欄位）</returns>
        [HttpGet("query/uidata/detail/{companyId}")]
        public MdASM05_dh GetDetailUIData(string companyId)
        {
            return BlASM05.GetDetailUIData(companyId);
        }

        #endregion

    }
}

#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.DAL.AllNewAS.Models.Private.vASM12;
using GUIStd.BLL.AllNewAS.Private;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR02固定資產清單統程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM12Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM12 BlASM12 => new BlASM12(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("page")]
        public MdASM12_h GetUIData()
        {
            return BlASM12.GetUIData();
        }
        /// <summary>
        /// 取得查詢資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("getdata/{company}")]
        public IEnumerable<MdASM12> GetData(string company)
        {
            return BlASM12.GetData(company);
        }
        /// <summary>
        /// 查詢複製頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("pagec")]
        public MdASM12c_h GetUIData_c()
        {
            return BlASM12.GetUIData_c();
        }


        #endregion

    }
}

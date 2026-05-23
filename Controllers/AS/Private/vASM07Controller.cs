#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM07;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM05 固定資產資本化 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM07Controller : GUIAppAuthController
    {

        #region " 私用屬性 "

        private BlASM07 BlASM07 => new BlASM07(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 UI 初始化資料（查詢頁面用：公司別、大部門、部門、資產科目下拉選單）
        /// </summary>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdASM07_h GetUIData()
        {
            return BlASM07.GetUIData();
        }


        /// <summary>
        /// 取得明細頁面 UI 輔助資料（公司別下拉選單）
        /// </summary>
        /// <returns>明細頁面 UI 輔助資料（僅含 companies 欄位）</returns>
        [HttpGet("query/uidata/detail/{companyId}")]
        public MdASM07_dh GetDetailUIData(string companyId)
        {
            return BlASM07.GetDetailUIData(companyId);
        }

        #endregion

    }
}

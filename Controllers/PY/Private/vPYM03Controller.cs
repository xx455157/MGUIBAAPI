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

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 出勤資料控制器
    /// mPYM03_GetData 轉換
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYM03Controller : GUIAppAuthController
    {

        #region " 私用屬性 "

        /// <summary>
        /// 出勤資料商業邏輯物件屬性
        /// </summary>
        private BlPYM03 BlPYM03 => mBlPYM03 = mBlPYM03 ?? new BlPYM03(ClientContent);
        private BlPYM03 mBlPYM03;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 UI 初始化資料
        /// </summary>
        /// <returns>UI 初始化資料模型</returns>
        [HttpGet("query/uidata")]
        public MdPYM03UiDate GetUIData()
        {
            return BlPYM03.GetUIData();
        }

        /// <summary>
        /// 查詢出勤資料
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <param name="pageNo">頁次</param>
        /// <returns>出勤資料分頁結果</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdPYM03QueryList_p GetQueryData([FromBody] MdPYM03Query query, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlPYM03.GetQueryData(query, ControlName, pageNo);
        }

        /// <summary>
        /// 取得員工輔助資料
        /// </summary>
        /// <param name="dateS">起日</param>
        /// <param name="dateE">迄日</param>
        /// <returns>員工資料代碼模型集合物件</returns>
        [HttpGet("help/employee")]
        public IEnumerable<AllNewGUIModels.MdCode> GetHelpEmployee([FromQuery] string dateS, [FromQuery] string dateE)
        {
            return BlPYM03.GetHelpEmployee(dateS, dateE);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 儲存計薪工時
        /// </summary>
        /// <param name="saveData">存檔資料</param>
        /// <returns>存檔結果</returns>
        [HttpPost("save/workhours")]
        public MdApiMessage SaveWorkHours([FromBody] MdPYM03SaveWorkHour saveData)
        {
            try
            {
                int result = BlPYM03.SaveWorkHours(saveData);
                return HttpContext.Response.UpdateSuccess(result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex); 
            }
        }

        #endregion
    }
}


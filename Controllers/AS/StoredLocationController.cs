#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM26;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 存放位置設定控制器
    /// </summary>
    [Route("as/[controller]")]
    public class StoredLocationController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlStoredLocation BlStoredLocation => new BlStoredLocation(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得資產存放位置
        /// </summary>
        /// <param name="pageNo">頁次</param>
        /// <returns></returns>
        [HttpGet("getdata/pages/{pageNo}")]
        public MdCode_p GetData([DARange(1, int.MaxValue)] int pageNo)
        {
            return BlStoredLocation.GetData(funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 判斷資產存放位置是否存在
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpGet("isexits/{AQ01}")]
        public bool isExists(string AQ01)
        {
            bool _result = BlStoredLocation.IsExists(AQ01);

            return _result;
        }

        /// <summary>
        /// 判斷資產存放位置是否存在AB
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpGet("isexitsByAB/{AQ01}")]
        public bool isExistsByAB(string AQ01)
        {
            bool _result = BlStoredLocation.IsExistsByAB(AQ01);

            return _result;
        }

        #endregion

        #region " 異動資料 "

        /// <summary>
        /// 刪除資產存放位置
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpDelete("delete/{AQ01}")]
        public MdApiMessage Delete(string AQ01)
        {
            try
            {
                int _result = BlStoredLocation.Delete(AQ01);
                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增資產存放位置
        /// </summary>
        /// <returns></returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdASM26_q obj)
        {
            try
            {
                int _result = BlStoredLocation.Insert(obj);

                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新資產存放位置
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public MdApiMessage Update([FromBody] MdASM26_q obj)
        {
            try
            {
                int _result = BlStoredLocation.Update(obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}

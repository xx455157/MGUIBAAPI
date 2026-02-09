#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.Models;
using GUIStd.BLL.AllNewAS;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.AS.NumberRules
{
	/// <summary>
	/// 【需經驗證】系統設定資料控制器
	/// </summary>
	[Route("as/[controller]")]
	public class NumberRulesController : GUIAppController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlAF BlAF => new BlAF(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        #region " 單據編號設定 (AF) "

        /// <summary>
        /// 取得單據編號設定
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <returns>單據編號原則設定模型泛型集合物件</returns>
        [HttpGet("getData")]
        public IEnumerable<MdTXNumberRule> GetData([FromQuery] string companyId = "")
        {
            return BlAF.GetData(AF01: companyId);
        }

        /// <summary>
        /// 判斷是否已存在
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="prefix">字軌</param>
        /// <param name="TXType">異動別</param>
        /// <returns></returns>
        [HttpGet("isExists")]
        public bool IsExist([FromQuery] string companyId="", [FromQuery] string prefix="", [FromQuery] string TXType="")
        {
            return BlAF.IsExist(companyId, prefix, TXType);
        }

        #endregion

        #endregion

        #region " 共用函式 -  異動資料 "

        #region " 單據編號設定 (AF)"

        /// <summary> 
        /// 新增資料
        /// </summary>
        /// <param name="obj">單據編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdTXNumberRule_i obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlAF.Insert(obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>	        
        /// <param name="companyId">公司別</param>
        /// <param name="prefix">字軌</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("delete/{companyId}/{prefix}")]
        public MdApiMessage Delete( string companyId, string prefix)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlAF.Delete(companyId, prefix);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>		        
        /// <param name="obj">單據編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("update")]
        public MdApiMessage Update([FromBody] MdTXNumberRule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = 0;

                _result = BlAF.Update(
                    Common.Cvr2String(obj.AF01o),
                    Common.Cvr2String(obj.AF02o),
                    Common.Cvr2String(obj.AF03),
                    Common.Cvr2String(obj.AF04),
                    Common.Cvr2String(obj.AF05)
                );

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }
        #endregion""

        #endregion ""


    }
}

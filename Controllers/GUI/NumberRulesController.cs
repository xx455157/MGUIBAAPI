#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUIStd.BLL.AllNewGUI;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.GUI.NumberRules
{
	/// <summary>
	/// 【需經驗證】系統設定資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class NumberRulesController : GUIAppController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA03 BlA03 => new BlA03(ClientContent);
        private BlB01 BlB01 => new BlB01(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        #region " 傳票編號設定 (A03) "
        /// <summary>
        /// 取得傳票編號設定
        /// </summary>
        /// <param name="A0301"></param>
        /// <param name="A0303">公司別(沒有輸入就視為沒有條件)</param>
        /// <returns>傳票編號原則設定模型泛型集合物件</returns>
        [HttpGet("getData")]
        public IEnumerable<MdNumberRule> GetData([FromQuery] string A0301 = "1", [FromQuery] string A0303 = "")
        {
            return BlA03.GetData(A0301: A0301, A0303: A0303);
        }

        /// <summary>
        /// 判斷是否已存在
        /// </summary>
        /// <param name="type">傳票種類</param>
        /// <param name="companyId">公司別</param>
        /// <returns></returns>
        [HttpGet("isexists/{type}/{companyId}")]
        public bool IsExist(string type, string companyId)
        {
            return BlA03.IsExist(type, companyId);
        }

        #endregion""

        #region " 單據編號設定 (B01) "

        /// <summary>
        /// 取得單據編號設定
        /// </summary>
        /// <param name="B0101">公司別</param>
        /// <returns>單據編號原則設定模型泛型集合物件</returns>
        [HttpGet("getTXData")]
        public IEnumerable<MdTXNumberRule> GetTXData([FromQuery] string B0101 = "")
        {
            return BlB01.GetData(B0101: B0101);
        }

        /// <summary>
        /// 判斷是否已存在
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="prefix">字軌</param>
        /// <param name="TXType">異動別</param>
        /// <returns></returns>
        [HttpGet("isExistsTX")]
        public bool IsExistTX([FromQuery] string companyId="", [FromQuery] string prefix="", [FromQuery] string TXType="")
        {
            return BlB01.IsExist(companyId, prefix, TXType);
        }

        #endregion

        #endregion

        #region " 共用函式 -  異動資料 "

        #region " 傳票編號設定 (A03) "

        /// <summary> 
        /// 新增資料
        /// </summary>
        /// <param name="obj">傳票編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdNumberRule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlA03.Insert(obj);
                //var _mdReqResult = new MdApiMessage();
                //_mdReqResult.Result = true;
                // 回應前端修改成功訊息
                return HttpContext.Response.InsertSuccess(_result);
                //return _mdReqResult;
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
        /// <param name="type">傳票種類</param>		        
        /// <param name="companyId">公司別</param>
        /// <param name="rule">編號原則</param>
        /// <param name="description">類別說明</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{type}/{companyId}/{rule}/{description}")]
        public MdApiMessage Delete(string type, string companyId, string rule, string description)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlA03.Delete(type, companyId, rule, description);

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
        /// <param name="obj">傳票編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdNumberRule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlA03.Delete(
                    Common.Cvr2String(obj.A0302o),
                    Common.Cvr2String(obj.A0303o),
                    Common.Cvr2String(obj.A0304o),
                    Common.Cvr2String(obj.A0305o));

                _result = BlA03.Insert(obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion ""

        #region " 單據編號設定 (B01)"

        /// <summary> 
        /// 新增資料
        /// </summary>
        /// <param name="obj">單據編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("InsertTX")]
        public MdApiMessage Insert([FromBody] MdTXNumberRule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlB01.Insert(obj);

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
        [HttpDelete("deleteTX/{companyId}/{prefix}")]
        public MdApiMessage DeleteTX( string companyId, string prefix)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlB01.Delete(companyId, prefix);

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
        /// <param name="obj">傳票編號原則模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("updateTX")]
        public MdApiMessage UpdateTX([FromBody] MdTXNumberRule obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = 0;

                _result = BlB01.Update(
                    Common.Cvr2String( obj.B0101o),
                    Common.Cvr2String(obj.B0102o),
                    Common.Cvr2String(obj.B0103),
                    Common.Cvr2String(obj.B0104),
                    Common.Cvr2String(obj.B0105)
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

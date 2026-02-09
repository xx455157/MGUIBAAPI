#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASM11;
using GUIStd.Models;
using GUICore.Web.Extensions;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 固定資產類別控制器
    /// </summary>
    [Route("as/[controller]")]
    public class CategoryController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlCategory BlCategory => new BlCategory(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得固定資產類別下拉選單
        /// </summary>
        /// <param name="queryText">財產編號或名稱必需包含傳入的參數值</param>
        /// <param name="pageNo">查詢頁次</param>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] bool FuzzySearch)
        {
            return BlCategory.GetHelp(compId, queryText, FuzzySearch, ControlName, pageNo);
        }

        /// <summary>
        /// 取得固定資產類別清單資料
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="pageNo">查詢頁次</param>
        [HttpPost("getdata/{pageNo}")]
        public MdASM11_p GetData([FromBody] MdASM11_q queryParams, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlCategory.GetData(queryParams, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 刪除固定資產類別
        /// </summary>
        /// <param name="AE01"></param>
        /// <param name="AE02"></param>
        [HttpDelete("delete/{AE01}/{AE02}")]
        public MdApiMessage Delete(string AE01, string AE02)
        {
            try
            {
                int _result = BlCategory.Delete(AE01, AE02); ;
                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增固定資產類別
        /// </summary>
        /// <param name="queryParams"></param>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdASM11d_q obj)
        {
            try
            {
                int _result = BlCategory.Insert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新固定資產類別
        /// </summary>
        /// <param name="queryParams"></param>
        [HttpPost("update")]
        public MdApiMessage Update([FromBody] MdASM11d_q obj)
        {
            try
            {
                int _result = BlCategory.Update(obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("ValidateCopyData")]
        public IActionResult ValidateCopyData([FromBody] MdASM11c_q queryParams)
        {
            try
            {
                Dictionary<string, List<string>> errors = BlCategory.ValidateCopyData(queryParams);

                Dictionary<string, object> messageDetail = new Dictionary<string, object>();
                if (errors["idDuplicate"].Count==0 && errors["nameDuplicate"].Count == 0)
                {
                    messageDetail["execResult"] = true;
                    messageDetail["errorMsg"] = null;
                }
                else
                {
                    messageDetail["execResult"] = false;
                    messageDetail["errorMsg"] = errors;
                }

                return Ok(new { result = true, message = messageDetail });
            }
            catch (Exception ex)
            {
                return BadRequest(new { result = false, message = ex.ToString() });
            }
        }

        /// <summary>
        /// 複製固定資產類別至指定公司
        /// </summary>
        /// <param name="queryParams"></param>
        [HttpPost("copy")]
        public IActionResult Copy([FromBody] MdASM11c_q queryParams)
        {
            try 
            { 
                Dictionary<string, List<string>>  _result = BlCategory.Copy(queryParams);
                Dictionary<string, object> messageDetail = new Dictionary<string, object>();
                if (_result == null)
                {
                    messageDetail["execResult"] = true;
                    messageDetail["errorMsg"] = null;
                }
                else
                {
                    
                    if (_result["idDuplicate"].Count == 0 && _result["nameDuplicate"].Count == 0)
                    {
                        messageDetail["execResult"] = true;
                        messageDetail["errorMsg"] = null;
                    }
                    else
                    {
                        messageDetail["execResult"] = false;
                        messageDetail["errorMsg"] = _result;
                    }
                }
                return Ok(new { result = true, message = messageDetail });
            }
            catch (Exception ex)
            {
                return BadRequest(new { result = false, message = ex.ToString() });
            }
        }

        /// <summary>
        /// 固定資產類別ID是否存在
        /// </summary>
        /// <param name="AE01"></param>
        /// <param name="AE02"></param>
        [HttpGet("isidexits/{AE01}/{AE02}")]
        public bool isExists(string AE01, string AE02)
        {
            return BlCategory.isExists(AE01, AE02);
        }

        /// <summary>
        /// 固定資產類別名稱是否存在
        /// </summary>
        /// <param name="AE01"></param>
        /// <param name="AE03"></param>
        [HttpGet("isnameexits/{AE01}/{AE03}")]
        public bool isExistByAE01AE03(string AE01, string AE03)
        {
            return BlCategory.IsExistByAE01AE03(AE01, AE03);
        }

        /// <summary>
        /// 取得指定固定資產類別資料
        /// </summary>
        /// <param name="AE01"></param>
        /// <param name="AE02"></param>
        [HttpGet("getrow/{AE01}/{AE02}")]
        public MdCategory GetRow(string AE01, string AE02)
        {
            return BlCategory.GetRow(AE01, AE02);
        }
        #endregion
    }
}

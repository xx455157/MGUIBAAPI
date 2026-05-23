#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.GUI;
using GUIStd.BLL.GUI.Private;
using GUIStd.BLL.AllNewGUI;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.EForm;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
	/// <summary>
	/// 【需經驗證】公告資料資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class EformController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlA79 BlA79 => new BlA79(ClientContent);
		private BlA89 BlA89 => new BlA89(ClientContent);
		private BlEF03 BlEF03 => new BlEF03(ClientContent);
		private BlEform BlEform => new BlEform(ClientContent);
		private BlPetition BlPetition => new BlPetition(ClientContent);
		private BlMail BlMail => new BlMail(ClientContent);

		#endregion

		#region " 共用函式 -  查詢資料 "

		/// <summary>
		/// 取得可填寫/查詢之電子表單,依類別分類之模型集合物件
		/// </summary>
		/// <returns>可用電子表單依類別分類之模型集合物件</returns>
		[HttpGet("auth")]
		public IEnumerable<MdFormType> GetAuthEForms()
		{
			return BlEform.GetEFormsAuthQuery();
		}

		/// <summary>
		/// 取得電子表單相關設定
		/// </summary>
		/// <param name="compId">公司別</param>
		/// <param name="formCode">電子表單代碼</param>
		/// <returns>電子表單物件模型</returns>
		[HttpGet("content/{compId}/{formCode}")]
		public MdMain GetData(string compId, string formCode)
		{
			return BlEform.GetRow(compId, formCode);
		}

		/// <summary>
		/// 取得指定電子表單內容(檢視需要,需取得題目及流程)
		/// </summary>
		/// <param name="compId">公司別</param>
		/// <param name="formCode">電子表單代碼</param>
		/// <param name="key">電子表單PKey</param>
		/// <returns>電子表單物件模型</returns>
		[HttpGet("review/{compId}/{formCode}/{key}")]
		public MdReview_r GetRow(string compId, string formCode, decimal key)
		{
			return BlEform.GetRow(compId, formCode, key);
		}

		/// <summary>
		/// 取得電子表單題目的圖片物件
		/// </summary>
		/// <param name="compId">公司別</param>
		/// <param name="formCode">電子表單代碼</param>
		/// <param name="objs">電子表單題目的附件物件模型</param>
		/// <returns>電子表單題目的圖片物件模型</returns>
		[HttpPost("images/{compId}/{formCode}")]
		public IEnumerable<MdOptFile> GetOptionFiles(string compId, string formCode,[FromBody] IEnumerable<MdOptFile> objs)
		{
			return BlEform.GetFiles(compId, formCode, objs);
		}

		/// <summary>
		/// 下載檔案
		/// </summary>
		/// <param name="compId">公司別</param>
		/// <param name="formCode">電子表單代碼</param>
		/// <param name="code">電子表單題目選項代碼</param>
		/// <param name="index">電子表單題選項目子序號</param>
		/// <returns>檔案的資料流</returns>
		[HttpGet("download/{compId}/{formCode}/{code}/{index}")]
		public async Task<IActionResult> GetFile(string compId, string formCode, string code, int index)
		{
			byte[] _fileContent = null; string _fileName = null;

			try
			{
				await Task.Run(() => BlEF03.GetRow(compId, formCode, code, index, out _fileContent, out _fileName));

				// 找不到附檔時，回傳錯誤訊息
				if (_fileContent == null) return BadRequest(HttpContext.Response.SendFileNotExistFailed());

				// 回傳檔案
				return HttpContext.Response.SendFile(_fileContent, _fileName);
			}
			catch (Exception ex)
			{
				return BadRequest(HttpContext.Response.SendFileFailed(ex));
			}
		}

		/// <summary>
		/// 取得上傳之物件
		/// </summary>
		/// <param name="compId">公司別</param>
		/// <param name="formCode">表單代碼</param>
		/// <param name="key">簽呈PKey</param>
		/// <returns>電子表單上傳附件之模型集合物件</returns>
		[HttpGet("files/{compId}/{formCode}/{key}")]
		public IEnumerable<MdTopicFile> GetUploadFiles(string compId, string formCode, decimal key)
		{
			return BlEform.GetFiles(compId, formCode, key);
		}

		/// <summary>
		/// 取得電子簽核內容與其表單設定
		/// </summary>
		/// <param name="pageNo">查詢頁次</param>
		/// <param name="obj">查詢條件物件</param>
		/// <returns>電子表單物件模型</returns>
		[HttpPost("query/pages/{pageNo}")]
		public MdReview_p GetData([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdQuery obj)
		{
			return BlEform.GetData(ControlName, obj, pageNo);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增資料
		/// </summary>
		/// <param name="obj">客戶資料模型物件</param>
		/// <param name="isSend">是否為送簽, 反之為存檔)</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost()]
		public MdApiMessage Insert([FromBody] MdPetition_3 obj, [FromQuery] bool isSend)
		{
			try
			{
				decimal _A8901 = -1;
				// 呼叫商業元件執行新增作業
				int _result = BlA89.ProcessInsert(obj, isSend, out _A8901);
				if (_result > 0)
				{
					// 是否送簽(建立流程)
					if (isSend)
					{
						// 建立後續流程所需的Model
						MdPetition_2 _flowObj = new MdPetition_2()
						{
							DU01 = obj.A8939,
							A8933 = obj.A8933,
							A8907 = obj.A8907,
							A8908 = obj.A8908,
							A8909 = obj.A8909,
							A8910 = obj.A8910,
							A8906n = obj.A8908,
							A8901 = _A8901,
							A8930 = obj.A8930,
							DU06 = 0
						};
						int _flowRes = BlPetition.ProcessInsert(_flowObj);

						BlMail.SendMail(_A8901, obj.signPathUrl);

						// 回應前端新增成功訊息
						return HttpContext.Response.InsertSuccess(_flowRes, responseData: BlA89.GetRow(_A8901));
					}
					else
					{
						// 回應前端新增成功訊息
						return HttpContext.Response.InsertSuccess(_result, responseData: BlA89.GetRow(_A8901));
					}
				}
				else
				{
					// 回應前端新增/複製失敗訊息
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		/// <summary>
		/// 修改資料
		/// </summary>
		/// <param name="obj">客戶資料模型物件</param>
		/// <param name="key">異動資料之PKey</param>
		/// <param name="isSend">是否為送簽, 反之為存檔)</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{key}")]
		public MdApiMessage Update([FromBody] MdPetition_3 obj,decimal key, [FromQuery] bool isSend)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlA89.ProcessUpdate(obj, key,isSend);
				if (_result > 0)
				{
					// 是否送簽(建立流程)
					if (isSend)
					{
						// 建立後續流程所需的Model
						MdPetition_2 _flowObj = new MdPetition_2()
						{
							DU01 = obj.A8939,
							A8933 = obj.A8933,
							A8907 = obj.A8907,
							A8908 = obj.A8908,
							A8909 = obj.A8909,
							A8910 = obj.A8910,
							A8906n = obj.A8908,
							A8901 = key,
							A8930 = obj.A8930,
							DU06 = 0
						};
						int _flowRes = BlPetition.ProcessInsert(_flowObj);
						_result += _flowRes;

						BlMail.SendMail(key, obj.signPathUrl);
					}

					// 回應前端修改成功訊息
					return HttpContext.Response.UpdateSuccess(_result, responseData: BlA89.GetRow(key));
				}
				else
				{
					// 回應前端修改失敗訊息
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

		/// <summary>
		/// 修改資料(撤簽)
		/// </summary>
		/// <param name="key">異動資料之PKey</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("recycle/{key}")]
		public MdApiMessage Update(decimal key)
		{
			try
			{
				// 呼叫商業元件執行"撤簽"
				int _result = BlEform.ProcessCxlApproved(key);

				// 回應前端修改成功訊息
				return HttpContext.Response.UpdateSuccess(_result, responseData: BlA89.GetRow(key));
			}
			catch (Exception ex)
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

		/// <summary>
		/// 電子表單催簽
		/// </summary>
		/// <param name="srcObj">來源查詢物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("pushSign")]
		public MdApiMessage PushSign(MdAttachment srcObj)
		{
			try
			{
				var _D501s = BlEform.ProcessPushSign(srcObj);
				if (_D501s != null && _D501s.Any() && BlA79.IsExist02(srcObj))
				{
					List<MdAttachment> _targets = new List<MdAttachment> { };
					foreach (var _D501 in _D501s)
					{
						_targets.Add(new MdAttachment
						{
							A7903 = "mBOF08",
							A79101 = Common.Cvr2String(_D501),
							A79102 = "D5"
						});
					}
					BlA79.CloneData(srcObj, _targets);
				}
				return HttpContext.Response.SendSuccess(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_UrgentSignSuccess"));
			}
			catch (Exception ex)
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.SendFailed(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_UrgentSignFail"), ex);
			}
		}

		/// <summary>
		/// 刪除資料
		/// </summary>
		/// <param name="key">異動資料之PKey</param>
		/// <param name="programId">程式名稱</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpDelete("{key}/{programId}")]
		public MdApiMessage Delete(decimal key, string programId)
		{
			try
			{
				// 呼叫商業元件執行刪除作業
				int _result = BlA89.ProcessDelete(key);

				// 附件刪除作業
				var _obj = new MdAttachment { A7903 = programId, A79101 = Common.Cvr2String(key), A79102 = "A89" };
				_result += BlA79.ProcessDelete02(_obj);

				// 回應前端刪除成功訊息
				return HttpContext.Response.DeleteSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端刪除失敗訊息
				return HttpContext.Response.DeleteFailed(ex);
			}
		}

		#endregion
	}
}

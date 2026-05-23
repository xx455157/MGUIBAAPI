#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web;
using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUICore.Web.Models;
using GUIStd;
using GUIStd.Security;
using MGUIBAAPI.Features;

#endregion

namespace MGUIBAAPI.Controllers.Test
{
    /// <summary>
    /// 服務測試控制器
    /// </summary>
    [Route("test/[controller]")]
	[ApiController]
	public class ReadyController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "

        /// <summary>
        /// 建構子
        /// </summary>
        public ReadyController()
		{
            // 改變執行服務的使用者帳號
            this.WSUser = "ReadyAPI";
		}

		#endregion

		#region " 匿名存取 API "

		/// <summary>
		/// 格式化目前時間、國際標準時間、Log全名
		/// </summary>
		/// <returns>字串陣列</returns>
		[AllowAnonymous]
        [HttpGet("values")]
        public IEnumerable<string> Get()
        {
            Logging.Logger.LogDebug("Call test/ready/values");

            return new string[] {
                Common.FormatedCurrentTime,
                Common.FormatedUTCCurrentTime,
                Logging.Settings.PathFormat
            };
        }

        /// <summary>
        /// 取得各語系資源
        /// </summary>
        /// <param name="key">資源名稱 ex. PanelDescpt_Code</param>
        /// <returns>字串陣列</returns>
        [AllowAnonymous]
        [HttpGet("resources/{key}")]
        public IEnumerable<string> GetResource(string key)
        {
            // 查看 ClientContent
            //var _clientContent = this.ClientContent;

            return new string[] {
                Localization.GetValue(Enums.ResourceLang.Lang, key),
                Localization.GetValue(Enums.ResourceCulture.EN, Enums.ResourceLang.Lang, key),
                Localization.GetValue(Enums.ResourceCulture.CN, Enums.ResourceLang.Lang, key),
                Localization.GetValue(Enums.ResourceCulture.TW, Enums.ResourceLang.Lang, key)
            };
        }

        /// <summary>
        /// 回傳 Base64String 格式的網站圖片
        /// </summary>
        /// <param name="programId">程式代號</param>
        /// <param name="fileName">圖片檔名</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("uriimagetobase64string")]
        public string GetUriImagetoBase64String(string programId, string fileName)
		{
            return Common.ImageToBase64String(Common.GetUriImage(
                $"{Utils.AppSettings.ImageReadPath}/{programId}/{fileName}"));
		}

        /// <summary>
        /// 【僅供測試區】 下載檔案，使用路徑參數
        /// </summary>
        /// <param name="fileName">檔案名稱</param>
        /// <returns>檔案的資料流</returns>
        [AllowAnonymous]
        [HttpGet("downloadfile/{fileName}")]
        public async Task<IActionResult>GetFile(string fileName)
        {
            var _srcFileName = Path.Combine("Content/Files/Export", fileName);

            // 決定前端下載使用的檔名
            var _clientFileName = 
            	$"{Path.GetFileNameWithoutExtension(_srcFileName)}_{Common.CurrentDateTime}{Path.GetExtension(_srcFileName)}";

            try
            {
                // 讀取實體檔案，回傳檔案的 Memory Stream
                var _stream = await Common.ReadFileToStreamAsync(_srcFileName);

                // 回傳檔案
                return HttpContext.Response.SendFile(_stream, _clientFileName);
            }
            catch (Exception ex)
            {
                return BadRequest(HttpContext.Response.SendFileFailed(ex));
            }
        }

        /// <summary>
        /// 【僅供測試區】 下載檔案，使用查詢參數
        /// </summary>
        /// <param name="fileName">檔案名稱</param>
        /// <param name="clientFileName">下載使用的檔名</param>
        /// <returns>檔案的資料流</returns>
        [AllowAnonymous]
        [HttpGet("downloadfile")]
        public async Task<IActionResult>GetFile([RequiredFromQuery] string fileName, [FromQuery] string clientFileName)
        {
            var _srcFileName = Path.Combine("Content/Files/Export", fileName);

            // 決定使用前端或後端檔名
            if (string.IsNullOrWhiteSpace(clientFileName))
            {
                clientFileName =
                    $"{Path.GetFileNameWithoutExtension(_srcFileName)}_{Common.CurrentDateTime}{Path.GetExtension(_srcFileName)}";
            }

            try
            {
                // 讀取實體檔案，回傳檔案的 Memory Stream
                var _stream = await Common.ReadFileToStreamAsync(_srcFileName);

                // 回傳檔案
                return HttpContext.Response.SendFile(_stream, clientFileName);
            }
            catch (Exception ex)
            {
                return BadRequest(HttpContext.Response.SendFileFailed(ex));
            }
        }

        #endregion

        #region " 授權存取 API "

        /// <summary>
        /// 【需經驗證】取得資料庫連線字串
        /// </summary>
        /// <returns>字串</returns>
        [HttpGet("dbconnectstring")]
        public string GetConnectionString()
        {
            // 查看 ClientContent
            //var _clientContent = this.ClientContent;

            return string.Format("DBPos = 0，Find RelatedDB.DBName='{0}'，DBPath = {1}",
                Enums.RelatedDB.AllNewGUI,
                Config.FindRelatedDBPath(Enums.RelatedDB.AllNewGUI)
            );
        }

		/// <summary>
		/// 【需經驗證】資料加密
		/// </summary>
		/// <returns>字串</returns>
		[HttpGet("encrypt/{text}")]
        public object EncryptText(string text)
        {
            var _file = Utils.AppSettings.KeyFile;
            var _rijndael = new RijndaelEncrypt(_file);
            var _str = _rijndael.Encrypt(text);
            return new
            {
                Text = text,
                EncryptText = _str
            };
        }

        /// <summary>
        /// 【需經驗證】產生指定長度的服務隨機授權碼
        /// </summary>
        /// <param name="numChars">指定授權碼長度</param>
        /// <returns>JSON 物件</returns>
        [HttpGet("genservicekey/{numChars}")]
        public object GenServiceAuthKey(int numChars)
        {
            var _rng = new RNGCrypto();
            var _str = _rng.GetRandomFixedChars(numChars);
            return new
            {
                NumChars = numChars,
                ServiceAuthKeyText = _str
            };
        }

		/// <summary>
		/// 【需經驗證】基於 Base64 加密文字
		/// </summary>
		/// <returns>JSON 物件</returns>
		[HttpPost("base64encode")]
		public async Task<object> Base64EnCodeAsync()
		{
			var _text = await Request.GetRawBodyStringAsync();
			return new
			{
				Text = _text,
				EncodeText = Base64EncoderHelper.Encode(_text)
			};
		}

        #region " 將上傳圖檔轉為 Base64 字串格式並回傳 "

        /// <summary>
        /// 【需經驗證】將上傳圖檔轉為 Base64 字串格式回傳
        /// </summary>
        /// <returns>JSON 物件</returns>
        [HttpPost("base64images")]
		public List<string> ImagesToBase64String([FromForm] MdImagesForm imagesForm)
		{
            var _files = imagesForm.ImageFiles;
            var _images = new List<string>();

            if (_files != null)
            {
                foreach (var _file in _files)
                {
                    var _reader = new BinaryReader(_file.OpenReadStream());
                    byte[] _imgBytes = _reader.ReadBytes((int)_file.Length);
                    _images.Add(Common.ImageToBase64String(
                        _imgBytes, Common.GetImageFormat(_file.FileName)));
                }
            }

			return _images;
		}

        /// <summary>
        /// 【需經驗證】使用系統共用模型物件接收上傳圖檔，並將圖檔轉為 Base64 字串格式回傳
        /// </summary>
        /// <returns>JSON 物件</returns>
        [HttpPost("base64imagesfrombytes")]
        public async Task<List<string>> ImagesToBytes([FromForm] MdFormData imagesForm)
        {
			var _images = new List<string>();

			if (imagesForm.FormFiles != null)
            {
                var _files = await WebFunc.ConvertFormFileToBase64Image(imagesForm);

                foreach (var _file in _files)
                {
                    _images.Add(Common.ImageToBase64String(
                        _file.Data, Common.GetImageFormat(_file.Name)));
                }
            }

            return _images;
        }

        /// <summary>
        /// 【需經驗證】使用自訂模型物件接收上傳圖檔，並將圖檔轉為 Base64 字串格式回傳
        /// </summary>
        /// <returns>JSON 物件</returns>
        [HttpPost("base64imagesfrombytes2")]
        public async Task<List<string>> ImagesToBytes2([FromForm] MdImagesForm imagesForm)
        {
            var _images = new List<string>();

            // 上傳一個檔案
            if (imagesForm.ImageFile != null)
            {
                // 由本文傳入的自訂模型物件提取所需的單檔上傳資料，放入系統共用模型物件的 FormFile 屬性中
                var _formFile = new MdFormData()
                {
                    FormFile = imagesForm.ImageFile
                };

                // 將單一圖檔轉為 Base64String 格式
                var _file = await WebFunc.IFormFile2Base64Image(imagesForm.ImageFile);

				//// 取得圖片的 Byte Array
				//var _imageBytes = _file.Data;

				// 將上傳圖片轉為 Base64String 格式後，暫存至準備回傳前端的清單物件中
				_images.Add(Common.ImageToBase64String(_file));
            }

            // 上傳多個檔案
            if (imagesForm.ImageFiles != null)
			{
				// 由本文傳入的自訂模型物件提取所需的多檔上傳資料，放入系統共用模型物件的 FormFiles 屬性中
				var _formFiles = new MdFormData()
				{
					FormFiles = imagesForm.ImageFiles
				};

				// 將所有圖檔轉為 Base64String 格式
				var _files = await WebFunc.ConvertFormFileToBase64Image(_formFiles);

				foreach (var _file in _files)
				{
					// 取得圖片的 Byte Array
					//var _imageBytes = _file.Data;

					// 將上傳圖片轉為 Base64String 格式後，暫存至準備回傳前端的清單物件中
					_images.Add(Common.ImageToBase64String(_file));
				}
			}

			return _images;
        }

        #endregion

        #endregion

        #region " 上傳圖片的模型類別 "

        /// <summary>
        /// 表單上傳圖片檔案模型類別
        /// </summary>
        public class MdImagesForm
        {
            /// <summary>
            /// 檔案物件
            /// </summary>
            public IFormFile ImageFile { get; set; }

            /// <summary>
            /// 檔案泛型集合物件
            /// </summary>
            public List<IFormFile> ImageFiles { get; set; }
        }

        #endregion
    }
}

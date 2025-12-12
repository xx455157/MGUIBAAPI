#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.BLL.AllNewHTL;
using GUIStd.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUICore.Web.Extensions;
using GUICore.Web.Controllers;
using GUICore.Web.Filters;
using GUICore.Web.Models;
using GUICore.Web.Attributes;
using GUICore.Web;
using MGUIBAAPI.Features;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
    /// <summary>
    /// 敦謙自助報到機 - 檔案管理控制器
    /// 處理旅客證件掃描、OCR 辨識資料及圖檔上傳功能
    /// </summary>
    [Route("general/DonQianKiosk/[controller]")]
    public class FilesController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化檔案控制器
        /// </summary>
        public FilesController()
        {
            // 改變執行服務的使用者帳號為 KIOSK
            this.WSUser = "KIOSK";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlKiosk BlKiosk => new BlKiosk(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        // 此控制器無查詢功能

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 上傳旅客證件掃描資料及 OCR 辨識結果
        /// 接收證件圖檔並將 OCR 辨識文字資料儲存至系統
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="booking_number">訂單號碼或住宿碼</param>
        /// <param name="obj">OCR 掃描資料物件，包含證件圖片及辨識文字</param>
        /// <returns>成功時回傳系統規範訊息物件，失敗時回傳 null</returns>
        /// <remarks>
        /// 處理流程：
        /// 1. 接收 Form 表單上傳的證件圖檔
        /// 2. 將圖檔轉換為 Base64 格式
        /// 3. 將 OCR 辨識文字資料與圖檔一併儲存
        /// 注意：發生例外時回傳 null
        /// </remarks>
        [HttpPost()]
        public async Task<MdApiMessage> UploadOCRData([FromQuery] string domain, [FromQuery] string booking_number, [FromForm] MdKioskOCR obj)
        {
            try
            {
                var _images = new List<string>();

                // 步驟 1：由本文傳入的自訂模型物件提取所需的檔案資料
                // 將上傳的檔案放入系統共用模型物件的 FormFiles 屬性中
                var _formFiles = new MdFormData()
                {
                    FormFiles = obj.image
                };

                // 步驟 2：將所有圖檔轉換為 Base64 編碼格式
                var _files = await WebFunc.ConvertFormFileToBase64Image(_formFiles);

                // 步驟 3：呼叫商業邏輯層儲存 OCR 檔案資料
                int _result = BlKiosk.SaveOCRFiles(booking_number, obj.OCR, _files.ToArray()[0]);

                // 回應前端上傳成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch
            {
                // 發生例外時回應前端 null（依對方需求）
                return null;
            }
        }

        #endregion

        #region " 內部資料模型 "

        /// <summary>
        /// KIOSK OCR 掃描資料模型
        /// 用於接收自助報到機上傳的證件掃描圖片及 OCR 辨識結果
        /// </summary>
        public class MdKioskOCR
        {
            /// <summary>
            /// 證件掃描圖片檔案集合
            /// 可接受多個圖檔上傳（如身分證正反面）
            /// </summary>
            public List<IFormFile> image { get; set; }

            /// <summary>
            /// OCR 辨識文字資料
            /// 包含從證件上辨識出的文字資訊（如姓名、身分證字號等）
            /// </summary>
            public string OCR { get; set; }
        }

        #endregion
    }
}

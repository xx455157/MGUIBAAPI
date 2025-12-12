#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.Attributes;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Models.GUI
{
    /// <summary>
	/// 控制器下的附件檔案新增模型衍生類別，繼承自 MdAttachment 模型
	/// 需加入 Order 屬性，讓此衍生類別的屬性成員輸出的排列順序低於基底類別
    /// </summary>
    public class CmAttachments_i : MdAttachment
    {
        #region " 共用屬性 "

        /// <summary>
        /// 刪除檔案檔名清單
        /// </summary>
        public string[] DelFiles { get; set; }

        /// <summary>
        /// 附件檔案模型物件
        /// </summary>
        [DADisplayName("PanelDescpt_Attachment3")]
        [JsonProperty(Order = 400)]
        public IEnumerable<IFormFile> FormFiles { get; set; }

        #endregion
    }
}

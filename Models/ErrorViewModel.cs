namespace MGUIBAAPI.Models
{
    /// <summary>
    /// 頁面檢視失敗模型類別
    /// </summary>
    public class ErrorViewModel
    {
        #region " 共用屬性 "

        /// <summary>
        /// Request Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 是否顯示 Request Id
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion
    }
}
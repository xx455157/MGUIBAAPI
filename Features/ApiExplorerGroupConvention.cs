#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

#endregion

namespace MGUIBAAPI.Features
{
    /// <summary>
    /// 自訂 API 文件版號原則的類別
    /// </summary>
    public class ApiExplorerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // 版號處理忽略有指定 SwaggerIgnore Attribute 的控制器
            var _ignoreAttr = controller.Attributes.FirstOrDefault(
                attr => attr.GetType().Name.ToLower() == "SwaggerIgnoreAttribute".ToLower());
            if (_ignoreAttr != null) return;

            // 以 Controller 資料夾下的第一層資料夾名稱當做分群版號
            var _namespace = controller.ControllerType.Namespace.Split('.');
            controller.ApiExplorer.GroupName = _namespace[2];
        }
    }
}


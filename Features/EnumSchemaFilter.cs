#region " 匯入的名稱空間：Framework "

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;

#endregion

namespace MGUIBAAPI.Features
{
    /// <summary>
    /// 自訂 Swagger UI 列舉下拉內容的篩選條件類別
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        private readonly List<XPathNavigator> _xmlNavigators = new List<XPathNavigator>();

        /// <summary>
        /// 建構子
        /// </summary>
        public EnumSchemaFilter()
        {
            // 建立所有 XML 文件的巡覽器
            foreach (var _file in Utils.GUIAssemblyXMLFiles)
            {
                if (File.Exists(_file))
                {
                    var _xmlDoc = new XPathDocument(_file);
                    _xmlNavigators.Add(_xmlDoc.CreateNavigator());
                }
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum) return;

            schema.Enum.Clear();

            // 預先建立描述字典
            Dictionary<string, string> _descriptionDict = new Dictionary<string, string>();
            foreach (var _field in context.Type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (_field.Name == "value__") continue;

                var _memberName = $"F:{context.Type.FullName.Replace("+", ".")}.{_field.Name}";

                // 從所有 XML 檔案中搜尋註解
                var _node = _xmlNavigators
                    .Select(nav => nav.SelectSingleNode($"/doc/members/member[@name='{_memberName}']/summary"))
                    .FirstOrDefault(n => n != null);

                var _summary = _node?.InnerXml.Trim() ?? string.Empty;
                var _value = (int)Enum.Parse(context.Type, _field.Name);

                // 保存註解
                _descriptionDict[_field.Name] = _summary;
            }

            // 將原下拉僅可顯示列舉值，改為顯示【列舉值 列舉名稱 - Summary說明】
            foreach (var value in Enum.GetValues(context.Type).Cast<int>())
            {
                var _name = Enum.GetName(context.Type, value);

                if (_descriptionDict.TryGetValue(_name, out var _description) && !string.IsNullOrEmpty(_description))
                {
                    // 重設顯示內容並縮短描述
                    var _newDescpt = System.Text.RegularExpressions.Regex.Replace(_description, @"\s+", " ");

                    if (_newDescpt.Length > 30)
                    {
                        _newDescpt = _newDescpt.Substring(0, 27) + "...";
                    }

                    schema.Enum.Add(new OpenApiString($"{value} {_name}({_newDescpt})"));
                }
                else
                {
                    schema.Enum.Add(new OpenApiString($"{value} {_name}"));
                }
            }
        }
    }
}

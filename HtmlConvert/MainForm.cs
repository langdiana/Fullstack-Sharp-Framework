using HtmlAgilityPack;
using System;
using System.CodeDom.Compiler;
using System.Text;
using System.Xml.Linq;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HtmlConvert
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		bool HasElements(HtmlNode node)
		{
			HtmlNodeCollection childNodes = node.ChildNodes;
			foreach (var childNode in childNodes)
			{
				if (childNode.NodeType == HtmlNodeType.Element || IsText(childNode))
				{
					return true;
				}
			}
			return false;
		}

		bool HasTextOnly(HtmlNode node)
		{
			HtmlNodeCollection childNodes = node.ChildNodes;
			foreach (var childNode in childNodes)
			{
				if (childNode.NodeType == HtmlNodeType.Element)
				{
					return false;
				}
			}
			return true;
		}

		bool IsNextElement(HtmlNode node)
		{
			HtmlNodeCollection childNodes = node.ChildNodes;
			foreach (var childNode in childNodes)
			{
				if (childNode.NodeType == HtmlNodeType.Element)
				{
					if (IsInline(childNode))
						return false;

					return true;
				}

				if (IsText(childNode))
					return false;
			}

			return false;
		}

		List<string> inlineTags = new List<string>() { "i", "img", "a", "span" };

		bool IsInline(HtmlNode node)
		{
			if (inlineTags.Contains(node.Name))
				return true;

			return false;
		}

		bool IsBlock(HtmlNode node)
		{
			if (!inlineTags.Contains(node.Name))
				return true;

			return false;
		}

		bool IsText(HtmlNode node)
		{
			if (node.NodeType == HtmlNodeType.Text && !string.IsNullOrWhiteSpace(node.InnerText))
				return true;

			return false;
		}

		bool IsAnchor(HtmlNode node)
		{
			return node.Name == "a";
		}

		int GetElementCount(HtmlNode node)
		{
			int count = 0;
			HtmlNodeCollection childNodes = node.ChildNodes;
			foreach (var childNode in childNodes)
			{
				if (childNode.NodeType == HtmlNodeType.Element || IsText(childNode))
				{
					count++;
				}
			}
			return count;
		}

		Dictionary<string, string> attribMap = new();

		void InitAttribMap()
		{
			attribMap["class"] = "className";
			attribMap["for"] = "hFor";
			attribMap["type"] = "htype";
		}

		string GetAttrib(string name)
		{
			return attribMap.ContainsKey(name) ? attribMap[name] : name;
		}

		string GetAttribs(HtmlNode node)
		{
			string result = "";
			foreach (var attrib in node.Attributes)
			{
				var attrName = GetAttrib(attrib.Name); // attrib.Name == "class" ? "className" : attrib.Name;
				string comma = string.IsNullOrEmpty(result) ? "" : ",";
				result = $"{result}{comma} {attrName} = \"{attrib.Value}\"";
			}

			return result;
		}

		void Parse(HtmlNode node, StringBuilder sb, int indent)
		{
			HtmlNodeCollection childNodes = node.ChildNodes;

			indent++;
			var index = 0;
			var elemCount = GetElementCount(node);
			foreach (var childNode in childNodes)
			{
				if (childNode.NodeType == HtmlNodeType.Element || IsText(childNode))
				{
					index++;

					if (childNode.NodeType == HtmlNodeType.Element)
					{
						string sIndent = "";
						if (IsBlock(node) || IsAnchor(node))
						{
							if (sb.Length > 0)
								sb.AppendLine();
							sIndent = new('\t', indent);
						}

						var attrObj = "_";
						if (childNode.HasAttributes)
						{
							attrObj = $"new() {{ {GetAttribs(childNode)} }}";
						}

						if (HasElements(childNode))
							attrObj = $"{attrObj},";
						sb.Append($"{sIndent}{childNode.Name}({attrObj}");
						Parse(childNode, sb, indent);

						var comma = index < elemCount ? "," : "";
						if (IsInline(childNode) && (!IsAnchor(childNode) || HasTextOnly(childNode)))
						{
							sb.Append($"){comma}");
						}
						else
						{
							sb.AppendLine();
							sb.Append($"{sIndent}){comma}");
						}
					}
					else if (IsText(childNode))
					{
						if (!string.IsNullOrWhiteSpace(childNode.InnerText))
						{
							var text = childNode.InnerText.Trim();
							var atSign = "";
							if (text.Contains('\r') || text.Contains("\n"))
								atSign = "@";
							sb.Append($" {atSign}\"{childNode.InnerText.Trim()}\"");
							var comma = index < elemCount ? "," : "";
							sb.Append($"{comma}");
						}
					}
				}
			}
		}

		void Convert()
		{
			InitAttribMap();
			tbResult.Clear();
			var html = tbHtml.Text;
			StringBuilder sbHtml = new();
			sbHtml.Append("<body>");
			sbHtml.Append(html);
			sbHtml.Append("</body>");


			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(sbHtml.ToString());

			var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

			StringBuilder sb = new();

			Parse(node, sb, 1);
			sb.Append(";");

			tbResult.Text = sb.ToString();
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			Convert();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			tbResult.SelectAll();
			tbResult.Copy();
			tbResult.SelectionLength = 0;

		}

	}
}

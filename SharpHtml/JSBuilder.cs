using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpHtml;

public class JSBuilder<T>
{
	T Item = default!;
	string ItemName = "item";

	public JSBuilder(T item, string itemName = "item") 
	{
		Item = item;
		ItemName = itemName;

		StartBuild();
		AddItem();
	}

	public JSBuilder(List<T> items)
	{
		StartBuild();
		AddList(items);
	}

	StringBuilder sb = new();

	void StartBuild()
	{
		sb.AppendLine("{");
	}

	void FinishBuild()
	{
		sb.AppendLine("}");
	}

	void AddItem()
	{
		var itemBody = Item.ToJS();
		sb.AppendLine(@$"
	{ItemName}: {itemBody},
");

	}

	void AddList(List<T> items)
	{
		string script = @$"
	items: {items.ToJS()},
";
		sb.AppendLine(script);

	}

	public string Build()
	{
		FinishBuild();

		return sb.ToString();
	}

	public string Field<U>(Expression<Func<T, U>> exp)
	{
		List<string> nestedMemberList = new();
		Expression? bodyExp = exp.Body;
		while(bodyExp is MemberExpression)
		{
			var memberExp = bodyExp as MemberExpression;
			nestedMemberList.Insert(0, memberExp!.Member.Name); // they are parsed backwards
			bodyExp = memberExp.Expression;
		};

		if (nestedMemberList.Count == 0)
			throw new Exception("Member Expression not supported");

		var result = $"{ItemName}";
		foreach (var memberName in nestedMemberList)
		{
			result = $"{result}.{memberName}";
		}

		return result;
	}

	public string ForItem => "item in items";

	public string JsonData => $"JSON.stringify($data.{ItemName})";

}

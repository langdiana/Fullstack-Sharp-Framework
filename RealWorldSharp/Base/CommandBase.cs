namespace RealWorldSharp.Base;

public class CommandBase: ICommand
{
	public CommandBase()
	{
		CommandId = Guid.NewGuid().ToString();
		CommandName = GetType().Name;
	}

	public readonly string CommandId;
	public readonly string CommandName;

	[JsonIgnore]
	public IResult Result = Results.Empty;

	public bool IsHtmx;
	public int? UserId;
	public string? UserEmail;
	public bool IsAuthenticated => UserId != null;

	[JsonIgnore]
	public CancellationToken CancellationToken;

	public List<string> Errors { get; } = new();
	
	public string ErrorMessage 
	{ 
		get => string.Join("\r\n", Errors);
		set 
		{ 
			Errors.Clear(); 
			Errors.Add(value); 
		}
	}

	public void ClearErrors() => Errors.Clear();

	public void AddError(string error, bool usePrefix = false)
	{
		string? prefix = usePrefix ? "ERROR: " : null;
		Errors.Add($"{prefix}{error}");
	}

	public bool OK => Errors.Count == 0;
	public bool IsCriticalError;
	public string? ErrorDetail;

	public List<string> Warnings { get; } = new();

	public string WarningMessage
	{
		get => string.Join("\r\n", Warnings);
		set
		{
			Warnings.Clear();
			Warnings.Add(value);
		}
	}

	public void ClearWarnings() => Warnings.Clear();

	public void AddWarning(string warning, bool usePrefix = false)
	{
		string? prefix = usePrefix ? "WARNING: " : null;
		Warnings.Add($"{prefix}{warning}");
	}

	public bool HasWarnings => Warnings.Count > 0;

}

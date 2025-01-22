using System.Diagnostics;

namespace RealWorldSharp.Base;

public abstract class ServiceBase
{

	ILogger logger;

	public ServiceBase(ILogger logger)
	{
		this.logger = logger;
	}
	

	public virtual async Task Handle<TCommand>(ICommandHandler<TCommand> cmdHandler, TCommand command) where TCommand : CommandBase
	{
		await InternalHandle(cmdHandler, command);
	}

	protected virtual async Task InternalHandle<TCommand>(ICommandHandler<TCommand> cmdHandler, TCommand cmd) where TCommand : CommandBase
	{
		try
		{
			IActionHandler actHandler = new ActionHandler();
			actHandler = new ProfilerDecorator(actHandler, cmd, logger);
			await actHandler.ExecuteAction(() => cmdHandler.Execute(cmd));
		}
		catch (Exception ex)
		{
			cmd.ErrorMessage = "A technical error has occurred";
			cmd.ErrorDetail = ex.ToString();
			PrepareException(ex, cmd);
		}

	}

	string PrepareException(Exception ex, CommandBase cmd)
	{
		string logDateTime = DateTime.UtcNow.ToString("yyyy-MM-dd_HH:mm:ss");

		CommandExceptionLog log = new();
		log.CommandName = cmd.CommandName;
		log.CommandId = cmd.CommandId;
		log.Date_Time = logDateTime;
		log.Message = ex.ToString();

		string json = JsonConvert.SerializeObject(log, Formatting.Indented);
		logger.LogCritical(json);

		return cmd.CommandId;
	}

	interface IActionHandler
	{
		Task ExecuteAction(Func<Task> act);
	}

	class ActionHandler : IActionHandler
	{
		public virtual async Task ExecuteAction(Func<Task> act)
		{
			await act();
		}
	}

	abstract class ActionDecorator : IActionHandler
	{
		protected IActionHandler target = null!;
		abstract public Task ExecuteAction(Func<Task> act);
	}

	class ProfilerDecorator : ActionDecorator
	{
		CommandBase cmd;
		ILogger logger;

		public ProfilerDecorator(IActionHandler target, CommandBase cmd, ILogger logger)
		{
			this.target = target;
			this.cmd = cmd;
			this.logger = logger;
		}

		void PrepareLog(int logLevel)
		{
			long elapsedMs = watch.ElapsedMilliseconds;

			CommandProfilerLog log = new();
			log.Command = cmd;
			log.Date_Time = DateTime.UtcNow.ToString("yyyy-MM-dd_HH:mm:ss");
			log.Duration = elapsedMs;

			string json = JsonConvert.SerializeObject(log, Formatting.Indented);

			switch (logLevel)
			{
				case 0:
					logger.LogInformation(json);
					break;
				case 1:
					logger.LogWarning(json);
					break;
				case 2:
					logger.LogError(json);
					break;
				case 3:
					logger.LogCritical(json);
					break;
			}
		}

		Stopwatch watch = new();

		public override async Task ExecuteAction(Func<Task> act)
		{
			watch = Stopwatch.StartNew();

			try
			{
				await target.ExecuteAction(act);
				if (cmd.OK)
				{
					if (cmd.HasWarnings)
						PrepareLog(1);
					else
						PrepareLog(0);
				}
				else
					PrepareLog(2);
			}
			catch (TaskCanceledException)
			{
				cmd.ErrorMessage = "Request aborted";
				PrepareLog(2);
			}
			catch
			{
				cmd.ErrorMessage = "A technical error has occurred";
				cmd.ErrorDetail = "See log";
				cmd.IsCriticalError = true;
				PrepareLog(3);
				throw;
			}
			finally
			{
				watch.Stop();
			}
		}

	}

}

public class CommandProfilerLog
{
	public ICommand Command = null!;
	public string Date_Time = null!;
	public long Duration;
}

public class CommandExceptionLog
{
	public string CommandName = null!;
	public string CommandId = null!;
	public string Date_Time = null!;
	public string Message = null!;
}
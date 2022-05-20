using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_3
{
	class Program
	{
		static void Main(string[] args)
		{
			TaskGenerator taskGenerator = new();
			Task<string>[] tasks = new Task<string>[10];

			for(int i = 0; i < 10; i++)
			{
				tasks[i] = taskGenerator.createTask($"Task - {i + 1}");
			}
			Parallel.ForEach(tasks, task =>
			{
				task.Start();
			});
			
			Console.WriteLine("\nРезультат: " + tasks[Task.WaitAny(tasks)].Result);
		}
	}
	class TaskGenerator
	{
		private readonly Random random = new Random();
		private const int minDelay = 10;
		private const int maxDelay = 300;

		public Task<string> createTask(string tag)
		{
			var delay = random.Next(minDelay, maxDelay);
			return getTagWithDelay(tag, delay);
		}

		private Task<string> getTagWithDelay(string tag, int delay)
		{
			Console.WriteLine($"==> {tag} has delay {delay}");
			return new Task<string>(() =>
			{
				Thread.Sleep(delay);
				Console.WriteLine($"==> {tag} executed with delay {delay}");
				return tag;
			});
		}
	}
}

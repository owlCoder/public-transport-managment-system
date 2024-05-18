using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
	public interface ILogger
	{
		void Log(LogLevel level, string message);
	}
}

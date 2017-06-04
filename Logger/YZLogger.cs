using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class YZLogger : ILogger {
    protected string logDir = "./Log";
    protected long file_max_len = 1024000;
    FileStream m_logFs;
    StreamWriter m_swLog;

    protected bool isLogEnabled = true;

    public LogType filterLogType 
    {
        get;
        set; 
    }

    public bool logEnabled 
    { 
        get
        {
            return isLogEnabled;
        }
        set
        {
            isLogEnabled = value;
        }
    }
    public ILogHandler logHandler { get; set; }

    protected bool IsDebug()
    {   
        if(Directory.Exists(logDir))
        {   
            return true;
        }
        return false;
    }

    protected StreamWriter GetLogWriter()
    {
        if(!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }
        if (m_logFs == null)
        {
			m_logFs = new FileStream(logDir + Path.DirectorySeparatorChar + GetDateFormat() + ".log", FileMode.CreateNew);
        }
        else if (m_logFs.Length > file_max_len)
        {
            m_swLog.Close();
            m_logFs.Flush();
            m_logFs.Close();
            m_logFs = new FileStream(logDir + Path.PathSeparator + GetDateFormat() + ".log", FileMode.CreateNew);
        }
        if(m_swLog == null)
        {
            m_swLog = new StreamWriter(m_logFs);
        }
        return m_swLog;
    }


    public string formatLog(LogType logType, string msg)
    {
        return formatLog(logType.ToString(), msg);
    }

    public string formatLog(string tag, string msg)
    {
        return GetDateFormat() + " [" + tag + "] " + msg;
    }

    public string formatLog(LogType logType, string tag, string msg)
    {
        return GetDateFormat() + "[" + logType.ToString() + "][" + tag + "] " + msg;
    }


    protected string GetDateFormat()
    {
        return DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");
    }

    public bool IsLogTypeAllowed(LogType logType)
    {
        return true;
    }

    public void Log(object message)
    {
        Log(LogType.Log, message);
    }

    protected void WriteLog(string msg)
    {
        if(!isLogEnabled)
        {
            return;
        }
        GetLogWriter().WriteLine(msg);
        GetLogWriter().Flush();
    }

    public void Log(LogType logType, object message)
    {
        WriteLog(formatLog(logType, message.ToString()));
    }

    public void Log(string tag, object message)
    {
        WriteLog(formatLog(tag, message.ToString()));
    }

    public void Log(LogType logType, object message, UnityEngine.Object context)
    {
        WriteLog(formatLog(logType, "[Context] " + context.ToString() + " [msg]" + message.ToString()));
    }

    public void Log(LogType logType, string tag, object message)
    {
        WriteLog(formatLog(logType, tag, message.ToString()));
    }

    public void Log(string tag, object message, UnityEngine.Object context)
    {
        WriteLog(formatLog(tag, "[Context] " + context.ToString() + " [msg]" + message.ToString()));
    }

    public void Log(LogType logType, string tag, object message, UnityEngine.Object context)
    {
        WriteLog(formatLog(logType, tag, "[Context] " + context.ToString() + " [msg]" + message.ToString()));
    }

    public void LogError(string tag, object message)
    {
        Log(LogType.Error, tag, message);
    }

    public void LogError(string tag, object message, UnityEngine.Object context)
    {
        Log(LogType.Error, tag, message, context);
    }

    public void LogException(Exception exception)
    {
        Log(LogType.Exception, exception.Message);
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        if (logHandler != null)
        {
            logHandler.LogException(exception, context);
        }
        else
        {
            Log(LogType.Exception, exception, context);
        }
    }
    public void LogFormat(LogType logType, string format, params object[] args)
    {
        Log(logType, string.Format(format, args));
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        if(logHandler != null)
        {
            LogFormat(logType, context, format, args);
        }
    }
    public void LogWarning(string tag, object message)
    {
        Log(LogType.Warning, message);
    }

    public void LogWarning(string tag, object message, UnityEngine.Object context)
    {
        Log(LogType.Warning, tag, message, context);
    }
}

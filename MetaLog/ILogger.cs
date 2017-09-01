﻿using System;
using System.IO;

namespace MetaLog {

    /// <summary>
    /// A MetaLog logging severity enum
    /// </summary>
    public enum LogSeverity {
        /// <summary>
        /// Indicating the log-message is for debugging
        /// </summary>
        Debug,
        /// <summary>
        /// Indicating the log-message is some kind of 
        /// information or progress update
        /// </summary>
        Info,
        /// <summary>
        /// Indicating the log-message is an ignorable
        /// warning or usual exception
        /// </summary>
        Warning,
        /// <summary>
        /// Indicating the log-message is a runtime-affecting
        /// error or unexpected exception
        /// </summary>
        Error,
        /// <summary>
        /// Indicating the log-message is an unexpected exception
        /// which may prevent the application from continuing
        /// </summary>
        Critical
    }

    /// <summary>
    /// A MetaLog logger
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// The log file this <see cref="ILogger"/> instance logs to
        /// </summary>
        string LogFile { get; }
        /// <summary>
        /// Whether this <see cref="ILogger"/> should use a single
        /// (File)-<see cref="Stream"/> for logging instead of opening new
        /// ones each log. (This may be faster but locks the file
        /// until the <see cref="ILogger"/> gets disposed, see: 
        /// <see href="https://en.wikipedia.org/wiki/File_locking">file locking</see>)
        /// </summary>
        bool UseStream { get; }
        /// <summary>
        /// The minimum <see cref="LogSeverity"/> to log by this Logger instance
        /// (It is recommended to use higher values such as <see cref="LogSeverity.Error"/>
        /// for releases)
        /// </summary>
        LogSeverity MinimumSeverity { get; }

        /// <summary>
        /// Log a new message to the <see cref="LogFile"/>
        /// </summary>
        /// <param name="severity">The <see cref="LogSeverity"/> of this message</param>
        /// <param name="message">The actual log-message</param>
        void Log(LogSeverity severity, string message);
        /// <summary>
        /// Log a new <see cref="Exception"/> tree (up to most 
        /// inner <see cref="Exception"/>) to the <see cref="LogFile"/>
        /// </summary>
        /// <param name="severity">The <see cref="LogSeverity"/> of this message</param>
        /// <param name="exception">An occured <see cref="Exception"/></param>
        void Log(LogSeverity severity, Exception exception);
    }
}
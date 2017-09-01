# MetaLog
> **Simple, yet effective, logging with Metadata CallerInfo for creating beautiful Log files in C#/VB .NET**

> _.NET Standard 2.0 Class Library_

Install via **NuGet Package Manager**:
```pm
PM> Install-Package MetaLog
```

# Usage
### Namespace
```cs
using MetaLog;
```

### Using the `ILogger`
```cs
// create a new Logger with log file at Documents\\log.log, and minimum log severity "Info"
ILogger logger = Logger.New("C:\\Users\\mrousavy\\Documents\\log.log", LogSeverity.Info);

logger.Log(LogSeverity.Info, "Logged in as mrousavy, version 1.0!");

/// Output (log.log):
/// [Info] [2017-09-01 20:37:02.755] [StaticTests.CustomTest:16]: Logged in as mrousavy, version 1.0!
```

### Using Streams
```cs
// create a new Logger with stream to log file at Documents\\log.log, and minimum log severity "Info"
using(ILogger logger = Logger.New("C:\\Users\\mrousavy\\Documents\\log.log", LogSeverity.Info, true)) {
  logger.Log(LogSeverity.Info, "Logged in as mrousavy, version 1.0!");
} //will call Dispose() here, you can also call it manually without the using(..){..} directive

/// Output (log.log):
/// [Info] [2017-09-01 20:37:02.755] [StaticTests.CustomTest:16]: Logged in as mrousavy, version 1.0!
```

### The Exception Tree
```cs
try {
  // something that will throw
} catch (Exception ex) {
  logger.Log(LogSeverity.Error, ex);
}

/// Output (log.log):
/// [Error]    [2017-09-01 20:53:27.936] [StaticTests.CustomTest:20]:             BEGIN EXCEPTION TREE:
///   ┌ System.IO.DirectoryNotFoundException: Could not find a part of the path 'C:\Users\mrousavy\test.txt'.
///   ├ ┬ at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
///   ├ ├    at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, /// SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
///   ├ ├    at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
///   ├ ├    at System.IO.StreamReader..ctor(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean checkHost)
///   ├ ├    at System.IO.File.InternalReadAllText(String path, Encoding encoding, Boolean checkHost)
///   ├ ├    at System.IO.File.ReadAllText(String path)
///   └ └    at MetaLog.Tests.StaticTests.CustomTest() in D:\Projects\MetaLog\MetaLog.Tests\StaticTests.cs:line 18

```

### Using the static `Logger` class
```cs
using MetaLog;
// ...
Logger.Log(LogSeverity.Info, "Logged in as mrousavy, version 1.0!");

/// Output (log.log):
/// [Info] [2017-09-01 20:37:02.755] [StaticTests.CustomTest:16]: Logged in as mrousavy, version 1.0!
```

# Performance
![.NET Unit-Test Results](http:)

# License
> Thanks for using **MetaLog**! License: [MIT](https://github.com/mrousavy/MetaLog/blob/master/LICENSE), [mrousavy](http://github.com/mrousavy) 2017

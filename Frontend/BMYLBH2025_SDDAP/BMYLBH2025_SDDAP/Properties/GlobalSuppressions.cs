using System.Diagnostics.CodeAnalysis;

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Frontend applications require comprehensive exception handling to prevent crashes")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Code clarity and debugging purposes")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Form event handlers and UI methods should remain instance methods")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Event handlers have validated parameters by framework")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "UI applications can use default culture formatting")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "UI controls are managed by the form lifecycle")]
[assembly: SuppressMessage("Design", "CA1064:Exceptions should be public", Justification = "Internal exceptions for application flow control")]
[assembly: SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "Simple applications may use direct logging")]
[assembly: SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "Configuration-based URI handling is acceptable")]
[assembly: SuppressMessage("Design", "CA1707:Identifiers should not contain underscores", Justification = "Event handler naming conventions")] 
﻿using System;
using System.IO;
using System.Reflection;

namespace MetaLog {
    public static class Helper {
        private const string TreeStart = "┌";
        private const string TreeItem = "├";
        private const string TreeEnd = "└";
        private const string SubTreeStart = "┬";
        private const string HSpacer = "─";


        /// <summary>
        /// Path to %appdata%
        /// </summary>
        public static string AppData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// Path to %appdata%/MetaLog
        /// </summary>
        public static string MetaLogAppData {
            get {
                string path = Path.Combine(AppData, "MetaLog");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }

        /// <summary>
        /// Get the Assembly this MetaLogger is called by
        /// </summary>
        public static string ExecutingAssemblyName {
            get {
                var execAssembly = Assembly.GetExecutingAssembly();
                return execAssembly.FullName;
            }
        }

        /// <summary>
        /// Censor a given string.
        /// </summary>
        /// <param name="text">The text to censor</param>
        /// <param name="censorPercent">The amount of text to censor 
        /// (from left to right) in percent (from 0 to 1)</param>
        /// <param name="censorChar">A custom character to use
        /// for censoring</param>
        public static string Censor(string text, double censorPercent = 0.4, char censorChar = '•') {
            int length = text.Length;
            int toCensor = (int)Math.Floor(length * censorPercent);
            string censored = text.Substring(0, toCensor) + new string(censorChar, length - toCensor);
            return censored;
        }

        /// <summary>
        /// Walk up an <see cref="Exception.InnerException"/> tree
        /// and return the result in string form.
        /// </summary>
        /// <param name="exception">The original <see cref="Exception"/></param>
        /// <param name="indent">The amount of spaces for indentation
        /// (will increase by 4 each inner-exception)</param>
        /// <returns>A built tree of <see cref="Exception.InnerException"/>s</returns>
        public static string RecurseException(Exception exception, int indent = 0) {

            string message = exception.Message;
            if (exception.InnerException != null) {
                message += RecurseException(exception.InnerException, indent + 4);
            }
            return message;
        }

        /// <summary>
        /// Build a tree of the given input
        /// </summary>
        /// <param name="input">The given input string</param>
        /// <param name="isSubtree">Indicating whether this is a
        /// subtree</param>
        /// <param name="isEnd">Indicating whether this is the
        /// last tree</param>
        /// <returns>A built tree</returns>
        public static string BuildTree(string input, bool isSubtree, bool isEnd) {
            string[] lines = input.Split(new[] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
            switch (lines.Length) {
                case 0:
                    return string.Empty;
                case 1:
                    return $"{SubTreeStart} {lines[0]}";
            }
            
            string nl = Environment.NewLine;

            string trimmed = lines[0].TrimStart(); //remove first whitespaces
            string indent = new string(' ', lines[0].Length - trimmed.Length); //get original whitespace indent

            string start = isSubtree ? SubTreeStart : TreeStart; //make ┬ or ┌ 
            lines[0] = $"{indent}{start} {trimmed}";

            for (int i = 1; i < lines.Length - 1; i++) {
                lines[i] = $"{indent}{TreeItem} {lines[i]}"; //make ├ {line}
            }

            string result;
            if (isEnd) {
                lines[lines.Length - 1] += $"{indent}{TreeEnd}{lines[lines.Length - 1]}";  //make └
                result = lines.ToString();
            } else {
                lines[lines.Length - 1] += $"{indent}{TreeItem}{lines[lines.Length - 1]}";  //make ├
                result = lines.ToString();
                result += $"{nl}{indent}{TreeEnd}{HSpacer}"; //make └─
            }
            return result;
        }
    }
}
﻿using System;
using System.Runtime.InteropServices;

namespace TFive_Windows_Information
{
    internal static class Win32
    {
        #region "DllImport"
        [DllImport("user32", EntryPoint = "IsWindow", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int IsWindow(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "IsWindowUnicode", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        internal static extern int IsWindowUnicode(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "GetWindowTextLengthA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowTextLengthA(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "GetWindowTextLengthW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowTextLengthW(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "GetWindowTextA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowTextA(IntPtr hWnd, IntPtr lpString, int cch);

        [DllImport("user32", EntryPoint = "GetWindowTextW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetWindowTextW(IntPtr hWnd, IntPtr lpString, int cch);

        [DllImport("user32", EntryPoint = "GetClassNameA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetClassNameA(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);

        [DllImport("user32", EntryPoint = "GetClassNameW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetClassNameW(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);



        #endregion
        internal static string GetWindowText(IntPtr hWnd)
        {
            int cch;
            IntPtr lpString;
            string sResult;

            if (IsWindow(hWnd) == 0)
                return "";

            if (IsWindowUnicode(hWnd) != 0)
            {
                lpString = Marshal.AllocHGlobal((cch = GetWindowTextLengthW(hWnd) + 1) * 2);
                GetWindowTextW(hWnd, lpString, cch);
                sResult = Marshal.PtrToStringUni(lpString, cch);
                Marshal.FreeHGlobal(lpString);
                return sResult;
            }
            lpString = Marshal.AllocHGlobal(cch = GetWindowTextLengthA(hWnd) + 1);
            GetWindowTextA(hWnd, lpString, cch);
            sResult = Marshal.PtrToStringAnsi(lpString, cch);
            Marshal.FreeHGlobal(lpString);
            return sResult;
        }

        internal static string GetClassName(IntPtr hWnd)
        {
            const int windowClassNameLength = 255;
            int cch;
            IntPtr lpString;
            string sResult;
            if (IsWindow(hWnd) == 0)
                return "";

            if (IsWindowUnicode(hWnd) != 0)
            {
                lpString = Marshal.AllocHGlobal((cch = windowClassNameLength + 1) * 2);
                GetClassNameW(hWnd, lpString, cch);
                sResult = Marshal.PtrToStringUni(lpString, cch);
                Marshal.FreeHGlobal(lpString);
                return sResult;
            }
            lpString = Marshal.AllocHGlobal(cch = GetWindowTextLengthA(hWnd) + 1);
            GetClassNameA(hWnd, lpString, cch);
            sResult = Marshal.PtrToStringAnsi(lpString, cch);
            Marshal.FreeHGlobal(lpString);
            return sResult;
        }


    }

}


/*
  2011 - This file is part of AcaLabelPrint 

  AcaLabelPrint is free Software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  AcaLabelprint is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with AcaLabelPrint.  If not, see <http:www.gnu.org/licenses/>.

  We encourage you to use and extend the functionality of AcaLabelPrint,
  and send us an e-mail on the outlines of the extension you build. If
  it's generic, maybe we could add it to the project.
  Send your mail to the projectadmin at http:sourceforge.net/projects/labelprint/
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace ACA.LabelX.Toolbox
{
    public class Win32ApiFunctions
    {
        //
        //Warning: If you change the struct layout, test as 64 and 32 bit. Like below it seems
        //to work for both... (on vista 64)... Need to test on XP and vista 32 also!!!
        //
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            //[MarshalAs(UnmanagedType.U4)]
            public uint wFunc;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pFrom;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pTo;
            public ushort fFlags;
            //[MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        const int FO_DELETE = 3;
        const int FOF_ALLOWUNDO = 0x40;
        const int FOF_NOCONFIRMATION = 0x10; //Don't prompt the user.; 
        
    }
}

//Struct stolen from below code... It might be usefull for future reference:
// http://www.koders.com/csharp/fidA3E1E78A50AB1607F3FEF5A73D6F61287AC68F21.aspx
//namespace ZO.SmartCore.My4CSharp
//{
//    /// <summary>
//    /// No suppress unmanaged code attribute, these are methods that can be used anywhere because a stack walk will be performed.
//    /// </summary>
//    internal static class NativeMethods
//    {

//        #region Constructors
        
   


//        #endregion

//        #region Destructor

//        #endregion

//        #region Fields
        
//        /// <summary>
//        /// kernel32.dll
//        /// </summary>
//        private const string kernel32 = "kernel32.dll";

//        #endregion

//        #region Events

//        #endregion

//        #region Operators

//        #endregion

//        #region Properties

//        #endregion

//        #region Methods
//        [DllImport(kernel32, CharSet = CharSet.Auto, SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        public static extern void GlobalMemoryStatus([In, Out] MEMORYSTATUS lpBuffer);

//        [return: MarshalAs(UnmanagedType.Bool)]
//        [DllImport(kernel32, CharSet = CharSet.Auto, SetLastError = true)]
//        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

//        /// <summary>
//        /// moves an existing file or directory.
//        /// </summary>
//        /// <param name="lpExistingFileName">Pointer to a null-terminated string that names an existing file or directory on the local computer.</param>
//        /// <param name="lpNewFileName">Pointer to a null-terminated string that specifies the new name of lpExistingFileName on the local computer. </param>
//        /// <param name="dwFlags"></param>
//        /// <returns></returns>
//        [return: MarshalAs(UnmanagedType.Bool)]
//        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
//        public static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);


//        /// <summary>
//        /// This function can be used to copy, move, rename, or delete a file system object.
//        /// </summary>
//        /// <param name="lpFileOp">Address of an SHFILEOPSTRUCT structure that contains information this function needs to carry out the specified operation.</param>
//        /// <returns>Returns true if successful, or false otherwise.</returns>
//        [DllImport("shell32.dll", EntryPoint = "SHFileOperation", CharSet = CharSet.Auto, SetLastError = true)]
//        private static extern int SHFileOperation32([In, Out] SHFILEOPSTRUCT lpFileOp);


//        /// <summary>
//        /// This function can be used to copy, move, rename, or delete a file system object.
//        /// </summary>
//        /// <param name="lpFileOp">Address of an SHFILEOPSTRUCT structure that contains information this function needs to carry out the specified operation.</param>
//        /// <returns>Returns true if successful, or false otherwise.</returns>
//        /// <remarks>Used on 64 Bit Platform</remarks>
//        [DllImport("shell32.dll", EntryPoint = "SHFileOperation", CharSet = CharSet.Auto, SetLastError = true)]
//        private static extern int SHFileOperation64([In, Out] SHFILEOPSTRUCT64 lpFileOp);

//        /// <summary>
//        /// This function can be used to copy, move, rename, or delete a file system object.
//        /// </summary>
//        /// <param name="lpFileOp">Address of an SHFILEOPSTRUCT structure that contains information this function needs to carry out the specified operation.</param>
//        /// <returns>Returns true if successful, or false otherwise.</returns>
//        public static int SHFileOperation(SHFILEOPSTRUCT lpFileOp)
//        {
//            if (IntPtr.Size == 4)
//            {
//                return SHFileOperation32(lpFileOp);
//            }
//            SHFILEOPSTRUCT64 shfileopstruct1 = new SHFILEOPSTRUCT64();
//            shfileopstruct1.hwnd = lpFileOp.hwnd;
//            shfileopstruct1.wFunc = lpFileOp.wFunc;
//            shfileopstruct1.pFrom = lpFileOp.pFrom;
//            shfileopstruct1.pTo = lpFileOp.pTo;
//            shfileopstruct1.fFlags = lpFileOp.fFlags;
//            shfileopstruct1.fAnyOperationsAborted = lpFileOp.fAnyOperationsAborted;
//            shfileopstruct1.hNameMappings = lpFileOp.hNameMappings;
//            shfileopstruct1.lpszProgressTitle = lpFileOp.lpszProgressTitle;
//            int ret = SHFileOperation64(shfileopstruct1);
//            lpFileOp.fAnyOperationsAborted = shfileopstruct1.fAnyOperationsAborted;
//            return ret;
//        }

//        /// <summary>
//        /// Notifies the system of an event that an application has performed. An application should use this function if it performs an action that may affect the Shell. 
//        /// </summary>
//        /// <param name="wEventId"></param>
//        /// <param name="uFlags"></param>
//        /// <param name="dwItem1"></param>
//        /// <param name="dwItem2"></param>
//        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        public static extern void SHChangeNotify(SHChangeEventType wEventId, SHChangeEventParameterFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);

 
//        #endregion

//        #region MoveFileFlags

//        /// <summary>
//        /// Flags for MoveFileEx and MoveFileWithProgress 
//        /// </summary>
//        public enum MoveFileFlags
//        {
//            /// <summary>
//            /// If a file named lpNewFileName exists, the function replaces its contents with the contents of the 
//            /// lpExistingFileName file. 
//            /// This value cannot be used if lpNewFileName or lpExistingFileName names a directory. 
//            /// </summary>
//            MOVEFILE_REPLACE_EXISTING = 0x00000001,

//            /// <summary>
//            /// If the file is to be moved to a different volume, the function simulates the move by using 
//            /// the CopyFile and DeleteFile functions. 
//            /// This value cannot be used with MOVEFILE_DELAY_UNTIL_REBOOT.
//            /// </summary>
//            MOVEFILE_COPY_ALLOWED = 0x00000002,

//            /// <summary>
//            /// The system does not move the file until the operating system is restarted. 
//            /// The system moves the file immediately after AUTOCHK is executed, but before creating 
//            /// any paging files. Consequently, this parameter enables the function to delete paging files
//            /// from previous startups. 
//            /// This value can be used only if the process is in the context of a user who belongs to 
//            /// the administrator group or the LocalSystem account.
//            /// This value cannot be used with MOVEFILE_COPY_ALLOWED.
//            /// </summary>
//            ///<remarks>
//            /// Windows 2000:  If you specify the MOVEFILE_DELAY_UNTIL_REBOOT flag for dwFlags, you cannot also prepend the filename specified by lpExistingFileName with "\\?".            
//            ///</remarks>
//            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,

//            /// <summary>
//            /// The function does not return until the file has actually been moved on the disk. 
//            /// Setting this value guarantees that a move performed as a copy and delete operation is
//            /// flushed to disk before the function returns. The flush occurs at the end of the copy operation.
//            /// This value has no effect if MOVEFILE_DELAY_UNTIL_REBOOT is set.
//            /// </summary>
//            MOVEFILE_WRITE_THROUGH = 0x00000008,

//            /// <summary>
//            /// Reserved for future use.
//            /// </summary>
//            MOVEFILE_CREATE_HARDLINK = 0x00000010,

//            /// <summary>
//            /// The function fails if the source file is a link source, but the file cannot be tracked after
//            /// the move. This situation can occur if the destination is a volume formatted with the FAT file system.
//            /// </summary>
//            /// <remarks>Windows NT:  This value is not supported.</remarks>
//            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
//        }
//        #endregion

//        #region SHFileOperationType

//        /// <summary>
//        /// indicates which operation to perform
//        /// </summary>
//        public enum SHFileOperationType : uint
//        {
//            /// <summary>
//            /// Copies the files specified in the pFrom member to the location specified in the pTo member. 
//            /// </summary>
//            FO_COPY = 2,

//            /// <summary>
//            /// Deletes the files specified in pFrom.
//            /// </summary>
//            FO_DELETE = 3,

//            /// <summary>
//            /// Moves the files specified in pFrom to the location specified in pTo. 
//            /// </summary>
//            FO_MOVE = 1,

//            /// <summary>
//            /// Renames the file specified in pFrom. You cannot use this flag to rename multiple files with a single function call. Use FO_MOVE instead
//            /// </summary>
//            FO_RENAME = 4
//        }
//        #endregion

//        #region ShFileOperationFlags

//        /// <summary>
//        /// Flags that control the file operation
//        /// </summary>
//        [Flags]
//        public enum ShFileOperationFlags : ushort
//        {
//            /// <summary>
//            /// Preserve Undo information, if possible. If pFrom does not contain fully qualified path and file names, this flag is ignored.
//            /// </summary>
//            FOF_ALLOWUNDO = 0x40,

//            /// <summary>
//            /// Not used.
//            /// </summary>
//            FOF_CONFIRMMOUSE = 2,

//            /// <summary>
//            /// Perform the operation on files only if a wildcard file name (*.*) is specified.
//            /// </summary>
//            FOF_FILESONLY = 0x80,

//            /// <summary>
//            /// The pTo member specifies multiple destination files (one for each source file) rather than one directory where all source files are to be deposited.
//            /// </summary>
//            FOF_MULTIDESTFILES = 1,

//            /// <summary>
//            /// Do not move connected files as a group. Only move the specified files.
//            /// </summary>
//            FOF_NO_CONNECTED_ELEMENTS = 0x2000,

//            /// <summary>
//            /// Respond with "Yes to All" for any dialog box that is displayed.
//            /// </summary>
//            FOF_NOCONFIRMATION = 0x10,

//            /// <summary>
//            /// Do not confirm the creation of a new directory if the operation requires one to be created.
//            /// </summary>
//            FOF_NOCONFIRMMKDIR = 0x200,

//            /// <summary>
//            /// Do not copy the security attributes of the file.
//            /// </summary>
//            FOF_NOCOPYSECURITYATTRIBS = 0x800,

//            /// <summary>
//            /// Do not display a user interface if an error occurs.
//            /// </summary>
//            FOF_NOERRORUI = 0x400,

//            /// <summary>
//            /// Treat reparse points as objects, not containers. You must set _WIN32_WINNT to 5.01 or later to use this flag
//            /// </summary>
//            FOF_NORECURSEREPARSE = 0x8000,

//            /// <summary>
//            /// Only operate in the local directory. Don't operate recursively into subdirectories.
//            /// </summary>
//            FOF_NORECURSION = 0x1000,

//            /// <summary>
//            /// Give the file being operated on a new name in a move, copy, or rename operation if a file with the target name already exists.
//            /// </summary>
//            FOF_RENAMEONCOLLISION = 8,

//            /// <summary>
//            /// Do not display a progress dialog box.
//            /// </summary>
//            FOF_SILENT = 4,

//            /// <summary>
//            /// Display a progress dialog box but do not show the file names.
//            /// </summary>
//            FOF_SIMPLEPROGRESS = 0x100,

//            /// <summary>
//            /// If FOF_RENAMEONCOLLISION is specified and any files were renamed, assign a name mapping object containing their old and new names to the hNameMappings member.
//            /// </summary>
//            FOF_WANTMAPPINGHANDLE = 0x20,

//            /// <summary>
//            /// Send a warning if a file is being destroyed during a delete operation rather than recycled. This flag partially overrides FOF_NOCONFIRMATION.
//            /// </summary>
//            FOF_WANTNUKEWARNING = 0x4000
//        }

//        #endregion

//        #region SHFILEOPSTRUCT

//        /// <summary>
//        /// contains information that the SHFileOperation function uses to perform file operations.
//        /// </summary>
//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
//        public class SHFILEOPSTRUCT
//        {
//            /// <summary>
//            /// Window handle to the dialog box to display information about the status of the file operation.
//            /// </summary>
//            public IntPtr hwnd;

//            /// <summary>
//            /// Value that indicates which operation to perform
//            /// </summary>
//            public SHFileOperationType wFunc;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pFrom;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pTo;

//            /// <summary>
//            /// Address of a buffer to specify one or more source file names. These names must be fully 
//            /// qualified paths. Standard Microsoft MS-DOS wild cards, such as "*", are permitted in the
//            /// file-name position. Although this member is declared as a null-terminated string, it is
//            /// used as a buffer to hold multiple file names. Each file name must be terminated by a 
//            /// single NULL character. An additional NULL character must be appended to the end of the
//            /// final name to indicate the end of pFrom.
//            /// </summary>
//            public ShFileOperationFlags fFlags;

//            /// <summary>
//            /// Value that receives TRUE if the user aborted any file operations before they were completed,
//            /// or FALSE otherwise.
//            /// </summary>
//            public bool fAnyOperationsAborted;

//            /// <summary>
//            /// A handle to a name mapping object containing the old and new names of the renamed files.
//            /// This member is used only if the fFlags member includes the FOF_WANTMAPPINGHANDLE flag. 
//            /// </summary>
//            public IntPtr hNameMappings;

//            /// <summary>
//            /// Address of a string to use as the title of a progress dialog box. 
//            /// This member is used only if fFlags includes the FOF_SIMPLEPROGRESS flag.
//            /// </summary>
//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string lpszProgressTitle;
//        }
//        #endregion


//        #region SHFILEOPSTRUCT64

//        /// <summary>
//        /// contains information that the SHFileOperation function uses to perform file operations.
//        /// </summary>
//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        private class SHFILEOPSTRUCT64
//        {
//            /// <summary>
//            /// Window handle to the dialog box to display information about the status of the file operation.
//            /// </summary>
//            public IntPtr hwnd;

//            /// <summary>
//            /// Value that indicates which operation to perform
//            /// </summary>
//            public SHFileOperationType wFunc;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pFrom;

//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string pTo;

//            /// <summary>
//            /// Address of a buffer to specify one or more source file names. These names must be fully 
//            /// qualified paths. Standard Microsoft MS-DOS wild cards, such as "*", are permitted in the
//            /// file-name position. Although this member is declared as a null-terminated string, it is
//            /// used as a buffer to hold multiple file names. Each file name must be terminated by a 
//            /// single NULL character. An additional NULL character must be appended to the end of the
//            /// final name to indicate the end of pFrom.
//            /// </summary>
//            public ShFileOperationFlags fFlags;

//            /// <summary>
//            /// Value that receives TRUE if the user aborted any file operations before they were completed,
//            /// or FALSE otherwise.
//            /// </summary>
//            public bool fAnyOperationsAborted;

//            /// <summary>
//            /// A handle to a name mapping object containing the old and new names of the renamed files.
//            /// This member is used only if the fFlags member includes the FOF_WANTMAPPINGHANDLE flag. 
//            /// </summary>
//            public IntPtr hNameMappings;

//            /// <summary>
//            /// Address of a string to use as the title of a progress dialog box. 
//            /// This member is used only if fFlags includes the FOF_SIMPLEPROGRESS flag.
//            /// </summary>
//            [MarshalAs(UnmanagedType.LPTStr)]
//            public string lpszProgressTitle;
//        }
//        #endregion

//        #region SHChangeEventParameterFlags

//        /// <summary>
//        /// Flags that indicate the meaning of the dwItem1 and dwItem2 parameters. 
//        /// </summary>
//        public enum SHChangeEventParameterFlags : uint
//        {
//            /// <summary>
//            /// dwItem1 and dwItem2 are the addresses of ITEMIDLIST structures that represent the item(s) affected by the change. Each ITEMIDLIST must be relative to the desktop folder. 
//            /// </summary>
//            SHCNF_IDLIST = 0x0000,


//            SHCNF_PATHA = 0x0001,
//            SHCNF_PRINTERA = 0x0002,

//            /// <summary>
//            /// The dwItem1 and dwItem2 parameters are DWORD values. 
//            /// </summary>
//            SHCNF_DWORD = 0x0003,
//            SHCNF_PATHW = 0x0005,
//            SHCNF_PRINTERW = 0x0006,
//            SHCNF_TYPE = 0x00FF,

//            /// <summary>
//            /// The function should not return until the notification has been delivered to all affected components. As this flag modifies other data-type flags, it cannot by used by itself.
//            /// </summary>
//            SHCNF_FLUSH = 0x1000,

//            /// <summary>
//            /// The function should begin delivering notifications to all affected components but should return as soon as the notification process has begun. As this flag modifies other data-type flags, it cannot by used by itself.
//            /// </summary>
//            SHCNF_FLUSHNOWAIT = 0x2000,

//            /// <summary>
//            /// dwItem1 and dwItem2 are the addresses of null-terminated strings of maximum length MAX_PATH that contain the full path names of the items affected by the change. 
//            /// </summary>
//            SHCNF_PATH = SHCNF_PATHW,

//            /// <summary>
//            /// dwItem1 and dwItem2 are the addresses of null-terminated strings that represent the friendly names of the printer(s) affected by the change. 
//            /// </summary>
//            SHCNF_PRINTER = SHCNF_PRINTERW
//        }
//        #endregion

//        #region MEMORYSTATUS

//        /// <summary>
//        /// The MEMORYSTATUS structure contains information about the current state of both physical and virtual memory.
//        /// </summary>
//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        public class MEMORYSTATUS
//        {
//            /// <summary>
//            /// Size of the MEMORYSTATUS data structure, in bytes. You do not need to set this member before calling the GlobalMemoryStatus function; the function sets it. 
//            /// </summary>
//            public uint dwLength;

//            /// <summary>
//            /// Number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use). 
//            /// Windows NT:  Percentage of approximately the last 1000 pages of physical memory that is in use.
//            /// </summary>
//            public uint dwMemoryLoad;

//            /// <summary>
//            /// Total size of physical memory, in bytes. 
//            /// </summary>
//            public uint dwTotalPhys;

//            /// <summary>
//            /// Size of physical memory available, in bytes
//            /// </summary>
//            public uint dwAvailPhys;

//            /// <summary>
//            /// Size of the committed memory limit, in bytes. 
//            /// </summary>
//            public uint dwTotalPageFile;

//            /// <summary>
//            /// Size of available memory to commit, in bytes. 
//            /// </summary>
//            public uint dwAvailPageFile;

//            /// <summary>
//            /// Total size of the user mode portion of the virtual address space of the calling process, in bytes. 
//            /// </summary>
//            public uint dwTotalVirtual;

//            /// <summary>
//            /// Size of unreserved and uncommitted memory in the user mode portion of the virtual address space of the calling process, in bytes. 
//            /// </summary>
//            public uint dwAvailVirtual;

//        } // class MEMORYSTATUS
//        #endregion

//        #region MEMORYSTATUSEX

//        /// <summary>
//        /// contains information about the current state of both physical and virtual memory, including extended memory
//        /// </summary>
//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        public class MEMORYSTATUSEX
//        {
//            /// <summary>
//            /// Size of the structure, in bytes. You must set this member before calling GlobalMemoryStatusEx. 
//            /// </summary>
//            public uint dwLength;

//            /// <summary>
//            /// Number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use). 
//            /// </summary>
//            public uint dwMemoryLoad;

//            /// <summary>
//            /// Total size of physical memory, in bytes.
//            /// </summary>
//            public ulong ullTotalPhys;

//            /// <summary>
//            /// Size of physical memory available, in bytes. 
//            /// </summary>
//            public ulong ullAvailPhys;

//            /// <summary>
//            /// Size of the committed memory limit, in bytes. This is physical memory plus the size of the page file, minus a small overhead. 
//            /// </summary>
//            public ulong ullTotalPageFile;


//            /// <summary>
//            /// Size of available memory to commit, in bytes. The limit is ullTotalPageFile. 
//            /// </summary>
//            public ulong ullAvailPageFile;

//            /// <summary>
//            /// Total size of the user mode portion of the virtual address space of the calling process, in bytes. 
//            /// </summary>
//            public ulong ullTotalVirtual;

//            /// <summary>
//            /// Size of unreserved and uncommitted memory in the user mode portion of the virtual address space of the calling process, in bytes. 
//            /// </summary>
//            public ulong ullAvailVirtual;

//            /// <summary>
//            /// Size of unreserved and uncommitted memory in the extended portion of the virtual address space of the calling process, in bytes. 
//            /// </summary>
//            public ulong ullAvailExtendedVirtual;

//            /// <summary>
//            /// Initializes a new instance of the <see cref="MEMORYSTATUSEX"/> class.
//            /// </summary>
//            public MEMORYSTATUSEX()
//            {
//                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
//            }
//        }
//        #endregion

//        #region SHChangeEventType

//        /// <summary>
//        /// Describes the event that has occurred. Typically, only one event is specified at a time. If more than one event is specified, the values contained in the dwItem1 and dwItem2 parameters must be the same, respectively, for all specified events. 
//        /// </summary>
//        public enum SHChangeEventType : uint
//        {
//            /// <summary>
//            /// The name of a nonfolder item has changed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the previous PIDL or name of the item. dwItem2 contains the new PIDL or name of the item. 
//            /// </summary>
//            SHCNE_RENAMEITEM = 0x00000001,

//            /// <summary>
//            /// A nonfolder item has been created. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the item that was created. dwItem2 is not used and should be NULL.
//            /// </summary>
//            SHCNE_CREATE = 0x00000002,

//            /// <summary>
//            /// A nonfolder item has been deleted. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the item that was deleted. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_DELETE = 0x00000004,

//            /// <summary>
//            /// A folder has been created. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the folder that was created. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_MKDIR = 0x00000008,

//            /// <summary>
//            /// A folder has been removed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the folder that was removed. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_RMDIR = 0x00000010,

//            /// <summary>
//            /// Storage media has been inserted into a drive. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive that contains the new media. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_MEDIAINSERTED = 0x00000020,

//            /// <summary>
//            /// Storage media has been removed from a drive. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive from which the media was removed. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_MEDIAREMOVED = 0x00000040,

//            /// <summary>
//            /// A drive has been removed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive that was removed. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_DRIVEREMOVED = 0x00000080,

//            /// <summary>
//            /// A drive has been added. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive that was added. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_DRIVEADD = 0x00000100,

//            /// <summary>
//            /// A folder on the local computer is being shared via the network. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the folder that is being shared. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_NETSHARE = 0x00000200,

//            /// <summary>
//            /// A folder on the local computer is no longer being shared via the network. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the folder that is no longer being shared. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_NETUNSHARE = 0x00000400,

//            /// <summary>
//            /// The attributes of an item or folder have changed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the item or folder that has changed. dwItem2 is not used and should be NULL.
//            /// </summary>
//            SHCNE_ATTRIBUTES = 0x00000800,

//            /// <summary>
//            /// The contents of an existing folder have changed, but the folder still exists and has not been renamed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the folder that has changed. dwItem2 is not used and should be NULL. If a folder has been created, deleted, or renamed, use SHCNE_MKDIR, SHCNE_RMDIR, or SHCNE_RENAMEFOLDER, respectively, instead. 
//            /// </summary>
//            SHCNE_UPDATEDIR = 0x00001000,

//            /// <summary>
//            /// An existing nonfolder item has changed, but the item still exists and has not been renamed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the item that has changed. dwItem2 is not used and should be NULL. If a nonfolder item has been created, deleted, or renamed, use SHCNE_CREATE, SHCNE_DELETE, or SHCNE_RENAMEITEM, respectively, instead. 
//            /// </summary>
//            SHCNE_UPDATEITEM = 0x00002000,

//            /// <summary>
//            /// The computer has disconnected from a server. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the server from which the computer was disconnected. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_SERVERDISCONNECT = 0x00004000,

//            /// <summary>
//            /// An image in the system image list has changed. SHCNF_DWORD must be specified in uFlags. 
//            /// </summary>
//            /// <remarks>
//            /// Windows NT/2000/XP: dwItem2 contains the index in the system image list that has changed. dwItem1 is not used and should be NULL.<br />
//            /// Windows 95/98: dwItem1 contains the index in the system image list that has changed. dwItem2 is not used and should be NULL.
//            /// </remarks>
//            SHCNE_UPDATEIMAGE = 0x00008000,

//            /// <summary>
//            /// A drive has been added and the Shell should create a new window for the drive. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive that was added. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_DRIVEADDGUI = 0x00010000,

//            /// <summary>
//            /// The name of a folder has changed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the previous pointer to an item identifier list (PIDL) or name of the folder. dwItem2 contains the new PIDL or name of the folder. 
//            /// </summary>
//            SHCNE_RENAMEFOLDER = 0x00020000,

//            /// <summary>
//            /// The amount of free space on a drive has changed. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1 contains the root of the drive on which the free space changed. dwItem2 is not used and should be NULL. 
//            /// </summary>
//            SHCNE_FREESPACE = 0x00040000,

//            /// <summary>
//            /// Not currently used. 
//            /// </summary>
//            SHCNE_EXTENDED_EVENT = 0x04000000,

//            /// <summary>
//            /// A file type association has changed. SHCNF_IDLIST must be specified in the uFlags parameter. dwItem1 and dwItem2 are not used and must be NULL. 
//            /// </summary>
//            SHCNE_ASSOCCHANGED = 0x08000000,

//            /// <summary>
//            /// Specifies a combination of all of the disk event identifiers. 
//            /// </summary>
//            SHCNE_DISKEVENTS = 0x0002381F,

//            /// <summary>
//            /// Specifies a combination of all of the global event identifiers. 
//            /// </summary>
//            SHCNE_GLOBALEVENTS = 0x0C0581E0,

//            /// <summary>
//            /// All events have occurred. 
//            /// </summary>
//            SHCNE_ALLEVENTS = 0x7FFFFFFF,

//            /// <summary>
//            /// The specified event occurred as a result of a system interrupt. As this value modifies other event values, it cannot be used alone.
//            /// </summary>
//            SHCNE_INTERRUPT = 0x80000000
//        }
//        #endregion

//    }
//}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/

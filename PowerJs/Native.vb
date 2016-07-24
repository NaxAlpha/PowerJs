Imports System.Runtime.ConstrainedExecution
Imports System.Runtime.InteropServices
Imports System.Security

Module Native

	<DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)>
	Private Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr,
	 ByVal dwSize As IntPtr, ByVal flAllocationType As AllocationType,
	 ByVal flProtect As MemoryProtection) As IntPtr
	End Function

	<DllImport("kernel32.dll")>
	Public Function OpenProcess(processAccess As ProcessAccessFlags, bInheritHandle As Boolean, processId As Integer) As IntPtr
	End Function

	<DllImport("kernel32.dll", SetLastError:=True)>
	<ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)>
	<SuppressUnmanagedCodeSecurity>
	Public Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

End Module
<Flags>
Public Enum ProcessAccessFlags As UInteger
	All = &H1F0FFF
	Terminate = &H1
	CreateThread = &H2
	VirtualMemoryOperation = &H8
	VirtualMemoryRead = &H10
	VirtualMemoryWrite = &H20
	DuplicateHandle = &H40
	CreateProcess = &H80
	SetQuota = &H100
	SetInformation = &H200
	QueryInformation = &H400
	QueryLimitedInformation = &H1000
	Synchronize = &H100000
End Enum

<Flags>
Public Enum AllocationType
	Commit = &H1000
	Reserve = &H2000
	Decommit = &H4000
	Release = &H8000
	Reset = &H80000
	Physical = &H400000
	TopDown = &H100000
	WriteWatch = &H200000
	LargePages = &H20000000
End Enum

<Flags>
Public Enum MemoryProtection
	Execute = &H10
	ExecuteRead = &H20
	ExecuteReadWrite = &H40
	ExecuteWriteCopy = &H80
	NoAccess = &H1
	[ReadOnly] = &H2
	ReadWrite = &H4
	WriteCopy = &H8
	GuardModifierflag = &H100
	NoCacheModifierflag = &H200
	WriteCombineModifierflag = &H400
End Enum

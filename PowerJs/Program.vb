Imports System.IO
Imports System.Linq.Expressions
Imports System.Reflection
Imports Jint

Module Program

	Private Engine As New Engine(Sub(c) c.AllowClr())

	Public Sub LoadType(type As Type)
		Dim methods = type.GetMethods(BindingFlags.Static Or BindingFlags.InvokeMethod Or BindingFlags.Public)
		For Each method In methods
			Dim params As New List(Of Type)
			For Each param In method.GetParameters()
				params.Add(param.ParameterType)
			Next
			params.Add(method.ReturnType)
			Dim deltype = Expression.GetDelegateType(params.ToArray())
			Engine.SetValue(method.Name, method.CreateDelegate(deltype))
		Next
	End Sub

	Private Sub Initialize()
		Console.Write("Loading <Core>... ")
		LoadType(GetType(Core))
		Console.ForegroundColor = ConsoleColor.Green
		Console.WriteLine("Core Loaded!")
		If Not Directory.Exists("lib") Then Return
		For Each library In Directory.GetFiles("lib")
			Engine.Execute(File.ReadAllText(library))
			Console.ForegroundColor = ConsoleColor.White
			Console.Write("Library: ")
			Console.ForegroundColor = ConsoleColor.Green
			Console.WriteLine(library)
		Next
	End Sub

	Sub Main()
		Initialize()
		While True
			Console.ForegroundColor = ConsoleColor.Yellow
			Console.Write(">> ")
			Console.ForegroundColor = ConsoleColor.White
			Dim ln = Console.ReadLine()
			Try
				Console.ForegroundColor = ConsoleColor.Gray
				Engine.Execute(ln)
			Catch ex As Exception
				Console.ForegroundColor = ConsoleColor.Blue
				Console.Write("[Error: ")
				Console.ForegroundColor = ConsoleColor.Red
				Console.Write(ex.ToString())
				Console.ForegroundColor = ConsoleColor.Blue
				Console.WriteLine("]")
			End Try
		End While
	End Sub

End Module

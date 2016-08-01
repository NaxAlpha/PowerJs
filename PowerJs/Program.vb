Imports System.IO
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Windows.Forms
Imports Jint
Imports PInvoke

Module Program

	Private Engine As Engine

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
		Console.Title = "PowerJs"
		Console.ForegroundColor = ConsoleColor.Magenta
		Console.WriteLine("============ PowerJs ============")
		Console.ForegroundColor = ConsoleColor.Cyan
		Console.WriteLine(".Net Javascript Experiment Engine")
		Console.ForegroundColor = ConsoleColor.White
		Console.WriteLine("Loading Core Assemblies... ")
		Dim loadedAssemblies As New List(Of Assembly)
		loadedAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies())
		For Each asmFile In Directory.GetFiles(Application.StartupPath, "*.dll")
			Try
				Dim asm = Assembly.LoadFile(asmFile)
				loadedAssemblies.Add(asm)
			Catch ex As Exception
				Console.ForegroundColor = ConsoleColor.DarkYellow
				Console.WriteLine("Unable to load assembly: " + Path.GetFileName(asmFile))
				Debug.WriteLine(ex)
			End Try
		Next
		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.WriteLine("Loaded Assemblies: " + loadedAssemblies.Count.ToString())
		Console.ForegroundColor = ConsoleColor.Yellow
		Console.WriteLine("Initializing Engine...")
		Engine = New Engine(Sub(c) c.AllowClr(loadedAssemblies.ToArray()))
		LoadType(GetType(Core))
		Console.ForegroundColor = ConsoleColor.Green
		Console.WriteLine("Done!")
		If Not Directory.Exists("lib") Then Return
		For Each library In Directory.GetFiles("lib")
			Engine.Execute(File.ReadAllText(library))
			Console.ForegroundColor = ConsoleColor.White
			Console.Write("Library: ")
			Console.ForegroundColor = ConsoleColor.Green
			Console.WriteLine(library)
		Next
	End Sub

	Private Sub Run()
		While True
			Console.ForegroundColor = ConsoleColor.Yellow
			Console.Write(">> ")
			Console.ForegroundColor = ConsoleColor.White
			Dim ln = Console.ReadLine()
			Try
				Console.ForegroundColor = ConsoleColor.Gray
				Engine.Execute(ln)
				Dim output = Engine.GetCompletionValue()
				Console.ForegroundColor = ConsoleColor.DarkGray
				Console.Write("<< ")
				Console.ForegroundColor = ConsoleColor.DarkMagenta
				Console.WriteLine(output.ToString())
			Catch ex As Exception
				Console.ForegroundColor = ConsoleColor.Blue
				Console.Write("[")
				Console.ForegroundColor = ConsoleColor.Red
				Console.Write(ex.ToString())
				Console.ForegroundColor = ConsoleColor.Blue
				Console.WriteLine("]")
			End Try
		End While
	End Sub

	Sub Main(ParamArray args() As String)
		Initialize()
		If args.Length = 0 Then
			Console.ForegroundColor = ConsoleColor.Black
			Console.BackgroundColor = ConsoleColor.White
			Console.Write("No Input File Supplied! Running Interactive Mode...")
			Console.BackgroundColor = ConsoleColor.Black
			Console.WriteLine()
			Run()
			Return
		End If

		For Each filePath In args
			Try
				Console.ForegroundColor = ConsoleColor.Black
				Console.BackgroundColor = ConsoleColor.White
				Console.Write("Executing Script: ")
				Console.ForegroundColor = ConsoleColor.Blue
				Console.Write(filePath)
				Console.BackgroundColor = ConsoleColor.Black
				Console.ForegroundColor = ConsoleColor.Gray
				Console.WriteLine()
				Dim fileData = File.ReadAllText(filePath)
				Engine.Execute(fileData)
			Catch ex As Exception
				Console.ForegroundColor = ConsoleColor.Blue
				Console.Write("[")
				Console.ForegroundColor = ConsoleColor.Red
				Console.Write(ex.ToString())
				Console.ForegroundColor = ConsoleColor.Blue
				Console.WriteLine("]")
			End Try
		Next
	End Sub

End Module

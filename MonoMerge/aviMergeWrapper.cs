using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;


namespace MonoMerge
{			
	public class aviMergeWrapper
	{		
		private static string processName = "avimerge";
		private static string processArguments = ""; // interactive
		private static Boolean isActive = false;
		private static ThreadStart job = null;
		private static Thread jobRunner = null;
		private static int timeStarted = 0;
		private static System.Diagnostics.Process processInstance;
		private static StreamWriter avimergeStreamWriter;
		private static int firstlines = 0;
		
		
		public aviMergeWrapper()
		{
			
		}	
		
		
		public static void merge(string inputs, string outputs)
		{
			aviMergeWrapper.processArguments = "-i " + inputs + " -o " + outputs;
			aviMergeWrapper.start();
		}
		
		
		public static void start()
		{			
			try
			{
				if(aviMergeWrapper.is_active()==false)
				{
					aviMergeWrapper.timeStarted = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
					aviMergeWrapper.isActive = true;
					aviMergeWrapper.job = new ThreadStart(process);
        			aviMergeWrapper.jobRunner = new Thread(aviMergeWrapper.job);
        			aviMergeWrapper.jobRunner.Start();						
				}										
			}
			catch(Exception t)
			{
				ExceptionOutputHandler.handle(t);			
			}
		}		
		
		
		public static void stop()
		{
			try
			{
				if(aviMergeWrapper.isActive==true)
				{
					aviMergeWrapper.processInstance.CloseMainWindow();
					aviMergeWrapper.processInstance.Dispose();
					aviMergeWrapper.processInstance.Close();		
					aviMergeWrapper.isActive = false;
					aviMergeWrapper.jobRunner.Abort();
				}
			}
			catch(Exception t)
			{
				ExceptionOutputHandler.handle(t);			
			}
		}
		
		
		public static Boolean is_active()
		{
			return aviMergeWrapper.isActive;	
		}
		
		
		public static int getTimeStarted()
		{
			return aviMergeWrapper.timeStarted;	
		}
		
		
		public static String getOutput()
		{
			return "";	
		}
		
				
		private static void process()
		{						
			if(aviMergeWrapper.isActive==true)
			{		
				Console.WriteLine(aviMergeWrapper.processName + " " + aviMergeWrapper.processArguments);
				aviMergeWrapper.processInstance = new Process();
        		aviMergeWrapper.processInstance.StartInfo.FileName = aviMergeWrapper.processName;
       			aviMergeWrapper.processInstance.StartInfo.Arguments = aviMergeWrapper.processArguments;				
       			aviMergeWrapper.processInstance.StartInfo.UseShellExecute = false;
        		aviMergeWrapper.processInstance.StartInfo.RedirectStandardOutput = true;
				aviMergeWrapper.processInstance.OutputDataReceived += HandleOutputDataReceived;
				aviMergeWrapper.processInstance.StartInfo.RedirectStandardInput = true;
				aviMergeWrapper.processInstance.Start();
				
				aviMergeWrapper.avimergeStreamWriter = aviMergeWrapper.processInstance.StandardInput;
				aviMergeWrapper.avimergeStreamWriter.AutoFlush = true;
				aviMergeWrapper.processInstance.BeginOutputReadLine();				   		
				aviMergeWrapper.processInstance.WaitForExit();
				aviMergeWrapper.isActive = false;
				Console.WriteLine("done");
			}								
		}

		protected static void HandleOutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if(aviMergeWrapper.firstlines < 10)
			{
				Console.WriteLine(e.Data.ToString());
				aviMergeWrapper.firstlines++;
				
				Console.WriteLine(aviMergeWrapper.firstlines);
			}
		}			
	}
}

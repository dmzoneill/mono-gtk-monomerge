using System;
using Gtk;
using Pango;
using MonoMerge;

public partial class MainWindow: Gtk.Window
{	
	private HBox[] boxes;
	private Label[] spacers;
	private Button[] addbuttons;
	private Button[] removebuttons;
	private FileChooserButton[] filechoosers;
	private string workingDirectory = "/home";
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.initial();		
	}
	
	
	public void initial()
	{
		
		this.addbuttons = new Button[11];
		this.addbuttons[1] = this.addButton1;
		this.addbuttons[2] = this.addButton2;
		this.addbuttons[3] = this.addButton3;
		this.addbuttons[4] = this.addButton4;
		this.addbuttons[5] = this.addButton5;
		this.addbuttons[6] = this.addButton6;
		this.addbuttons[7] = this.addButton7;
		this.addbuttons[8] = this.addButton8;
		this.addbuttons[9] = this.addButton9;
		this.addbuttons[10] = this.addButton10;
		
		
		this.removebuttons = new Button[11];
		this.removebuttons[1] = this.removeButton1;
		this.removebuttons[2] = this.removeButton2;
		this.removebuttons[3] = this.removeButton3;
		this.removebuttons[4] = this.removeButton4;
		this.removebuttons[5] = this.removeButton5;
		this.removebuttons[6] = this.removeButton6;
		this.removebuttons[7] = this.removeButton7;
		this.removebuttons[8] = this.removeButton8;
		this.removebuttons[9] = this.removeButton9;
		this.removebuttons[10] = this.removeButton10;		
		
		
		this.boxes = new HBox[11];
		this.boxes[1] = this.filebox1;
		this.boxes[2] = this.filebox2;
		this.boxes[3] = this.filebox3;
		this.boxes[4] = this.filebox4;
		this.boxes[5] = this.filebox5;
		this.boxes[6] = this.filebox6;
		this.boxes[7] = this.filebox7;
		this.boxes[8] = this.filebox8;
		this.boxes[9] = this.filebox9;
		this.boxes[10] = this.filebox10;
		
		for(int x = 3; x < this.boxes.Length; x++)
		{
			this.boxes[x].Visible = false;
		}		
		
		this.filechoosers = new FileChooserButton[11];
		this.filechoosers[1] = this.file1;
		this.filechoosers[2] = this.file2;
		this.filechoosers[3] = this.file3;
		this.filechoosers[4] = this.file4;
		this.filechoosers[5] = this.file5;
		this.filechoosers[6] = this.file6;
		this.filechoosers[7] = this.file7;
		this.filechoosers[8] = this.file8;
		this.filechoosers[9] = this.file9;
		this.filechoosers[10] = this.file10;
		
		
		this.spacers = new Label[4];
		this.spacers[0] = this.spacer4;
		this.spacers[1] = this.spacer5;
		this.spacers[2] = this.spacer6;
		this.spacers[3] = this.spacer7;
		
		for(int x = 0; x < this.spacers.Length; x++)
		{
			this.spacers[x].WidthRequest = 28;
			this.spacers[x].Text = " ";
		}		
		
		FontDescription desc = FontDescription.FromString("8 bold");
		
		this.inputFilesLabel.ModifyFont(desc);
		this.progressLabel.ModifyFont(desc);
		this.outputFileLabel.ModifyFont(desc);
		this.mergeOptionsLabel.ModifyFont(desc);
				
		this.Resize(550,300);
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void addFileButton (object sender, System.EventArgs e)
	{
		for(int x = 0; x < this.addbuttons.Length -1; x++)
		{
			if(sender.Equals(this.addbuttons[x]))
			{
				this.removebuttons[x].Sensitive = false;
				this.addbuttons[x].Sensitive = false;
				this.boxes[x + 1].Visible = true;
			}
		}	
		
		this.Resize(550,300);
	}

	protected virtual void removeFileButton (object sender, System.EventArgs e)
	{
		for(int x = 3; x < this.removebuttons.Length; x++)
		{
			if(sender.Equals(this.removebuttons[x]))
			{
				if(x > 3)
				{
					this.removebuttons[x -1].Sensitive = true;
				}
				this.addbuttons[x -1].Sensitive = true;
				this.boxes[x].Visible = false;
			}
		}	
		
		this.Resize(550,300);
	}

	protected virtual void OnMergeActionButtonClicked (object sender, System.EventArgs e)
	{
		string inputFiles = "";
		string outputFilename = "";
		
		for(int x =1; x < this.boxes.Length; x++)
		{
			if(this.boxes[x].Visible==true && this.filechoosers[x].Filename.Length>0)
			{
				inputFiles += "\"" + this.filechoosers[x].Filename + "\" ";					
			}
		}
		
		Gtk.FileChooserDialog fc= new Gtk.FileChooserDialog("Save merged files to", this, FileChooserAction.Save, "Cancel",ResponseType.Cancel, "Save",ResponseType.Accept);

		if (fc.Run() == (int)ResponseType.Accept) 
		{
			outputFilename = fc.Filename;
		}
		fc.Destroy();

		
		Console.WriteLine(inputFiles);
		Console.WriteLine(outputFilename);
		
		outputFilename = "\"" + outputFilename + "\"";
		
		aviMergeWrapper.merge(inputFiles, outputFilename);
		
	}

	protected virtual void OnCurrentFolderChanged (object sender, System.EventArgs e)
	{
		FileChooserButton but = (FileChooserButton) sender;
		this.workingDirectory = but.CurrentFolder;
		
		Console.WriteLine(this.workingDirectory);		
	}
}
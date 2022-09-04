using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DankRP;

[Library]
public partial class Jobs : Panel
{
	VirtualScrollPanel Canvas;
	Dictionary<string, Panel> jobCategories = new Dictionary<string, Panel>();
	public Jobs()
	{
		
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemHeight = 600;
		Canvas.Layout.ItemWidth = 1200;

		Canvas.OnCreateCell = ( cell, data ) =>
		{
			
			string jobCat = (string)data;
			var panel = cell.Add.Panel();
			

			panel.Style.Height = Length.Auto;
			panel.Style.Width = 1200;
			panel.Style.BackgroundColor = "rgba(35,35,35, 50%)";
			
			panel.Style.FlexGrow = 1;
			panel.Style.FlexDirection = FlexDirection.Column;
			panel.Style.Display = DisplayMode.Flex;

			var catHeader = new Panel();
			catHeader.Style.Height = 50;
			catHeader.Style.Width = 1200;
			Label lbl = new Label();

			lbl.Text = jobCat;
			lbl.Style.FontColor = "rgb(255,255,255)";
			lbl.Style.Padding = 5;
			catHeader.AddChild( lbl );
			catHeader.Style.BackgroundColor = "rgb(35,35,35)";
			panel.AddChild( catHeader );

			var JobList = new Panel();
			//JobList.Style.BackgroundColor = "rgb(75,0,100)";
			 
			
			JobList.Style.Display = DisplayMode.Flex;
			JobList.Style.FlexDirection = FlexDirection.Row;
			JobList.Style.FlexWrap = Wrap.Wrap;
			JobList.Style.Padding = 15;
			JobList.Style.Width = Length.Percent( 100 );
			panel.AddChild( JobList );
			
			
			if (jobCategories.ContainsKey(jobCat))
			{
				jobCategories[jobCat] = panel;
			} else
			{
				jobCategories.Add( jobCat, panel );
			}
			
			foreach ( Job job in Job.All.Values )
			{
				if ( job.JobCategory == jobCat )
				{
					Panel jPnl = JobList.Add.Panel();
					jPnl.Style.Width = 300;
					jPnl.Style.MarginLeft = 5;
					jPnl.Style.MarginRight = 5;
					jPnl.Style.MarginTop = 10;
					jPnl.Style.Height = 70;
					jPnl.Style.BackgroundColor = job.teamColor;
					jPnl.AddEventListener( "onclick", () => DBase.jobCMD( job.UniqueId ) );







					Label jlbl = new Label();
					jlbl.Style.FontColor = "rgb(255,255,255)";
					jlbl.Text = job.Title;
					jlbl.Style.FontSize = 30;
					jlbl.Style.Padding = 15;
					jPnl.AddChild( jlbl );


					Log.Trace( " > - " + job.Title );
				} else
				{
					Log.Trace( "> - " + job.Title + " is not category '" + data + "'" );
				}
			}
			//panel.Style.BackgroundColor = "rgb(35,35,35)";

		};


		//Job job = (Job)data;

		//var panel = cell.Add.Panel( "icon" );
		//panel.AddEventListener( "onclick", () => DankRP.jobCMD( job.UniqueId ) );
		//panel.Style.BackgroundColor = job.teamColor;
		//panel.Style.BackgroundImage = Texture.Load(Job.All[job].playerModel);

		List<string> cats = new List<string>();

		foreach ( Job job in Job.All.Values )
		{
			if ( !cats.Contains( job.JobCategory ) )
				cats.Add( job.JobCategory );
		}

		foreach (string cat in cats)
		{
			Canvas.AddItem( cat );
		}
	}
}

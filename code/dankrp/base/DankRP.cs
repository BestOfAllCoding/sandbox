using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace DankRP
{
	
	public partial class DBase : BaseNetworkable
	{
		public Data data;
		public static DBase instance;
		public static Config config;

		public DBase()
		{
			instance = this;


			this.data = new Data();
			Log.Trace( "Init DANKRP" );

			FileSystem.Data.CreateDirectory( "config/" );
			if ( !FileSystem.Data.FileExists( "config/config.json" ) )
			{
				config = new Config() { default_job = "CITIZEN" };
				FileSystem.Data.WriteJson( "config/config.json", config );
			}
			else
			{
				config = FileSystem.Data.ReadJson<Config>( "config/config.json" );
			}
			if ( !FileSystem.Data.FileExists( "config/jobs.json" ) )
			{
				FileSystem.Data.WriteJson( "config/jobs.json", new Dictionary<string, Job>() );
			}

			new xAdmin.Main();

			loadConfiguration();

			// Load Commands
			new Advert( "Advert", "advert [string]", "Advertise a roleplay message to the server." );
			new Ooc();
			new PrivateMessage();
			new Broadcast();

		}




		private void loadConfiguration()
		{
			if ( !Host.IsServer ) return;
		}

		public static DPlayer getPlayer( string id )
		{
			
			return DBase.instance.data.getPlayer( id );
		}
		 
		public static SandboxPlayer findOnlinePlayer( string search )
		{
			if ( DBase.instance.data.getPlayer( search ) != null )
				return DBase.instance.data.getPlayer( search ).Pawn;

			foreach ( DPlayer player in DBase.instance.data.players.Values )
			{
				Log.Trace( player.name.ToLower() + " : " + search.ToLower() );
				if ( player.name.ToLower().StartsWith( search.ToLower() ) )
				{
					return player.Pawn;
				}
			}


			return null;
		}
		public static Job getJob( string id )
		{
			//PrintLog( DankRP.gameData.jobs.Count.ToString() );
			if ( Job.All.ContainsKey( id ) )
				return Job.All[id];
			else
				return null;
		}

		public static void setJob( string steamid, Job job )
		{
			Client client = SandboxGame.connectedClients[steamid];
			SandboxPlayer player = (SandboxPlayer)SandboxGame.connectedClients[steamid].Pawn;
			Job previousJob = Job.All[player.playerData.job];
			player.SetModel( job.playerModel );
			player.Respawn();
			((SandboxPlayer)client.Pawn).playerData.job = job.UniqueId;
			Logger.PrintLog( client.Name + "(" + client.PlayerId + ") changed job from " + previousJob.Title + " to " + job.Title );

		}
		public static void setJob( string steamid, string JobID )
		{
			Job job = Job.All.GetValueOrDefault( JobID, null );
			if ( job != null )
			{
				setJob( steamid, job );
			}
			else
			{
				Logger.PrintError( "They was an error while setting job for steamid '" + steamid + "': Job ID was invalid." );
			}
		}


		[ConCmd.Server( "job" )]
		public static void jobCMD( string jobID )
		{
			setJobCMD( ConsoleSystem.Caller.PlayerId.ToString(), jobID );
		}

		[ConCmd.Admin( "setjob" )]
		public static void setJobCMD( string SteamID, string jobID )
		{
			if ( SandboxGame.connectedClients.ContainsKey( SteamID ) )
			{
				if ( Job.All.ContainsKey( jobID ) )
				{
					setJob( SteamID, jobID );
				}
				else
				{
					Logger.PrintError( "That job does not exist!" );
				}
			}
			else
			{
				Log.Error( "Invalid SteamID" );
			}
		}




	}
}

using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DankRP;

namespace DankRP
{
	public class Data
	{

		private string playerDataPath = "players/";
		public Dictionary<string, DPlayer> players = new Dictionary<string, DPlayer>();


		public Data()
		{
			if ( !FileSystem.Data.DirectoryExists( playerDataPath ) )
			{
				FileSystem.Data.CreateDirectory( playerDataPath );
			}
		}



		public DPlayer getPlayer( string id )
		{
			return players.GetValueOrDefault( id, null );
		}

		private DPlayer addPlayer( Client client )
		{
			players.Add( client.PlayerId.ToString(), new DPlayer( client ) );
			FileSystem.Data.WriteJson( playerDataPath + client.PlayerId.ToString() + ".txt", players[client.PlayerId.ToString()] );
			return players[client.PlayerId.ToString()];
		}

		public DPlayer loadPlayer( Client client )
		{
			if ( FileSystem.Data.FileExists( playerDataPath + client.PlayerId.ToString() + ".txt" ) )
			{
				players[client.PlayerId.ToString()] = FileSystem.Data.ReadJson<DPlayer>( playerDataPath + client.PlayerId.ToString() + ".txt" );
				return players[client.PlayerId.ToString()];
			}
			else
			{
				return addPlayer( client );
			}
		}

		[ConCmd.Server( "add_money" )]
		public static void addMoneyCMD( string id, int amount )
		{
			if ( DBase.instance.data.players.ContainsKey( id ) )
			{
				DBase.instance.data.players[id].money += amount;
				FileSystem.Data.WriteJson( "players/" + id + ".txt", DankRP.DBase.instance.data.players[id] );
			}
		}

		[ConCmd.Server( "get_money" )]
		public static void getMoneyCMD( string id )
		{
			Log.Trace( id );
			Log.Trace( DBase.getPlayer( id ).money );
			foreach ( DPlayer player in DankRP.DBase.instance.data.players.Values )
			{
				Log.Trace( player.id + ":" + player.name );
			}
		}
	}

}

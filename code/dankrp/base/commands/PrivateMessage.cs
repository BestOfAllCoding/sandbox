using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankRP
{
	public class PrivateMessage : Command
	{

		public PrivateMessage() : base( "Private Message", "pm <online_player> <string>", "Send a message to a player." )
		{

		}

		public override bool onCommand( SandboxPlayer player, string cmd, string[] args, string fullCmd )
		{
			if ( !base.onCommand( player, cmd, args, fullCmd ) )
				return false;
			SandboxPlayer ply = DBase.findOnlinePlayer( args[0] );

			List<string> m = new List<string>( args );
			m.RemoveAt( 0 );


			if ( ply != null )
			{
				DankChatBox.AddChatEntry( Sandbox.To.Single( ply ), player.Client.Name, String.Join( " ", m.ToArray() ), null, null, "| [PM FROM] ", DBase.getJob( player.playerData.job ).teamColor.Rgb, "rgb(252, 211, 3)" );
				DankChatBox.AddChatEntry( Sandbox.To.Single( ply ), ply.Client.Name, String.Join( " ", m.ToArray() ), null, null, "| [PM TO] ", DBase.getJob( ply.playerData.job ).teamColor.Rgb, "rgb(252, 211, 3)" );
			}
			return true;
		}

	}
}

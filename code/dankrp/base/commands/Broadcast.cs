using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankRP
{
	public class Broadcast : Command
	{
		public Broadcast() : base( "Broadcast", "broadcast <string>", "Broadcast a message to your city (Mayor only)." )
		{

		}

		public override bool onCommand( SandboxPlayer player, string cmd, string[] args, string fullCmd )
		{
			if ( !base.onCommand( player, cmd, args, fullCmd ) )
				return false;

			if ( !DBase.getJob( player.playerData.job ).IsMayor )
			{
				DankChatBox.AddChatEntry( Sandbox.To.Single( player ), "|", "Only the mayor can execute this command!", null, null, "#ff8a8a" );
				return false;
			}

			DankChatBox.announce( string.Join( " ", args ), "[Broadcast] " + player.Client.Name, "#ff8a8a" );
			return true;
		}
	}
}

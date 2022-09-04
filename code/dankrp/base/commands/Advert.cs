using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DankRP
{
	public class Advert : Command
	{
		public Advert( string cmdName, string cmdUsage, string description = "No description." ) : base( cmdName, cmdUsage, description )
		{

		}

		public override bool onCommand( SandboxPlayer player, string cmd, string[] args, string fullCmd )
		{
			if ( !base.onCommand( player, cmd, args, fullCmd ) )
				return false;
			DankChatBox.announce( string.Join( " ", args ), player.Client.Name, ((Color)DBase.getJob( player.playerData.job ).teamColor).Rgb, "[Advert]", "#ff8a8a" );
			return true;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankRP
{
	public class Ooc : Command
	{
		public Ooc() : base( "OOC", "ooc <string>", "Put a message in OOC (Out Of Character) chat." )
		{

		}

		public override bool onCommand( SandboxPlayer player, string cmd, string[] args, string fullCmd )
		{
			if ( !base.onCommand( player, cmd, args, fullCmd ) )
				return false;
			base.onCommand( player, cmd, args, fullCmd );
			DankChatBox.announce( string.Join( " ", args ), player.Client.Name, ((Color)DBase.getJob( player.playerData.job ).teamColor).Rgb, "[OOC]", "rgb( 0, 171, 142 )" );

			return true;
		}

	}
}

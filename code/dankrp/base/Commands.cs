using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;

namespace DankRP
{

	public class Command
	{
		public string cmdUsage { get; set; }
		public string cmdName { get; set; }
		public string description { get; set; }

		public string cmdStart = "";

		public Command( string cmdName, string cmdUsage, string description = "No description." )
		{

			this.cmdName = cmdName;
			this.cmdUsage = cmdUsage;
			this.description = description;

			if ( cmdUsage.Length > 0 )
			{
				Log.Trace( "DankRP - Checking CMD '" + this.cmdName + "' is valid..." );
				string[] c = cmdUsage.Split( " " );
				cmdStart = c[0];

				foreach ( string cmd in c )
				{

					if ( cmd != cmdStart )
					{
						Log.Trace( "  | Checking argument '" + cmd + "' is valid..." );
						string p1 = @"<[a-zA-Z_]+>";
						string p2 = @"\[[a-zA-Z_]+\]";
						// Create a Regex  
						Regex rg1 = new Regex( p1 );
						Regex rg2 = new Regex( p2 );
						if ( rg1.IsMatch( cmd ) || rg2.IsMatch( cmd ) )
						{
							string newC = cmd;
							newC = newC.Replace( "<", "" );
							newC = newC.Replace( ">", "" );

							newC = newC.Replace( "[", "" );
							newC = newC.Replace( "]", "" );



							if ( newC != "online_player" && newC != "string" && newC != "integer" && newC != "double" )
							{
								Logger.PrintError( "Command usage for command '" + this.cmdName + "' is not valid! The argument type '" + newC + "' is not valid!" );
								return;
							}
							Logger.PrintLog( "Loaded command '" + this.cmdName + "'" );
						}
						else
						{
							Logger.PrintError( "Command usage for command '" + this.cmdName + "' is not valid!" );
							return;
						}

					}
				}
			}

			Commands.commands.Add( cmdStart, this );
		}

		public virtual bool onCommand( SandboxPlayer player, string cmd, string[] a, string fullCmd )
		{
			List<string> args = new List<string>( a );
			string[] c = cmdUsage.Split( " " );
			int i = 0;
			foreach ( string arg in c )
			{
				if ( arg != c[0] )
				{
					string p1 = @"<[a-zA-Z_]+>";
					string p2 = @"\[[a-zA-Z_]+\]";
					// Create a Regex  
					Regex rg1 = new Regex( p1 );
					Regex rg2 = new Regex( p2 );
					if ( rg1.IsMatch( arg ) || rg2.IsMatch( arg ) )
					{
						string value = "";

						if ( args.Count >= i + 1 )
						{
							value = args[i];
						}


						string newC = arg;
						newC = newC.Replace( "<", "" );
						newC = newC.Replace( ">", "" );

						newC = newC.Replace( "[", "" );
						newC = newC.Replace( "]", "" );

						if ( newC == "integer" )
						{
							int number = 0;
							if ( !Int32.TryParse( value, out number ) )
							{
								DankChatBox.AddChatEntry( Sandbox.To.Single( player ), "|", "'" + value + "' is not a valid integer! Has to be a whole number!", null, null, "#ff8a8a" );
								return false;
							}
						}
						else if ( newC == "double" )
						{
							double number = 0;
							if ( !Double.TryParse( value, out number ) )
							{
								DankChatBox.AddChatEntry( Sandbox.To.Single( player ), "|", "'" + value + "' is not a valid number!", null, null, "#ff8a8a" );
								return false;
							}
						}
						else if ( newC == "online_player" )
						{
							SandboxPlayer p = DBase.findOnlinePlayer( value );
							if ( p == null )
							{
								DankChatBox.AddChatEntry( Sandbox.To.Single( player ), "|", "Could not find player \"" + value + "\"", null, null, "#ff8a8a" );
								return false;
							}
						}
					}
					else
					{
						Logger.PrintError( player.Client.Name + "(" + player.Client.PlayerId.ToString() + "): Something went wrong while executing command '" + cmd + "'" );
						return false;
					}
					i++;
				}
			}
			return true;
		}

	}

	public static class Commands
	{


		public static Dictionary<string, Command> commands = new Dictionary<string, Command>();


	}
}


using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using DankRP;

public partial class DankChatBox : Panel
{
	static DankChatBox Current;

	public Panel Canvas { get; protected set; }
	public TextEntry Input { get; protected set; }

	public DankChatBox()
	{
		Current = this;

		StyleSheet.Load( "/dankrp/plugins/chatbox/chatbox.scss" );

		Canvas = Add.Panel( "chat_canvas" );

		Input = Add.TextEntry( "" );
		Input.AddEventListener( "onsubmit", () => Submit() );
		Input.AddEventListener( "onblur", () => Close() );
		Input.AcceptsFocus = true;
		Input.AllowEmojiReplace = true;
	}

	void Open()
	{
		AddClass( "open" );
		Input.Focus();
	}

	void Close()
	{
		RemoveClass( "open" );
		Input.Blur();
	}

	public override void Tick()
	{
		base.Tick();

		if ( Sandbox.Input.Pressed( InputButton.Chat ) )
		{
			Open();
		}
	}

	void Submit()
	{
		Close();

		var msg = Input.Text.Trim();
		Input.Text = "";

		if ( string.IsNullOrWhiteSpace( msg ) )
			return;

		DankSay( msg );
	}

	public void AddEntry( string name, string message, string avatar, string prefix = null, string lobbyState = null, string nameC="#ffffff", string prefixC= "#c0fb2e" )
	{
		var e = Canvas.AddChild<DankChatEntry>();
		
		e.Message.Text = message;
		e.NameLabel.Text = name;
		e.NameLabel.Style.FontColor = nameC;
		e.PrefixLabel.Text = prefix;
		e.PrefixLabel.Style.FontColor = prefixC;
		
		e.Avatar.SetTexture( avatar );

		e.SetClass( "noname", string.IsNullOrEmpty( name ) );
		e.SetClass( "noprefix", string.IsNullOrEmpty( prefix ) );
		e.SetClass( "noavatar", string.IsNullOrEmpty( avatar ) );

		if ( lobbyState == "ready" || lobbyState == "staging" )
		{
			e.SetClass( "is-lobby", true );
		}
	}


	[ConCmd.Client( "rp_chat_add", CanBeCalledFromServer = true )]
	public static void AddChatEntry( string name, string message, string avatar = null, string lobbyState = null, string prefix = null, string nameC = "#ffffff", string prefixC = "#c0fb2e" )
	{

		Current?.AddEntry( name, message, avatar, prefix, lobbyState, nameC, prefixC );

		// Only log clientside if we're not the listen server host
		if ( !Global.IsListenServer )
		{
			Log.Info( $"{name}: {message}" );
		}
	}

	[ConCmd.Client( "rp_chat_addinfo", CanBeCalledFromServer = true )]
	public static void AddInformation( string message, string avatar = null )
	{
		Current?.AddEntry( null, message, avatar );
	}

	[ConCmd.Server( "rp_say" )]
	public static void DankSay( string message )
	{
		Assert.NotNull( ConsoleSystem.Caller );

		// todo - reject more stuff
		if ( message.Contains( '\n' ) || message.Contains( '\r' ) )
		{
			return;
		}


		if ( message.StartsWith( "/" ) || message.StartsWith( "!" ) )
		{
			string[] m = message.Split( ' ' );

			m[0] = m[0].Replace( "/", "" );
			m[0] = m[0].Replace( "!", "" );

			string[] args = new string[m.Length - 1];
			int i = 0;
			foreach ( string arg in m )
			{
				if ( arg != m[0] )
				{
					args[i] = arg;
					i++;
				}
			}

			if ( Commands.commands.ContainsKey( m[0] ) )
			{
				Commands.commands[m[0]].onCommand( (SandboxPlayer)ConsoleSystem.Caller.Pawn, m[0], args, message );
			}
			else
			{
				AddChatEntry( To.Single( ConsoleSystem.Caller.Pawn ), null, "Command '" + m[0] + "' does not exist!", null, null, "|", null, "rgb(252, 136, 136)" );
			}


			return;
		}

		Log.Info( $"{ConsoleSystem.Caller}: {message}" );
		AddChatEntry( To.Everyone, ConsoleSystem.Caller.Name, message, $"avatar:{ConsoleSystem.Caller.PlayerId}", null, null, DBase.getJob(((SandboxPlayer)ConsoleSystem.Caller.Pawn).playerData.job).teamColor.Rgb, null );
	}

	[ConCmd.Server( "rp_announce" )]
	public static void announce( string message, string name="", string nameColor=null, string prefix=null, string prefixColor = null )
	{
		AddChatEntry( To.Everyone, name, message, null, null, prefix, nameColor, prefixColor );
	}

}

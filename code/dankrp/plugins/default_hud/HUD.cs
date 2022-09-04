using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

[Library]  
public partial class DHud : HudEntity<RootPanel>
{

	public static RootPanel root;

	public DHud()
	{
		if ( !IsClient ) 
			return;
		root = RootPanel;
		RootPanel.StyleSheet.Load( "/dankrp/plugins/default_hud/bhud.scss" );
		
		
		RootPanel.AddChild<DankChatBox>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<BasicHud>();
		RootPanel.AddChild( new Notification( "testing lol." ) );
	}
}

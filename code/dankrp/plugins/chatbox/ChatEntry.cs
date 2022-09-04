using Sandbox.UI.Construct;
using Sandbox;
using Sandbox.UI;

public partial class DankChatEntry : Panel
{
	public Label NameLabel { get; internal set; }
	public Label Message { get; internal set; }

	public Label PrefixLabel { get; internal set; }
	public Image Avatar { get; internal set; }

	public RealTimeSince TimeSinceBorn = 0;

	public DankChatEntry()
	{
		Avatar = Add.Image();
		PrefixLabel = Add.Label( "Prefix", "prefix" );
		NameLabel = Add.Label( "Name", "name" );
		Message = Add.Label( "Message", "message" );
	}

	public override void Tick()
	{
		base.Tick();

		if ( TimeSinceBorn > 10 )
		{
			Delete();
		}
	}
}


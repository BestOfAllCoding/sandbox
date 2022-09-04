using Sandbox;
using Sandbox.UI;
using DankRP;

[UseTemplate]
public class BasicHud : Panel
{
	public Label health;
	private HudItem moneyItem;
	private HudItem healthItem;
	private HudItem jobItem;

	public BasicHud()
	{
		setup();
	}

	private void setup()
	{
		moneyItem = new HudItem( "$0", "rgb(148, 123, 0 )" );
		healthItem = new HudItem( "100%", "rgb(45,100,45 )" );
		jobItem = new HudItem( "Citizen", "rgb(45, 45, 125 )" );
		AddChild( jobItem );
		AddChild( healthItem );
		AddChild( moneyItem );
		
	}

	private int lastMoney = 0;
	private int ticks = 0;

	public override void Tick()
	{
		DPlayer player = null;
		if (Local.Pawn is SandboxPlayer)
		{
			player = ((SandboxPlayer)Local.Pawn).playerData;
		}

		if ( player.money != lastMoney && ticks < 50 )
		{
			ticks += 1;
			moneyItem.Style.BackgroundColor = "rgb(209, 174, 0)";
		} else
		{
			lastMoney = player.money;
			ticks = 0;
			moneyItem.Style.BackgroundColor = "rgb(148, 123, 0 )";
		}

		if (moneyItem != null && player != null)
		{
			moneyItem.itemText = "$" + player.money.ToString();
			healthItem.itemText = $"{Local.Pawn.Health.CeilToInt()}%";
			jobItem.itemText = DBase.getJob( player.job ).Title;
			jobItem.Style.BackgroundColor = DBase.getJob( player.job ).teamColor;
		}
		

	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();
		foreach(Panel panel in this.Children)
		{
			panel.Delete();
		}
		this.setup();
	}

}

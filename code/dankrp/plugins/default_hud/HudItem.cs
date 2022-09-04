using Sandbox;
using Sandbox.UI;

[UseTemplate]  
public class HudItem : Panel
{
	public Label health;
	public string itemText { get; set; }
	
	public HudItem(string text, Color? bgColor)
	{
		itemText = text;
		Style.BackgroundColor = bgColor;
	}
	public override void Tick()
	{

	}

}

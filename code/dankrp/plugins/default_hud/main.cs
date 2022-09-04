using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DankRP;

public class main : HudInterface
{
	DHud hud;
	public main()
	{
		hud = new DHud();
	}

	public void hide()
	{
		hud.LocalScale = 0;
	}

	public void show()
	{
		hud.LocalScale = 1;
	}

	public void showNotification( string notification )
	{
		
	}
}

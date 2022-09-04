using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class DPlayer : BaseNetworkable
{

	[Net] public string id { get; set; }
	[Net] public string name { get; set; }

	[Net] public int money { get; set; }

	[Net] public string job { get; set; }

	public SandboxPlayer Pawn { get; set; }

	public DPlayer(Client client)
	{
		this.id = client.PlayerId.ToString();
		this.name = client.Name;
		this.money = 0;
	}

	public DPlayer()
	{

	}


}


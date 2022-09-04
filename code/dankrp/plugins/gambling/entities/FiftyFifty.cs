using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

class FiftyFifty : Prop
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "entities/drone/drone.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DankRP;

namespace xAdmin {

	public class Main
	{
		public static DBase dankRP;
		public Main()
		{
			dankRP = DBase.instance;

			Log.Info( @"[XAdmin] Loading XAdmin. 🤨" );
			Log.Info( @"[XAdmin] Loaded along side DankRP. ✔️" );

		}
		
	}

}
	


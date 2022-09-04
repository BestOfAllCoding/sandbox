using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankRP
{
	public class Logger
	{
		public static void PrintError( string log )
		{
			Log.Error( "[DankRP] " + log );
		}


		public static void PrintLog( string log )
		{
			Log.Trace( "[DankRP] " + log );
		}
	}
}

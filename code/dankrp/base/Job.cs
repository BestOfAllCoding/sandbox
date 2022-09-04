using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace DankRP
{


	[GameResource( "Job", "job", "Create a new job that you will see in game and can switch to." )]
	public partial class Job : GameResource
	{

		// Access these statically with Clothing.All
		public static IReadOnlyDictionary<string, Job> All => _all;
		internal static Dictionary<string, Job> _all = new();

		protected override void PostLoad()
		{
			base.PostLoad();

			if ( !_all.ContainsKey( this.UniqueId ) )
				_all.Add( this.UniqueId, this );
			else
				Logger.PrintError( "Failed Loading Job: They is already a job with the same id '" + this.UniqueId + "'" );
		}

		public string JobCategory { get; set; }

		public string UniqueId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public Color teamColor { get; set; }

		[ResourceType( "vmdl" )]
		public string playerModel { get; set; }

		public bool IsMayor { get; set; }


	}
}

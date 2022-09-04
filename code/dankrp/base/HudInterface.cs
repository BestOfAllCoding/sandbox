using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankRP
{
	public interface HudInterface
	{
		public void showNotification( string notification );
		public void hide();
		public void show();

	}
}

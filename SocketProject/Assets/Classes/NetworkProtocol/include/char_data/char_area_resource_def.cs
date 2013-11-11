using System;

namespace twp
{
	namespace app
	{
		namespace unit
		{
			public class AreaResourceInfo //区域资源
			{

				public twp.app.resource.Resource[] resource = new twp.app.resource.Resource[(int)twp.app.city.EAreaDefLimit.LIMIT_MAX_AREA_COUNT];//资源
				public twp.app.resource.Engineer[] engineer = new twp.app.resource.Engineer[(int)twp.app.city.EAreaDefLimit.LIMIT_MAX_AREA_COUNT];//工程师

				public void FromBin (NetSocket.ByteArray bin)
				{

					for (UInt16 i = 0; i < (UInt16)twp.app.city.EAreaDefLimit.LIMIT_MAX_AREA_COUNT; ++i) {
						resource [i] = new twp.app.resource.Resource ();
						resource [i].FromBin (bin);
					}		
					
					for (UInt16 i = 0; i < (UInt16)twp.app.city.EAreaDefLimit.LIMIT_MAX_AREA_COUNT; ++i) {
						engineer [i] = new twp.app.resource.Engineer (); 
						engineer [i].FromBin (bin);
					}

				}
			};


		}
	}
}



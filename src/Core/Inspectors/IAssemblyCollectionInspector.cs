﻿using NDifference.Analysis;
using System.Collections.Generic;

namespace NDifference.Inspectors
{
	public interface IAssemblyCollectionInspector
	{
		void Inspect(IEnumerable<IAssemblyDiskInfo> first, IEnumerable<IAssemblyDiskInfo> second, IdentifiedChangeCollection changes);
	}
}

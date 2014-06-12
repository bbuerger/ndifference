﻿using NDifference.Analysis;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class DemoTypeInspector : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI00DEMO"; } }

		public string DisplayName { get { return "Demo"; } }

		public string Description { get { return "Demonstration used for getting report writing correct."; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			changes.Add(new IdentifiedChange
			{
				Description = "This is a demonstration type inspector-identified change",
				Priority = 1,// need value... for type taxonomy-like changes,
				Inspector = this.ShortCode

			});
		}
	}
}

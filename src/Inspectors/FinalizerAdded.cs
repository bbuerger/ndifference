﻿using NDifference.Analysis;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class FinalizerAdded : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI006"; } }

		public string DisplayName { get { return "Added Finalizer"; } }

		public string Description { get { return "Looks for new finalizer for a type."; } }
		
		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				&& second.Taxonomy == TypeTaxonomy.Class)
			{
				ClassDefinition cd1 = first as ClassDefinition;
				ClassDefinition cd2 = second as ClassDefinition;

				Finalizer wasDestructor = cd1.Finalizer;
				Finalizer nowDestructor = cd2.Finalizer;

				if (wasDestructor == null && nowDestructor != null)
				{
					changes.Add(new IdentifiedChange
					{
						Description = "Finalizer changes",
						Priority = 1,// need value... for type taxonomy-like changes,
						Inspector = this.ShortCode,
						Descriptor = new TextDescriptor { Name = "Finalizer added", Message = nowDestructor.ToCode() }
					});
				}
			}
		}
	}
}

﻿using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class MethodsChanged : ITypeInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TI_MAM"; } }

		public string DisplayName { get { return "Changed Methods"; } }

		public string Description { get { return "Checks for changes to existing methods"; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
			if (first.Taxonomy == TypeTaxonomy.Class
				|| first.Taxonomy == TypeTaxonomy.Interface
				|| second.Taxonomy == TypeTaxonomy.Class
				|| second.Taxonomy == TypeTaxonomy.Interface)
			{
				IReferenceTypeDefinition firstRef = first as IReferenceTypeDefinition;
				IReferenceTypeDefinition secondRef = second as IReferenceTypeDefinition;

				// look for non-overloaded methods that have a counterpart in the new version...
				foreach (var method in firstRef.AllMethods)
				{
					var counterpart = secondRef.AllMethods.FindExactMatchFor(method);

					if (counterpart != null)
					{
						if (method.IsVirtual != counterpart.IsVirtual)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsChanged,
									new NamedDeltaDescriptor
									{
										Name = method.IsVirtual ? "Method is no longer virtual" : "Method is now virtual",
										Was = method.ToCode(),
										IsNow = counterpart.ToCode()
									}));
						}
						else if (!method.IsAbstract && counterpart.IsAbstract)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsChanged,
									new NamedDeltaDescriptor
									{
										Name = "Method is now abstract",
										Was = method.ToCode(),
										IsNow = counterpart.ToCode()
									}));
						}

						if (method.IsStatic != counterpart.IsStatic)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsChanged,
									new NamedDeltaDescriptor
									{
										Name = method.IsStatic ? "Method is no longer static" : "Method is now static",
										Was = method.ToCode(),
										IsNow = counterpart.ToCode()
									}));
						}

						if (method.Accessibility != counterpart.Accessibility)
						{
							changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsChanged,
									new NamedDeltaDescriptor
									{
										Name = "Accessibility has changed",
										Was = method.Accessibility.ToDescription(),
										IsNow = counterpart.Accessibility.ToDescription()
									}));
						}

						MemberMethod mm = method as MemberMethod;
						MemberMethod cm = counterpart as MemberMethod;

						if (mm != null && cm != null)
						{
							if (mm.ReturnType != cm.ReturnType)
							{
								changes.Add(new IdentifiedChange(this, WellKnownTypeCategories.MethodsChanged,
										new NamedDeltaDescriptor
										{
											Name = "Return type has changed",
											Was = mm.ToCode(),
											IsNow = cm.ToCode()
										}));
							}
						}

						// other things have changed ???
					}
				}
			}
		}
	}

}

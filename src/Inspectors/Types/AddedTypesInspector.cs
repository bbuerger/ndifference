﻿using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.TypeSystem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Looking for types that have been added between first and second.
	/// </summary>
	public class AddedTypesInspector : ITypeCollectionInspector
	{
		public bool Enabled { get; set; }

		public string ShortCode { get { return "TCI_ATI"; } }

		public string DisplayName { get { return "New Types"; } }

		public string Description { get { return "Checks for new types in an assembly"; } }

		public void Inspect(ICombinedTypes types, IdentifiedChangeCollection changes)
		{
			Debug.Assert(types != null, "List of types cannot be null");
			Debug.Assert(changes != null, "Changes object cannot be null");

            var addedTypes = types.InLaterOnly;

            if (addedTypes.Any())
            {
                foreach (var added in addedTypes)
                {
                    changes.Add(new IdentifiedChange(this, WellKnownAssemblyCategories.AddedTypes, added.Second.FullName));
                }
            }
		
		}
	}
}

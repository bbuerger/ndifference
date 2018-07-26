﻿using NDifference.Analysis;
using NDifference.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Changes that apply at the top level - all assemblies.
	/// </summary>
	public static class WellKnownSummaryCategories
	{
		public static readonly Category RemovedAssemblies = new Category
		{
			Name = "Removed Assemblies",
			Description = "These assemblies were removed from the new version of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.RemovedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.Error
		};

		public static readonly Category ChangedAssemblies = new Category
		{
			Name = "Changed Assemblies",
			Description = "These assemblies have changed between the two versions of the product",
			Priority = new CategoryPriority(WellKnownChangePriorities.ChangedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.Warning
		};

        public static readonly Category PotentiallyChangedAssemblies = new Category
        {
            Name = "Potentially Changed Assemblies",
            Description = "These assemblies have failed a hash check between the two versions of the product",
            Priority = new CategoryPriority(WellKnownChangePriorities.PotentiallyChangedAssemblies),
            Headings = new string[] { "Assembly" },
            Severity = Severity.Warning
        };

        public static readonly Category AddedAssemblies = new Category
		{
			Name = "New Assemblies",
			Description = "These assemblies have been added to the new version",
			Priority = new CategoryPriority(WellKnownChangePriorities.AddedAssemblies),
			Headings = new string[] { "Assembly" },
            Severity = Severity.Information
		};

		//public static readonly Category UnchangedAssemblies = new Category 
		//{ 
		//	Name = "Changed Assemblies", 
		//	Description = "These assemblies have not changed between the two versions of the product",
		//	Priority = new CategoryPriority(WellKnownChangePriorities.UnchangedAssemblies) 
		//};
	}
}
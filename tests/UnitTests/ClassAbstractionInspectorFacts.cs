﻿using NDifference.Inspectors;
using NDifference.UnitTests.TestDataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class ClassAbstractionInspectorFacts
	{
		[Fact]
		public void ClassAbstractionInspector_Identifies_When_Class_Is_Made_Abstract()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsAbstract();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassAbstractionInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ClassAbstractionInspector_Ignores_When_Class_Is_Always_Abstract()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsAbstract();

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.IsAbstract();

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassAbstractionInspector())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void ClassAbstractionInspector_Ignores_When_Not_Abstract()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ClassAbstractionInspector())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}
	}
}

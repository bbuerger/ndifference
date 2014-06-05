﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	/// <summary>
	/// Inter document link - parent/child relationship.
	/// </summary>
	public interface IDocumentLink : IUniquelyIdentifiable
	{
		string LinkText { get; }

		string LinkUrl { get; }
	}
}

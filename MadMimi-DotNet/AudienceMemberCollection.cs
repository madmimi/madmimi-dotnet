using System;
using System.Collections.Generic;
using System.Data;

namespace MadMimi
{
	public class AudienceMemberCollection : List<AudienceMember>
	{
		public AudienceMemberCollection ()
		{
		}
		
		public DataTable ToDataTable() {
			// Turn this collection into a data table for import
		}
		
		private List<string> UniqueColumnNames() {
			// Return a list of unique column names in this collection
		}
	}
}


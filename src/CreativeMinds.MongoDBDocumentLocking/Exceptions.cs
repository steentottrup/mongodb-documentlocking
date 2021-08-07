using System;

namespace CreativeMinds.MongoDBDocumentLocking {

	public class NoDocumentLockedException : ApplicationException {
		public NoDocumentLockedException() : base("This document lock does not have a document locked") { }
	}
}

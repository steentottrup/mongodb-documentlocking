using System;
using MongoDB.Bson;

namespace MongoDB.DocumentLocking.Tests {

	public class TestClass : ILockableDocument {

		public String Name { get; set; }

		public ObjectId Id { get; set; }

		public ObjectId LockId { get; set; }
	}
}
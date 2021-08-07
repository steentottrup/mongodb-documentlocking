using CreativeMinds.MongoDBDocumentLocking;
using MongoDB.Bson;
using System;

namespace CreativeMinds.Tests.MongoDBDocumentLocking {

	public class TestClass : ILockableDocument {

		public String Name { get; set; }

		public ObjectId Id { get; set; }

		public ObjectId LockId { get; set; }
	}
}

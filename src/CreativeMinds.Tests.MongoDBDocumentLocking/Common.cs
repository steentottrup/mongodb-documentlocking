using MongoDB.Driver;
using System;

namespace CreativeMinds.Tests.MongoDBDocumentLocking {

	public static class Common {
		private const String mongoUrl = "mongodb://localhost/test";

		private static void CleanUp(IMongoDatabase db) {
			db.DropCollectionAsync("testclass");
		}

		public static IMongoCollection<TestClass> GetCollection() {
			MongoUrl url = new MongoUrl(mongoUrl);
			IMongoDatabase db = new MongoClient(url).GetDatabase(url.DatabaseName);

			CleanUp(db);

			return db.GetCollection<TestClass>("testclass");
		}
	}
}

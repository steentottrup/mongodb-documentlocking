using MongoDB.Driver;
using System;

namespace CreativeMinds.Tests.MongoDBDocumentLocking {

	public static class Common {
		private static String mongoUrl = $"mongodb://localhost/test{DateTime.UtcNow:yyyyMMddhhmmss}";

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

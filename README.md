# MongoDB.DocumentLocking

This is a small library that make it easy to get an exclusive lock on any MongoDB document.

Example:

	// Let's initiate the lock, here the input parameter is the id of the document
	using (DocumentLock<Document> docLock = new DocumentLock<Document>(collection, id)) {
		// Did we get a lock?
		if (docLock.Locked) {
			// Let's make the update we want to do!
			UpdateDefinition<Document> update = Builders<Document>
				.Update
				.Set(d => d.SomeProperty, newValue);

			// Push the update!
			docLock.Update(update);

			// We're done, release the lock!
			docLock.Release();
		}
	}

Another example, where we do not have the Id of the document we need (maybe we're locating it by status etc.).

	// Let's make a filter
	var filter = Builders<Document>
		.Filter
		.Eq(d => d.Status, Status.AwaitingSomething);
	// Let's initiate the lock, here the input parameter is a filter
	using (DocumentLock<Document> docLock = new DocumentLock<Document>(collection, filter)) {
		// ... same as before!
	}


// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
	using NUnit.Framework;
	using System;
	using FestivalManager.Entities;
	using System.Collections.Generic;

	[TestFixture]
	public class StageTests
	{
		private Stage stage;

		[SetUp]
		public void Setup()
		{
			stage = new Stage();
		}

		[Test]
		public void PerformensReturnsListAsReadOnlyCollection()
		{
			Assert.That(stage.Performers, Is.InstanceOf<IReadOnlyCollection<Performer>>());
		}

		[Test]
		public void AddPerformerMethodWorkProper()
		{
			Performer performer = new Performer("s", "sd", 37);

			stage.AddPerformer(performer);

			Assert.AreEqual(1, stage.Performers.Count);
		}

		[Test]
		public void AddPerformerMethodThrowExeptionWhenPerformerIsNull()
		{
			Performer performer = null;

			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(performer));
		}

		[Test]
		public void AddPerformerMethodThrowExeptionWhenPerformerAgeIsUnderEighteen()
		{
			Performer performer = new Performer("s", "sd", 3);

			Assert.Throws<ArgumentException>(() => stage.AddPerformer(performer));
		}

		[Test]
		public void AddSongMethodThrow()
		{
			Song song = null;

			Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));
		}

		[Test]
		public void AddSongMethodThrowSongDurationInMinutesUnderOne()
		{
			Song song = new Song("isb", TimeSpan.FromMinutes(0));

			Assert.Throws<ArgumentException>(() => stage.AddSong(song));
		}

		[Test]
		public void AddSongMethodWorkProper()
		{
			Song song = new Song("One", new TimeSpan(0, 6, 45));
			Performer performer = new Performer("Dimitar", "Antonov", 37);

			stage.AddSong(song);
			stage.AddPerformer(performer);

			string expected = "One (06:45) will be performed by Dimitar Antonov";
			string actual = stage.AddSongToPerformer("One", "Dimitar Antonov");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void AddSongToPerformerMethodWorkProper()
		{
			Performer performer = new Performer("Dimitar", "Haralampiev", 30);
			Song song = new Song("One", new TimeSpan(0, 6, 45));

			performer.SongList.Add(song);

			Assert.AreEqual(1, performer.SongList.Count);
		}

		[Test]
		public void PlayMethodTerurnProperValue()
		{
			Performer performer = new Performer("Dimitar", "Haralampiev", 30);
			Song song = new Song("One", new TimeSpan(0, 6, 45));

			stage.AddPerformer(performer);
			stage.AddSong(song);

			stage.AddSongToPerformer("One", "Dimitar Haralampiev");

			string expected = "1 performers played 1 songs";

			string actual = stage.Play();

			Assert.AreEqual(expected, actual);
		}
	}
}
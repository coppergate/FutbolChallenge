using System;
using System.Collections.Generic;

namespace FutbolChallengeDataRepository.Composites
{
	public class SeasonGroupComposite
	{
		public int Sequence {  get; set; }
		public string Name {  get; set; }
		public DateTime GroupStart {  get; set; }
		public DateTime GroupEnd {  get; set; }

		public IList<Game> Games { get; set; } = new List<Game>();

	}
}
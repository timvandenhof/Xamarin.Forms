using Sample.Services;

namespace Sample.Droid.Services
{
	public class TextService : ITextService
	{
		public string GetText() => "Hello From Android";
	}
}
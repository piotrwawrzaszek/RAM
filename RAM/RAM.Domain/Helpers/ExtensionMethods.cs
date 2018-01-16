namespace RAM.Domain.Helpers
{
	public static class ExtensionMethods
	{
		public static bool IsNullOrEmpty(this string s)
			=> string.IsNullOrEmpty(s);

		public static bool IsNullOrWhiteSpace(this string s)
			=> string.IsNullOrWhiteSpace(s);
	}
}

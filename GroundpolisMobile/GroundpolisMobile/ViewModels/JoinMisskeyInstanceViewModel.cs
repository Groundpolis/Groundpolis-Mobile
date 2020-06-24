using Reactive.Bindings;
using System;

namespace GroundpolisMobile.ViewModels
{
	public class JoinMisskeyInstanceViewModel
	{
		public ReactiveProperty<string> Url { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>("");
		public ReactiveProperty<Meta> Meta { get; } = new ReactiveProperty<Meta>(new Meta());

		public JoinMisskeyInstanceViewModel(JoinMisskeyInstance instance)
		{
			if (instance == null) throw new ArgumentNullException(nameof(instance));
			Url.Value = instance.Url;
			Name.Value = string.IsNullOrEmpty(instance.Meta.Name) ? instance.Url : instance.Meta.Name;
			var desc = string.IsNullOrEmpty(instance.Meta.Description) ? "説明はありません。" : instance.Meta.Description;
			Description.Value = desc;
			Meta.Value = instance.Meta;
		}
	}
}

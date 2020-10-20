using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Platform;

namespace Xamarin.Forms
{

	public class RendererToHandlerShim : Xamarin.Platform.IViewHandler, IAndroidViewHandler
	{
		private global::Android.Content.Context _context;

		internal IVisualElementRenderer VisualElementRenderer { get; private set; }

		public static IViewHandler CreateShim(object renderer)
		{
			if (renderer is IViewHandler handler)
				return handler;

			if (renderer is IVisualElementRenderer ivr)
				return new RendererToHandlerShim(ivr);

			return new RendererToHandlerShim(null);
		}

		public RendererToHandlerShim(IVisualElementRenderer visualElementRenderer)
		{
			if(visualElementRenderer != null)
				SetupRenderer(visualElementRenderer);
		}

		public void SetupRenderer(IVisualElementRenderer visualElementRenderer)
		{
			VisualElementRenderer = visualElementRenderer;
			VisualElementRenderer.ElementChanged += OnElementChanged;

			if (VisualElementRenderer.Element is IView view)
			{
				view.Handler = this;
				this.SetVirtualView(view);
			}
			else if (VisualElementRenderer.Element != null)
				throw new Exception($"{VisualElementRenderer.Element} must implement: {nameof(Xamarin.Platform.IView)}");
		}

		void OnElementChanged(object sender, VisualElementChangedEventArgs e)
		{
			if (e.OldElement is IView view)
				view.Handler = null;

			if (e.NewElement is IView newView)
			{
				newView.Handler = this;				
				this.SetVirtualView(newView);
			}
			else if (e.NewElement != null)
				throw new Exception($"{e.NewElement} must implement: {nameof(Xamarin.Platform.IView)}");

		}

		public object NativeView => VisualElementRenderer.View;

		public bool HasContainer
		{
			get;
			set;
		}

		global::Android.Views.View IAndroidViewHandler.View => throw new NotImplementedException();

		public Size GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var returnValue = VisualElementRenderer.GetDesiredSize((int)widthConstraint, (int)heightConstraint);
			return returnValue;
		}

		public void SetFrame(Rectangle frame)
		{
			var context = VisualElementRenderer.View.Context;
			var width = MeasureSpecFactory.MakeMeasureSpec((int)Platform.Android.ContextExtensions.ToPixels(context, frame.Width), global::Android.Views.MeasureSpecMode.Exactly);
			var height = MeasureSpecFactory.MakeMeasureSpec((int)Platform.Android.ContextExtensions.ToPixels(context, frame.Height), global::Android.Views.MeasureSpecMode.Exactly);

			VisualElementRenderer.View.Measure(width, height);
		}

		public void SetVirtualView(IView view)
		{
			if(VisualElementRenderer == null && _context != null)
			{
				var renderer = Internals.Registrar.Registered.GetHandlerForObject<IVisualElementRenderer>(view, _context)
										   ?? new Platform.Android.AppCompat.Platform.DefaultRenderer(_context);

				SetupRenderer(renderer);
			}

			if (VisualElementRenderer.Element != view)
				VisualElementRenderer.SetElement((VisualElement)view);
		}

		public void DisconnectHandler()
		{
			VisualElementRenderer.SetElement(null);
		}

		public void UpdateValue(string property)
		{
			if (property == "Frame")
			{
				SetFrame(VisualElementRenderer.Element.Bounds);
			}
		}

		void IAndroidViewHandler.SetContext(global::Android.Content.Context context)
		{
			_context = context;
		}
	}
}

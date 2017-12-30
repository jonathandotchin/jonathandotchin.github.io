In this post, we will look at an issue regarding Xamarin WebView and its ability to bind directly to HTML source.

# Background

In Xamarin Forms, if we want to display HTML content, we can use the WebView control. The content itself can be specified via an URL or HTML source code. In our case, we wanted to bind the HTML source to the XAML control as follow

``` html
<WebView HorizontalOptions="Fill" VerticalOptions="FillAndExpand">
    <WebView.Source>
        <HtmlWebViewSource Html="{Binding Content}" />
    </WebView.Source>
</WebView>
```

# Binding Issues

As it turns out, it simply did not work and when instead of binding, we would embed the content directly into the XAML as follow, it would work.

``` html
<WebView HorizontalOptions="Fill" VerticalOptions="FillAndExpand">
    <WebView.Source>
        <HtmlWebViewSource Html="&lt;html&gt;&lt;body&gt;&lt;p&gt;Hello World.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;" />
    </WebView.Source>
</WebView>
```

As for the model itself, there weren't anything special.

``` c#
    public class HtmlDocument : ObservableObject
    {
        private string content;

        public HtmlDocument()
        {
        }

        public string Content
        {
            get => this.content;
            set => this.SetProperty(ref this.content, value);
        }
    }
```

And yet, using the binding method would simply generate a blank WebView

# Solution

It turns out that the WebView cannot handle binding to a ```null``` value. By default, ```content``` is set to ```null``` since it is a ```string``` and therefore a reference type. In this case, the WebView didn't work. Initializing to a real default value solved the problem.

``` c#
    public class HtmlDocument : ObservableObject
    {
        private string content;

        public HtmlDocument()
        {
            this.Content = string.Empty;
        }

        public string Content
        {
            get => this.content;
            set => this.SetProperty(ref this.content, value);
        }
    }
```
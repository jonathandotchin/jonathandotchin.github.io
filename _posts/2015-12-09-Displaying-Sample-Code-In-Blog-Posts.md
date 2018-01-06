---
tags:
  - documentation
---

This post examines the different strategies at including sample code in a blog post.

# Background

When creating this blog, I wanted a simple way to include sample code in it. There weren't any elaborate criteria, only the following.

- Enable syntax highlighting
>> This makes the code easier to read and more 'beautiful'
- Keep the code with the blog post
>> Although, I would still test the code in a separate environment, I like the idea of having all the text in the same file / location. It makes reviewing and working on the post much more fluent.
- Provide the ability to copy and paste
>> This simply enable better sharing of the code.

# Strategies

In this section, we will look over the different strategies that could help us. I would add more as I find more.

## Images

This is actually straightforward. Simply take a snippet of your code as an image and include it in your blog.

![Sample Code]({{site.url}}/resources/2015-12-09-Displaying-Sample-Code-In-Blog-Posts\images/sample-code.PNG "Sample Code"){: .align-center}

### Pros

- Enables syntax highlighting
- Keeps the code with the post

### Cons

- Unable to copy and paste

## Gist

In essence, this uses [GitHub Gist](https://gist.github.com/) to embed the sample code in your post. Once a Gist is created, you simply need to copy and paste the generated `script` tag into your post.

``` javascript
<script src="https://gist.github.com/jonathandotchin/9106953ba91c7c39fa11375a6a293f01.js"></script>
```

becomes

<script src="https://gist.github.com/jonathandotchin/9106953ba91c7c39fa11375a6a293f01.js"></script>

### Pros

- Enables syntax highlighting
- Enables copy and paste

### Cons

- Code located elsewhere

## Syntax Highlighter

In essence, syntax highlighter examines a piece of code and hightlights it in consequence. This can be done on the client side with a library such as [highlight.js](https://highlightjs.org/) and [SyntaxHighlighter](http://alexgorbatchev.com/SyntaxHighlighter/) or on the server side with a library like [rouge](http://rouge.jneen.net/). In my case, since I am using the Jekyll blogging platform, it integrated the ability to [highlight code snippets](http://jekyllrb.com/docs/posts/) using either Pygments or Rouge.

``` c#

/// <summary>
/// Roots the frame navigation failed.
/// </summary>
/// <param name="sender">The sender.</param>
/// <param name="e">The <see cref="NavigationFailedEventArgs"/> instance containing the event data.</param>
private void RootFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
{
    // Code to execute if a navigation fails
    if (Debugger.IsAttached)
    {
        // A navigation has failed; break into the debugger
        Debugger.Break();
    }
}

```

### Pros

- Enables syntax highlighting
- Enables copy and paste
- Keeps the code with the post

### Cons

- None for now

# Further Reading

The blogging platform that I am using integrates Pygments or Rouge for syntax highlighting. If this is not your case, Alex Gorbatchev's [SyntaxHighlighter](http://alexgorbatchev.com/SyntaxHighlighter/manual/installation.html) would suit your needs.
In this post, we will examine the infamous 80 characters limit on a single line of code.

# Background

Today, the new guy asked me a simple question. He was wondering why we have a 120 characters line limit instead of the standard 80. Through our conversation, a couple of things surprised me.

- First, he was still taught the 80 characters line limit at school
- Second he didn't know why there was such a limit and I am not referring to the technical aspect of punch cards or terminal. More on that on [wikipedia](https://en.wikipedia.org/wiki/Characters_per_line). He was taught that 80 characters was the standard and it wasn't elaborated further.

The main reason we imposed a characters line limit is for improved readability by avoiding horizontal scrolling in all of our daily tasks such as coding, debugging, code reviews, etc.

# 120 Characters Line limit

In order to understand how we arrived at 120 characters line limit, we need to look at our work environment.

- Our programming language and framework of choice is C# / .NET
- Each developer have 2 24in LCD monitors

The C# language is rather verbose so we needed more characters per line. At first, we thought of using 180 characters. The debugging windows and other web browsers / reference windows can be on the other monitors. However, we realize that it was extremely cumbersome for code reviews and code merge. If we put the window on the same monitor, there would be either horizontal scrolling or auto wrap. If we put them in different monitor, there would be a bezel that would break the flow. Furthermore, some developers put their second monitor in portrait mode. In the end, we decrease the limit to 120 such that the code review / merge can occur on 1 monitor in a maximized window. 

# Conclusion

What's important to remember is understanding the reason behind a standard and to adapt that standard to your needs.
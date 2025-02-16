# StaticUsingExample

This sample shows two (2) different ways of varying behavior:
1. At compile-time by statically including a different implementation class depending on `Build` configuration
1. At runtime by changing the implementation of a static logging function, as desired
 - Note the calling code in `LibraryOne` is fully-unaware and doesn't care about what the actual implementation points to
 - This could be useful for unit test purposes and many other potential applications

 ## `Sunday, 2/16/25`

 - Setup automated builds for just this project, for now.
 - Added `yaml` formatter

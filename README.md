# Sweet Notes Front End

TODOs:

## Front End

- [x] Refactor all routes to constant files in an object
- [ ] Disable Login / Signup button after pressing
- [x] Switch `error-list` from an `id` to a `class`
- [x] Migrate Note textarea to use `useRenderGraphQLErrorList`
- [ ] Find a way to refactor CSS into more concise way
- [ ] Fix initial loading of Notes not showing
- [ ] Go through CSS codebase to ensure font sizes are set to rem and not to em
- [ ] Query SpecialSomeoneForUser and Notes fire up when changing tabs. Should not happen

## Back End

- [x] Extract UserEndpoint.cs into separate classes
- [ ] Look for opportunities to introduce a IDateTimeProvider and remove concrete implementations of DateTime
- [ ] Refactor exceptions to use Result Pattern with NuGet ErrorOr
- [ ] Replace Special Someone UniqueId with ID Generator + Base62
	- https://github.com/RobThree/IdGen
	- https://github.com/ghost1face/base62

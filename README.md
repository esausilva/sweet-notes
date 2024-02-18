# Sweet Notes Front End

TODOs:

## Front End

- [x] Refactor all routes to constant files in an object
- [ ] Disable Login / Signup button after pressing
- [ ] Switch `error-list` from an `id` to a `class`
- [ ] Migrate Note textarea to use `useRenderGraphQLErrorList`

## Back End

- [x] Extract UserEndpoint.cs into separate classes
- [ ] Look for opportunities to introduce a IDateTimeProvider and remove concrete implementations of DateTime
- [ ] Refactor exceptions to use Result Pattern with NuGet ErrorOr
- [ ] Replace Special Someone UniqueId with ID Generator + Base62
	- https://github.com/RobThree/IdGen
	- https://github.com/ghost1face/base62

# Sweet Notes

## Front End

- [x] Refactor all routes to constant files in an object
- [x] Disable Login / Signup button after pressing using react-query
- [x] Switch `error-list` from an `id` to a `class`
- [x] Migrate Note textarea to use `useRenderGraphQLErrorList`
- [ ] Find a way to refactor CSS into more concise way
- [x] Fix initial loading of Notes not showing
- [ ] Go through CSS codebase to ensure font sizes are set to rem and not to em
- [x] Query SpecialSomeoneForUser and Notes fire up when changing tabs. Should not happen
- [x] Call REST endpoint to get Special Someone's Name
- [ ] Reuse as much as possible date picker between **[slug].tsx** and **Notes.tsx**
- [x] Put a "refresh" button in `/ss/[slug]` route
- [x] Show toast message in `/ss/[slug]` after pressing refresh button but no new notes were found
- [x] Show toast message in admin section after creating new notes

## Back End

- [x] Extract UserEndpoint.cs into separate classes
- [ ] Look for opportunities to introduce a IDateTimeProvider and remove concrete implementations of DateTime
- [ ] Refactor exceptions to use Result Pattern with NuGet ErrorOr
- [ ] Replace Special Someone UniqueId with ID Generator + Base62
	- https://github.com/RobThree/IdGen
	- https://github.com/ghost1face/base62
- [x] Create REST endpoint to get Special Someone's Name
- [ ] Encrypt notes
- [x] Re-organize RestEndpoints. Separate folders, append Request to models that need it
- [ ] Implement rate limiter in `special-someone-name/{identifier}` route. NoteQueries.cs, SpecialSomeoneName.cs
- [ ] Implement some sort of captcha for spam user account creation

### Misc

https://webdesign.tutsplus.com/create-a-sticky-note-effect-in-5-easy-steps-with-css3-and-html5--net-13934t
https://www.npmjs.com/package/randomcolor
https://css-tricks.com/re-pleasing-color-palettes/
https://tanstack.com/query/latest/docs/framework/react/guides/window-focus-refetching

just use the queryClient directly

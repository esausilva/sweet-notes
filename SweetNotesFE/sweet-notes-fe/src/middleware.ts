import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

import { Routes, AUTH_COOKIE_NAME } from '@/constants';

export function middleware(request: NextRequest) {
  const authCookie = request.cookies.get(AUTH_COOKIE_NAME);
  const origin = request.nextUrl.origin;

  switch (request.nextUrl.pathname) {
    case '/':
    case `/${Routes.SIGNUP}`:
      if (authCookie)
        return NextResponse.redirect(`${origin}/${Routes.USER_ADMINISTRATION}`);
      break;

    case `/${Routes.USER}`:
      if (authCookie)
        return NextResponse.redirect(`${origin}/${Routes.USER_ADMINISTRATION}`);
      return NextResponse.redirect(origin);

    case `/${Routes.USER_ADMINISTRATION}`:
      if (authCookie === undefined) return NextResponse.redirect(origin);
      break;

    case `/${Routes.SPECIAL_SOMEONE}`:
      return NextResponse.redirect(`${origin}`);
  }
}

export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico (favicon file)
     */
    '/((?!api|_next/static|_next/image|favicon.ico).*)',
  ],
};

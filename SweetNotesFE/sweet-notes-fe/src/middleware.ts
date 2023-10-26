import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

import { AUTH_COOKIE_NAME, USER_ADMIN_ROUTE } from '@/constants';

export function middleware(request: NextRequest) {
  const authCookie = request.cookies.get(AUTH_COOKIE_NAME);
  const rootUrl = request.nextUrl.origin;

  switch (request.nextUrl.pathname) {
    case '/':
    case '/signup':
      if (authCookie) {
        return NextResponse.redirect(`${rootUrl}${USER_ADMIN_ROUTE}`);
      }
      break;
    case '/user':
      if (authCookie) {
        return NextResponse.redirect(`${rootUrl}${USER_ADMIN_ROUTE}`);
      }
      return NextResponse.redirect(rootUrl);
    case USER_ADMIN_ROUTE:
      if (authCookie === undefined) {
        return NextResponse.redirect(rootUrl);
      }
      break;
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

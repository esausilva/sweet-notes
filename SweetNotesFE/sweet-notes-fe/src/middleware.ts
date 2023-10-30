import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

import { USER_ADMIN_ROUTE } from '@/constants';

export function middleware(request: NextRequest) {
  const authCookie = request.cookies.get('SweetNotesAuthCookie');
  const origin = request.nextUrl.origin;

  switch (request.nextUrl.pathname) {
    case '/':
    case '/signup':
      if (authCookie)
        return NextResponse.redirect(`${origin}${USER_ADMIN_ROUTE}`);
      break;

    case '/user':
      if (authCookie)
        return NextResponse.redirect(`${origin}${USER_ADMIN_ROUTE}`);
      return NextResponse.redirect(origin);

    case USER_ADMIN_ROUTE:
      if (authCookie === undefined) return NextResponse.redirect(origin);
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

import type { GetServerSideProps } from 'next';

import { USER_ADMIN_ROUTE, AUTH_COOKIE_NAME } from '@/constants';

export const getServerSideProps = (async context => {
  const authCookie = context.req.cookies[AUTH_COOKIE_NAME];

  if (authCookie) {
    return {
      redirect: {
        destination: USER_ADMIN_ROUTE,
        permanent: false,
      },
    };
  }

  return {
    redirect: {
      destination: '/',
      permanent: false,
    },
  };

  return { props: {} };
}) satisfies GetServerSideProps<{}>;

export default function Index(): void {}

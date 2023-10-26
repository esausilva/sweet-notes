import type { GetServerSideProps } from 'next';
import Head from 'next/head';

import { AUTH_COOKIE_NAME } from '@/constants';

export const getServerSideProps = (async context => {
  const authCookie = context.req.cookies[AUTH_COOKIE_NAME];

  if (authCookie === undefined) {
    return {
      redirect: {
        destination: '/',
        permanent: false,
      },
    };
  }

  return { props: {} };
}) satisfies GetServerSideProps<{}>;

export default function UserAdmin(): JSX.Element {
  return (
    <div>
      <Head>
        <title>Admin | Sweet Notes</title>
      </Head>

      <h1>Admin</h1>
    </div>
  );
}

import Head from 'next/head';
import type { GetServerSideProps } from 'next';

import { UserAdminLayout } from '@/component/layouts/UserAdminLayout';
import { Header } from '@/component/administration/Header';
import { SpecialSomeones } from '@/component/administration/SpecialSomeones';
import { Notes } from '@/component/administration/Notes';
import { fetchGet } from '@/helper/fetchHelpers';
import { Me } from '@/types';
import { AUTH_COOKIE_NAME, Routes } from '@/constants';

import styles from './administration.module.scss';

export default function UserAdmin({ me }: { me: Me }): JSX.Element {
  return (
    <UserAdminLayout header={<Header me={me} />}>
      <Head>
        <title>Admin | Sweet Notes</title>
      </Head>
      <SpecialSomeones />
      <div className={styles.separator}></div>
      <Notes />
    </UserAdminLayout>
  );
}

export const getServerSideProps = (async context => {
  const cookie = context.req.cookies[AUTH_COOKIE_NAME];
  const response: Response = await fetchGet({
    route: Routes.USER_ME,
    headers: {
      cookie: `${AUTH_COOKIE_NAME}=${cookie}`,
    },
  });
  const me: Me = await response.json();

  return { props: { me } };
}) satisfies GetServerSideProps<{
  me: Me;
}>;

import Head from 'next/head';
import { useState, FormEvent, useEffect } from 'react';
import type { GetServerSideProps } from 'next';
import { useQuery } from '@tanstack/react-query';

import { UserAdminLayout } from '@/component/layouts/UserAdminLayout';
import { Header } from '@/component/administration/Header';
import { SpecialSomeones } from '@/component/administration/SpecialSomeones';
import { Notes } from '@/component/administration/Notes';
import { fetchGet } from '@/helper/fetchHelpers';
import { Me } from '@/types';
import { AUTH_COOKIE_NAME, Routes } from '@/constants';
import { graphql } from '@/gql/gql';
import { GraphQLClient } from '@/helper/graphQlClient';
import {
  GetSpecialSomeoneId,
  IsDoneLoadingSpecialSomeones,
} from '@/component/administration/helpers';

import styles from './administration.module.scss';

const specialSomeones = graphql(`
  query SpecialSomeone {
    specialSomeonesForUser(order: { firstName: ASC, lastName: ASC }) {
      id
      uniqueIdentifier
      firstName
      lastName
      nickname
    }
  }
`);

export default function UserAdmin({ me }: { me: Me }): JSX.Element {
  const queryResult = useQuery({
    queryKey: ['specialSomeones'],
    queryFn: async () => await GraphQLClient.request(specialSomeones, {}),
  });

  const [specialSomeone, setSpecialSomeone] = useState<{
    uniqueIdentifier: string;
    id: string;
  }>({ uniqueIdentifier: '', id: '' });

  const handleSelect = (event: FormEvent<HTMLSelectElement>) => {
    const uniqueIdentifier = event.currentTarget.value;
    setSpecialSomeone({
      uniqueIdentifier,
      id: GetSpecialSomeoneId(queryResult.data!, uniqueIdentifier),
    });
  };

  useEffect(() => {
    if (IsDoneLoadingSpecialSomeones(queryResult)) {
      const uniqueIdentifier =
        queryResult.data?.specialSomeonesForUser[0].uniqueIdentifier!;
      setSpecialSomeone({
        uniqueIdentifier,
        id: GetSpecialSomeoneId(queryResult.data!, uniqueIdentifier),
      });
    }
    return () => {};
  }, [queryResult.isLoading]);

  return (
    <UserAdminLayout header={<Header me={me} />}>
      <Head>
        <title>Admin | Sweet Notes</title>
      </Head>
      <SpecialSomeones
        queryResult={queryResult}
        specialSomeone={specialSomeone}
      >
        <div className={styles.specialSomeoneDropdownWrapper}>
          <select onChange={handleSelect}>
            {queryResult.data?.specialSomeonesForUser.map(ss => (
              <option
                value={`${ss.uniqueIdentifier}`}
                key={ss.uniqueIdentifier}
              >
                {`${ss.firstName} 
              ${ss?.nickname ? `"${ss.nickname}"` : ''} 
              ${ss.lastName}`}
              </option>
            ))}
          </select>
          <p className="annotation">
            <a href="#">Add Special Someone</a>
          </p>
        </div>
      </SpecialSomeones>
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

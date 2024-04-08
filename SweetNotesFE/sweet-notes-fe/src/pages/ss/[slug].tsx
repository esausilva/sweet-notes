import Head from 'next/head';
import type { GetServerSideProps } from 'next';
import { useState, useEffect } from 'react';
import { Inter } from 'next/font/google';
import { useRouter } from 'next/router';
import { useQuery } from '@tanstack/react-query';
import DatePicker from 'react-datepicker';

import { graphql } from '@/gql/gql';
import { GraphQLClient, FetchGet } from '@/helper/networkHelpers';
import { QueryKeys, Routes } from '@/constants';
import { FormatSpecialSomeoneName, ShouldReturnNotFound } from '@/helper/index';
import { SpecialSomeoneName, ApiErrorResponse } from '@/types';
import {
  GetFirstAndLastDaysOfTheMonth,
  FromUtcToLocal,
} from '@/component/administration/helpers';

const inter = Inter({ subsets: ['latin'] });
import styles from './slug.module.scss';
import 'react-datepicker/dist/react-datepicker.css';

const specialSomeoneNotes = graphql(`
  query Notes($uniqueIdentifier: String!, $from: DateTime!, $to: DateTime!) {
    notes(
      where: {
        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }
        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]
      }
      order: { createdUTC: DESC }
    ) {
      totalCount
      nodes {
        message
        createdUTC
      }
    }
  }
`);

export default function Page({
  specialSomeone,
}: {
  specialSomeone: SpecialSomeoneName | ApiErrorResponse;
}): JSX.Element {
  const router = useRouter();
  const [filterMonth, setFilterMonth] = useState<Date | null>(new Date());

  const { data, refetch, isLoading } = useQuery({
    queryKey: [QueryKeys.SPECIAL_SOMEONE_NOTES, router.query.slug],
    queryFn: async () =>
      await GraphQLClient.request(specialSomeoneNotes, {
        uniqueIdentifier: router.query.slug as string,
        from: GetFirstAndLastDaysOfTheMonth(filterMonth!).firstDay,
        to: GetFirstAndLastDaysOfTheMonth(filterMonth!).lastDay,
      }),
    enabled: !!router.query.slug,
    refetchOnWindowFocus: false,
  });

  useEffect(() => {
    refetch();
    return () => {};
  }, [filterMonth]);

  const SpecialSomeoneNameOrNotFound = (
    specialSomeone: SpecialSomeoneName | ApiErrorResponse,
  ): string => {
    return ShouldReturnNotFound(specialSomeone)
      ? `Special Someone Not Found`
      : `${FormatSpecialSomeoneName(
          specialSomeone as SpecialSomeoneName,
        )} Notes`;
  };

  return (
    <main className={`${inter.className} ${styles.main}`}>
      <Head>
        <title>{SpecialSomeoneNameOrNotFound(specialSomeone)}</title>
      </Head>

      <header className={styles.header}>
        <h1>{SpecialSomeoneNameOrNotFound(specialSomeone)}</h1>
      </header>

      <div className={styles.body}>
        <section className={styles.bodyTop}>
          <div className={styles.filter}>
            <p>Filter By: </p>
            <DatePicker
              selected={filterMonth}
              onChange={date => setFilterMonth(date)}
              dateFormat="MM/yyyy"
              showMonthYearPicker
            />
          </div>
          <p>Total Notes For This Month: {data?.notes?.totalCount ?? 0}</p>
        </section>

        <ul className={styles.notes}>
          {isLoading && <p className={styles.loading}>Loading...</p>}

          {ShouldReturnNotFound(specialSomeone) && !isLoading ? (
            <p className={styles.notFound}>
              Looks like the link you are trying to access is not valid!
            </p>
          ) : null}

          {data?.notes?.nodes?.map((note, idx) => (
            <li key={`note-${idx}`}>
              <span>{FromUtcToLocal(note.createdUTC)}</span> <br />
              {note.message}
            </li>
          ))}
        </ul>
      </div>
    </main>
  );
}

export const getServerSideProps = (async ({ query }) => {
  const response: Response = await FetchGet({
    route: `${Routes.SPECIAL_SOMEONE_NAME}/${query.slug}`,
  });
  const specialSomeone: SpecialSomeoneName | ApiErrorResponse =
    await response.json();

  return { props: { specialSomeone } };
}) satisfies GetServerSideProps<{
  specialSomeone: SpecialSomeoneName | ApiErrorResponse;
}>;

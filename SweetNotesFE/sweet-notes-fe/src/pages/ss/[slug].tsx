import Head from 'next/head';
import { useState, useEffect } from 'react';
import { Inter } from 'next/font/google';
import { useRouter } from 'next/router';
import { useQuery } from '@tanstack/react-query';
import DatePicker from 'react-datepicker';

import { graphql } from '@/gql/gql';
import { GraphQLClient } from '@/helper/graphQlClient';
import { QueryKeys } from '@/constants';
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

/*
TODO:

  Handle not found special someone
  Handle no notes
  Create Special Someone Name REST endpoint
*/

export default function Page(): JSX.Element {
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

  return (
    <main className={`${inter.className} ${styles.main}`}>
      <Head>
        <title>[[SPECIAL SOMEONE NAME]] | Notes</title>
      </Head>

      <header className={styles.header}>
        <h1>Special Someone Notes</h1>
      </header>

      <div className={styles.body}>
        <div className={styles.filter}>
          <p>Filter By: </p>
          <DatePicker
            selected={filterMonth}
            onChange={date => setFilterMonth(date)}
            dateFormat="MM/yyyy"
            showMonthYearPicker
          />
        </div>

        <ul className={styles.notes}>
          {isLoading && <p className={styles.loading}>Loading...</p>}
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

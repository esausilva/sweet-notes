import { JSX, useEffect, useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import DatePicker from 'react-datepicker';

import { graphql } from '@/gql/gql';
import { GraphQLClient } from '@/helper/networkHelpers';
import { QueryKeys } from '@/constants';
import { DeleteNote } from '@/component/administration/DeleteNote';
import {
  FromUtcToLocal,
  GetFirstAndLastDaysOfTheMonth,
} from '@/component/administration/helpers';

import styles from './Notes.module.scss';
import 'react-datepicker/dist/react-datepicker.css';

const specialSomeoneNotes = graphql(`
  query NotesAdmin(
    $uniqueIdentifier: String!
    $from: DateTime!
    $to: DateTime!
  ) {
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
        id
        createdUTC
      }
    }
  }
`);

export function Notes({
  specialSomeoneIdentifier,
}: {
  specialSomeoneIdentifier: string;
}): JSX.Element {
  const [filterMonth, setFilterMonth] = useState<Date | null>(new Date());

  const { data, refetch, isLoading } = useQuery({
    queryKey: [QueryKeys.SPECIAL_SOMEONE_NOTES, specialSomeoneIdentifier],
    queryFn: async () =>
      await GraphQLClient.request(specialSomeoneNotes, {
        uniqueIdentifier: specialSomeoneIdentifier,
        from: GetFirstAndLastDaysOfTheMonth(filterMonth!).firstDay,
        to: GetFirstAndLastDaysOfTheMonth(filterMonth!).lastDay,
      }),
    enabled: !!specialSomeoneIdentifier,
    refetchOnWindowFocus: false,
  });

  useEffect(() => {
    if (specialSomeoneIdentifier !== '') refetch();
    return () => {};
  }, [specialSomeoneIdentifier]);

  useEffect(() => {
    refetch();
    return () => {};
  }, [filterMonth]);

  return (
    <section className={styles.notes}>
      <h2>Notes</h2>

      <div className={styles.filter}>
        <p>Filter By: </p>
        <DatePicker
          selected={filterMonth}
          onChange={date => setFilterMonth(date)}
          dateFormat="MM/yyyy"
          showMonthYearPicker
        />
      </div>

      <p>Notes sent this month: {data?.notes?.totalCount ?? 0}</p>

      <hr className={styles.divider} />

      <ul className={styles.notesList}>
        {isLoading && <p>Loading...</p>}
        {data?.notes?.nodes?.map((note, idx) => (
          <li key={`note-${idx}`}>
            <div>
              <span>{FromUtcToLocal(note.createdUTC)}</span>
              <p>{note.message}</p>
            </div>
            <DeleteNote noteId={note.id} />
          </li>
        ))}
      </ul>
    </section>
  );
}

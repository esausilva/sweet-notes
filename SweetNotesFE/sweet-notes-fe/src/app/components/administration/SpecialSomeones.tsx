import { useState, useEffect, FormEvent } from 'react';
import { GraphQLClient } from 'graphql-request';
import { useQuery } from '@tanstack/react-query';
import { graphql } from '@/gql/gql';

import { GetSpecialSomeoneId } from '@/component/administration/helpers';

import styles from './SpecialSomeones.module.scss';

const graphQLClient = new GraphQLClient(
  `${process.env.NEXT_PUBLIC_BACKEND_ROOT_URI}/graphql`,
  {
    credentials: `include`,
    mode: `cors`,
  },
);

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

export function SpecialSomeones(): JSX.Element {
  const messageCountMax = 150;

  const { data, isLoading } = useQuery({
    queryKey: ['specialSomeones'],
    queryFn: async () => await graphQLClient.request(specialSomeones, {}),
  });

  const [specialSomeone, setSpecialSomeone] = useState<{
    uniqueIdentifier: string;
    id: string;
  }>({ uniqueIdentifier: '', id: '' });

  const [buttonDisabled, setButtonDisabled] = useState<boolean>(true);

  const [note, setNote] = useState<{ message: string; count: number }>({
    message: '',
    count: 0,
  });

  useEffect(() => {
    if (isLoading === false && data?.specialSomeonesForUser.length! > 0) {
      const uniqueIdentifier =
        data?.specialSomeonesForUser[0].uniqueIdentifier!;
      setSpecialSomeone({
        uniqueIdentifier,
        id: GetSpecialSomeoneId(data!, uniqueIdentifier),
      });
      setButtonDisabled(false);
    }
    return () => {};
  }, [isLoading]);

  const handleSelect = (event: FormEvent<HTMLSelectElement>) => {
    const uniqueIdentifier = event.currentTarget.value;
    setSpecialSomeone({
      uniqueIdentifier,
      id: GetSpecialSomeoneId(data!, uniqueIdentifier),
    });
  };

  const handleTextArea = (event: FormEvent<HTMLTextAreaElement>) => {
    const message = event.currentTarget.value;
    const count = event.currentTarget.value.length;
    setNote({ message, count });
  };

  return (
    <section className={styles.specialSomeones}>
      <h2>Special Someones</h2>
      <div className={styles.specialSomeoneDropdownWrapper}>
        <select onChange={handleSelect}>
          {data?.specialSomeonesForUser.map(ss => (
            <option value={`${ss.uniqueIdentifier}`} key={ss.uniqueIdentifier}>
              {`${ss.firstName} 
              ${ss?.nickname ? `"${ss.nickname}"` : ''} 
              ${ss.lastName}`}
            </option>
          ))}
        </select>
        <p className={styles.annotation}>
          <a href="#">Add Special Someone</a>
        </p>
      </div>
      <form className={styles.noteForm} action="">
        <label>Note</label>
        <textarea name="note" onChange={handleTextArea}></textarea>
        <p className={styles.annotation}>10 / {messageCountMax}</p>
        <button
          className="button--primary"
          type="submit"
          disabled={buttonDisabled}
        >
          Send Note
        </button>
      </form>
      <p>
        Share this unique link with your <em>Special Someone</em> to access your
        notes to them:
      </p>
      <p>
        <a href="#" target="_blank">
          https://domain.com/ss/{`${specialSomeone.uniqueIdentifier}`}
        </a>
      </p>
      <p className={styles.annotation}>
        Note: Anyone with the <em>Special Someone</em> link will be able to see
        the notes!
      </p>
    </section>
  );
}

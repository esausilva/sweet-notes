import { useState, useEffect, FormEvent } from 'react';
import { useQuery, useMutation } from '@tanstack/react-query';
import { graphql } from '@/gql/gql';

import { GetSpecialSomeoneId } from '@/component/administration/helpers';
import { GraphQLClient } from '@/helper/graphQlClient';

import styles from './SpecialSomeones.module.scss';

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

const createNote = graphql(`
  mutation createNote($message: String!, $specialSomeoneId: ID!) {
    createNote(
      input: { message: $message, specialSomeoneId: $specialSomeoneId }
    ) {
      note {
        id
      }
    }
  }
`);

export function SpecialSomeones(): JSX.Element {
  const messageCountMax = 150;

  const { data, isLoading } = useQuery({
    queryKey: ['specialSomeones'],
    queryFn: async () => await GraphQLClient.request(specialSomeones, {}),
  });

  const { isPending, mutate } = useMutation({
    mutationFn: async () =>
      await GraphQLClient.request(createNote, {
        message: note.message,
        specialSomeoneId: specialSomeone.id,
      }),
    onSuccess: (data, variables, context) => {
      setNote({ message: '', count: 0 });
      setFormErrors('');
    },
    onError: (error, variables, context) => {
      setFormErrors(error?.message.substring(0, error?.message.indexOf(':')));
    },
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

  const [formErrors, setFormErrors] = useState<string>('');

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
    setFormErrors('');
  };

  const handleChange = (event: FormEvent<HTMLTextAreaElement>) => {
    const message = event.currentTarget.value;
    const count = event.currentTarget.value.length;

    if (count <= messageCountMax) setNote({ message, count });
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    mutate();
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

      <form className={styles.noteForm} onSubmit={handleSubmit}>
        {formErrors ? <div id="error-list">{formErrors}</div> : <></>}

        <label>Note</label>
        <textarea
          name="note"
          onChange={handleChange}
          disabled={isPending}
          value={note.message}
        />
        <p className={styles.annotation}>
          {note.count} / {messageCountMax}
        </p>

        <button
          className="button--primary"
          type="submit"
          disabled={buttonDisabled || isPending}
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

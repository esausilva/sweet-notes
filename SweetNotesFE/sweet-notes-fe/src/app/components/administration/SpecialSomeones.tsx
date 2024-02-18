import { useState, useEffect, FormEvent } from 'react';
import { useMutation } from '@tanstack/react-query';

import { IsDoneLoadingSpecialSomeones } from '@/component/administration/helpers';
import { GraphQLClient } from '@/helper/graphQlClient';
import { graphql } from '@/gql/gql';
import { ISpecialSomeones } from '@/interfaces';

import styles from './SpecialSomeones.module.scss';

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

export function SpecialSomeones({
  children,
  specialSomeone,
  queryResult,
}: ISpecialSomeones): JSX.Element {
  const messageCountMax = 150;

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

  const [buttonDisabled, setButtonDisabled] = useState<boolean>(true);

  const [note, setNote] = useState<{ message: string; count: number }>({
    message: '',
    count: 0,
  });

  const [formErrors, setFormErrors] = useState<string>('');

  useEffect(() => {
    if (IsDoneLoadingSpecialSomeones(queryResult)) setButtonDisabled(false);
    else setButtonDisabled(true);

    return () => {};
  }, [queryResult.isLoading]);

  useEffect(() => {
    setFormErrors('');
    return () => {};
  }, [specialSomeone]);

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

      {children}

      <form className={styles.noteForm} onSubmit={handleSubmit}>
        {formErrors ? <div id="error-list">{formErrors}</div> : <></>}

        <label>Note</label>
        <textarea
          name="note"
          onChange={handleChange}
          disabled={isPending}
          value={note.message}
        />
        <p className="annotation">
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

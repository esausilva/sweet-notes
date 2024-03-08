import { useState, useEffect, FormEvent } from 'react';
import { useMutation, useQueryClient } from '@tanstack/react-query';

import { IsDoneLoadingSpecialSomeones } from '@/component/administration/helpers';
import { GraphQLClient } from '@/helper/graphQlClient';
import { graphql } from '@/gql/gql';
import { ISpecialSomeones } from '@/interfaces';
import { GraphQLErrorResponse, GraphQLError } from '@/types';
import { useRenderGraphQLErrorList } from '@/hook/useRenderGraphQLErrorList';
import { QueryKeys, MutationKeys } from '@/constants';

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
    mutationKey: [MutationKeys.CREATE_NOTE],
    mutationFn: async () =>
      await GraphQLClient.request(createNote, {
        message: note.message,
        specialSomeoneId: specialSomeone.id,
      }),
    onSuccess: (data, variables, context) => {
      setNote({ message: '', count: 0 });
      setFormErrors({
        errors: [],
      });
      queryClient.invalidateQueries({
        queryKey: [QueryKeys.SPECIAL_SOMEONE_NOTES],
      });
    },
    onError: (error: GraphQLErrorResponse, variables, context) => {
      setFormErrors(error.response);
    },
  });

  const [buttonDisabled, setButtonDisabled] = useState<boolean>(true);
  const queryClient = useQueryClient();

  const [note, setNote] = useState<{ message: string; count: number }>({
    message: '',
    count: 0,
  });

  const [formErrors, setFormErrors] = useState<GraphQLError>({
    errors: [],
  });

  useEffect(() => {
    if (IsDoneLoadingSpecialSomeones(queryResult)) setButtonDisabled(false);
    else setButtonDisabled(true);

    return () => {};
  }, [queryResult.isLoading]);

  useEffect(() => {
    setFormErrors({
      errors: [],
    });
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
        {useRenderGraphQLErrorList(formErrors)}

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

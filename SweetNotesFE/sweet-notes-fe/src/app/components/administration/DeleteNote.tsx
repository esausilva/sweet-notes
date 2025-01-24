import { useMutation, useQueryClient } from '@tanstack/react-query';

import { TrashIcon } from '@/resources/TrashIcon';
import { graphql } from '@/gql/gql';
import { GraphQLClient } from '@/helper/networkHelpers';
import { MutationKeys, QueryKeys } from '@/constants';
import { GraphQLErrorResponse } from '@/types';
import { useSuccessToast, useErrorToast } from '@/hook/useToast';

import styles from './DeleteNote.module.scss';

const deleteNote = graphql(`
  mutation deleteNote($id: ID!) {
    deleteNote(input: { id: $id }) {
      note {
        id
      }
    }
  }
`);

export function DeleteNote({ noteId }: { noteId: string }): JSX.Element {
  const queryClient = useQueryClient();

  const { isPending, mutate } = useMutation({
    mutationKey: [MutationKeys.DELETE_NOTE],
    mutationFn: async () =>
      await GraphQLClient.request(deleteNote, {
        id: noteId,
      }),
    onSuccess: (data, variables, context) => {
      useSuccessToast('Note Successfully Deleted');
      queryClient.invalidateQueries({
        queryKey: [QueryKeys.SPECIAL_SOMEONE_NOTES],
      });
    },
    onError: (error: GraphQLErrorResponse) => {
      useErrorToast(
        'Something went wrong in trying to delete the note. We have refreshed the notes list.',
      );
      queryClient.invalidateQueries({
        queryKey: [QueryKeys.SPECIAL_SOMEONE_NOTES],
      });
    },
  });

  const handleClick = () => {
    mutate();
  };

  return <TrashIcon className={styles.trash} onClick={handleClick} />;
}

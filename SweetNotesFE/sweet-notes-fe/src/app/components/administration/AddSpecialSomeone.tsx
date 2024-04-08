import { useState, LegacyRef, useReducer, FormEvent } from 'react';
import { Modal } from 'react-responsive-modal';
import { useMutation, useQueryClient } from '@tanstack/react-query';

import { graphql } from '@/gql/gql';
import { GraphQLClient } from '@/helper/networkHelpers';
import {
  CreateSpecialSomeoneForm,
  FormFieldData,
  GraphQLErrorResponse,
  GraphQLError,
} from '@/types';
import { useRenderGraphQLErrorList } from '@/hook/useRenderGraphQLErrorList';
import { IAddSpecialSomeone } from '@/interfaces';
import styles from './AddSpecialSomeone.module.scss';
import { QueryKeys, MutationKeys } from '@/constants';

const createSpecialSomeone = graphql(`
  mutation createSpecialSomeone(
    $firstName: String!
    $nickName: String
    $lastName: String!
  ) {
    createSpecialSomeone(
      input: { firstName: $firstName, lastName: $lastName, nickName: $nickName }
    ) {
      specialSomeone {
        id
      }
    }
  }
`);

const initialFormState: CreateSpecialSomeoneForm = {
  firstName: '',
  lastName: '',
  nickName: '',
};

const formReducer = (
  state: CreateSpecialSomeoneForm,
  { name, value }: FormFieldData,
): CreateSpecialSomeoneForm => {
  return {
    ...state,
    [name]: value,
  };
};

export function AddSpecialSomeone({
  buttonRef,
}: IAddSpecialSomeone): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [buttonDisabled, setButtonDisabled] = useState<boolean>(false);
  const [formErrors, setFormErrors] = useState<GraphQLError>({
    errors: [],
  });
  const queryClient = useQueryClient();

  const { isPending, mutate, isSuccess } = useMutation({
    mutationKey: [MutationKeys.CREATE_SPECIAL_SOMEONE],
    mutationFn: async () =>
      await GraphQLClient.request(createSpecialSomeone, {
        firstName: formData.firstName,
        nickName: formData.nickName,
        lastName: formData.lastName,
      }),
    onSuccess: (data, variables, context) => {
      clearForm();
      setFormErrors({
        errors: [],
      });
      setButtonDisabled(false);
      queryClient.invalidateQueries({ queryKey: [QueryKeys.SPECIAL_SOMEONES] });
    },
    onError: (error: GraphQLErrorResponse) => {
      setFormErrors(error.response);
      setButtonDisabled(false);
    },
  });

  const clearForm = () => {
    dispatch({ name: 'firstName', value: '' });
    dispatch({ name: 'nickName', value: '' });
    dispatch({ name: 'lastName', value: '' });
  };

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    mutate();
  };

  const onOpenModal = () => setOpenModal(true);
  const onCloseModal = () => {
    setOpenModal(false);
    clearForm();
  };

  return (
    <>
      <Modal
        open={openModal}
        onClose={onCloseModal}
        center
        classNames={{
          modal: styles.modal,
        }}
      >
        <h3 className={styles.title}>Add a Special Someone</h3>
        <form className={styles.addSpecialSomeoneForm} onSubmit={handleSubmit}>
          {useRenderGraphQLErrorList(formErrors)}

          {isSuccess ? (
            <span className={styles.success}>Successfully added!</span>
          ) : null}

          <label htmlFor="firstName">First Name</label>
          <input
            type="text"
            name="firstName"
            onChange={handleChange}
            value={formData.firstName}
            disabled={isPending}
          />

          <label htmlFor="nickName">Nickname</label>
          <input
            type="text"
            name="nickName"
            onChange={handleChange}
            value={formData.nickName}
            disabled={isPending}
          />

          <label htmlFor="lastName">Last Name</label>
          <input
            type="text"
            name="lastName"
            onChange={handleChange}
            value={formData.lastName}
            disabled={isPending}
          />

          <button
            className="button--primary"
            type="submit"
            disabled={buttonDisabled || isPending}
          >
            Add
          </button>
        </form>
      </Modal>
      <button onClick={onOpenModal} ref={buttonRef} hidden={true}></button>
    </>
  );
}

import { FormEvent, useReducer, useState } from 'react';
import type { GetServerSideProps } from 'next';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import Head from 'next/head';

import { UserAdminLayout } from '@/component/layouts/UserAdminLayout';
import { Header } from '@/component/administration/Header';
import { AUTH_COOKIE_NAME, Routes, ApiRootUris } from '@/constants';
import { FetchGet, FetchPost } from '@/helper/networkHelpers';
import { useRenderErrorList } from '@/hook/useRenderErrorList';
import { useSuccessToast, useErrorToast } from '@/hook/useToast';
import {
  Me,
  UpdatePasswordForm,
  FormFieldData,
  ApiError,
  ApiErrorResponse,
} from '@/types';

import styles from './updatePassword.module.scss';

const initialFormState: UpdatePasswordForm = {
  currentPassword: '',
  newPassword: '',
};

const formReducer = (
  state: UpdatePasswordForm,
  { name, value }: FormFieldData,
): UpdatePasswordForm => {
  return {
    ...state,
    [name]: value,
  };
};

export default function UpdatePassword({ me }: { me: Me }): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);
  const [errors, setErrors] = useState<ApiError>({});

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    console.log({ formData });
    mutate();
  };

  const { isPending, mutate } = useMutation({
    mutationFn: async () =>
      await FetchPost({
        route: Routes.USER_UPDATE_PASSWORD,
        body: {
          currentPassword: formData.currentPassword,
          newPassword: formData.newPassword,
        },
        fetchOptions: {
          credentials: `include`,
        },
      }),
    onSuccess: async (data, variables, context) => {
      const responseBody = (await data.json()) as ApiErrorResponse;

      if (data.status === 500) {
        useErrorToast(`${responseBody.title} Please contact support.`);
        return;
      }

      if (data.status !== 200) {
        setErrors(responseBody.errors!);
        clearForm();
        return;
      }

      useSuccessToast(`Your password has been updated!`);
      clearForm();
      setErrors({});
    },
    onError: (error: ApiErrorResponse) => {
      useErrorToast(`${error.title} Please contact support.`);
    },
  });

  const clearForm = () => {
    dispatch({ name: 'currentPassword', value: '' });
    dispatch({ name: 'newPassword', value: '' });
  };

  return (
    <UserAdminLayout header={<Header me={me} />}>
      <Head>
        <title>Admin | Update Password</title>
      </Head>

      <section>
        <h2>Update Password</h2>

        <form id={styles.updatePasswordForm} onSubmit={handleSubmit}>
          <>{useRenderErrorList(errors)}</>

          <label htmlFor="email">Current Password</label>
          <input
            type="password"
            name="currentPassword"
            onChange={handleChange}
            disabled={isPending}
            value={formData.currentPassword}
          />

          <label htmlFor="password">New Password</label>
          <input
            type="password"
            name="newPassword"
            onChange={handleChange}
            disabled={isPending}
            value={formData.newPassword}
          />

          <button
            className="button--primary"
            type="submit"
            disabled={isPending}
          >
            Update Password
          </button>
        </form>
      </section>
    </UserAdminLayout>
  );
}

export const getServerSideProps = (async ({ req }) => {
  const cookie = req.cookies[AUTH_COOKIE_NAME];
  const response: Response = await FetchGet({
    route: Routes.USER_ME,
    rootUri: ApiRootUris.FOR_SERVER_SIDE,
    headers: {
      cookie: `${AUTH_COOKIE_NAME}=${cookie}`,
    },
  });
  const me: Me = await response.json();

  return { props: { me } };
}) satisfies GetServerSideProps<{
  me: Me;
}>;

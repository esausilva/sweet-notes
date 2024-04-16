import { FormEvent, useReducer, useState } from 'react';
import { useRouter } from 'next/navigation';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '@/component/layouts/MainEntryLayout';
import { SignupForm, FormFieldData, ApiError, ApiErrorResponse } from '@/types';
import { useAuthService } from '@/service/authService';
import { useRenderErrorList } from '@/hook/useRenderErrorList';
import { Routes, MutationKeys } from '@/constants';
import { useErrorToast } from '@/hook/useToast';

import styles from './index.module.scss';

const initialFormState: SignupForm = {
  firstName: '',
  lastName: '',
  email: '',
  password: '',
};

const formReducer = (
  state: SignupForm,
  { name, value }: FormFieldData,
): SignupForm => {
  return {
    ...state,
    [name]: value,
  };
};

export default function Signup(): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);
  const [errors, setErrors] = useState<ApiError>({});
  const router = useRouter();

  const { mutateAsync, isPending } = useAuthService(
    Routes.USER_SIGNUP,
    MutationKeys.SIGNUP,
    {
      firstName: formData.firstName,
      lastName: formData.lastName,
      emailAddress: formData.email,
      password: formData.password,
    },
  );

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (
    event: FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    const data = await mutateAsync();
    const responseBody = (await data.json()) as ApiErrorResponse;

    if (data.status === 200) router.push(Routes.USER_ADMINISTRATION);

    if (responseBody.errors) setErrors(responseBody.errors);

    if (data.status === 500)
      useErrorToast(`${responseBody.title} Please contact support.`);
  };

  return (
    <MainLayout>
      <Head>
        <title>Create an Account | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Signup</h1>

          <>{useRenderErrorList(errors)}</>

          <label htmlFor="firstName">First Name</label>
          <input
            type="text"
            name="firstName"
            onChange={handleChange}
            disabled={isPending}
            value={formData.firstName}
          />

          <label htmlFor="lastName">Last Name</label>
          <input
            type="text"
            name="lastName"
            onChange={handleChange}
            disabled={isPending}
            value={formData.lastName}
          />

          <label htmlFor="email">Email</label>
          <input
            type="text"
            name="email"
            onChange={handleChange}
            disabled={isPending}
            value={formData.email}
          />

          <label htmlFor="password">Password</label>
          <input
            type="password"
            name="password"
            onChange={handleChange}
            disabled={isPending}
            value={formData.password}
          />

          <button
            className="button--primary"
            type="submit"
            disabled={isPending}
          >
            Signup
          </button>
        </form>
        <Link href="/">Already have an account?</Link>
      </section>
    </MainLayout>
  );
}

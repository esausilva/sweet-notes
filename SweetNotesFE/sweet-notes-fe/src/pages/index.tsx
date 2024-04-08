import { FormEvent, useReducer, useState } from 'react';
import { useRouter } from 'next/navigation';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '@/component/layouts/MainEntryLayout';
import { FormFieldData, LoginForm, ApiError } from '@/types';
import { Authenticate } from '@/service/authService';
import { useRenderErrorList } from '@/hook/useRenderErrorList';
import { Routes } from '@/constants';

import styles from './index.module.scss';

const initialFormState: LoginForm = {
  email: '',
  password: '',
};

const formReducer = (
  state: LoginForm,
  { name, value }: FormFieldData,
): LoginForm => {
  return {
    ...state,
    [name]: value,
  };
};

export default function Index(): JSX.Element {
  const [formData, dispatch] = useReducer(formReducer, initialFormState);
  const [errors, setErrors] = useState<ApiError>({});
  const router = useRouter();

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    var result = await Authenticate(Routes.USER_LOGIN, {
      emailAddress: formData.email,
      password: formData.password,
    });

    if (result.status === 200) router.push(Routes.USER_ADMINISTRATION);
    if (result.errors) setErrors(result.errors);
  };

  return (
    <MainLayout>
      <Head>
        <title>Login | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Login</h1>

          <>{useRenderErrorList(errors)}</>

          <label htmlFor="email">Email</label>
          <input type="text" name="email" onChange={handleChange} />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" onChange={handleChange} />

          <button className="button--primary" type="submit">
            Login
          </button>
        </form>
        <Link href="/signup">Need an account?</Link>
      </section>
    </MainLayout>
  );
}

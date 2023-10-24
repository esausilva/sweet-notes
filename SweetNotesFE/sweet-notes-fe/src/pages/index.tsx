import { FormEvent, useReducer, useEffect, useState } from 'react';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '@/layout/MainLayout';
import { FormFieldData, LoginForm, ApiError } from '@/type/index';
import { authenticate } from '@/service/authService';
import { useRenderErrorList } from '@/hook/useRenderErrorList';

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
  const [errors, setErrors] = useState({} as ApiError);

  useEffect(() => {
    // TODO: Load cookie, if authenticated, redirect to user admin page
    return () => {};
  }, []);

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (
    event: FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    var result = await authenticate('user/login', {
      emailAddress: formData.email,
      password: formData.password,
    });

    if (result.errors) setErrors(result.errors);

    if (result.status === 200) {
      // TODO: Redirect to user admin page
    }
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

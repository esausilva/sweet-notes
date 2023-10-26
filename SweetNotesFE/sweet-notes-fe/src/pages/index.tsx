import { FormEvent, useReducer, useState } from 'react';
import type { GetServerSideProps } from 'next';
import { useRouter } from 'next/navigation';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '@/layout/MainLayout';
import { FormFieldData, LoginForm, ApiError } from '@/types';
import { authenticate } from '@/service/authService';
import { useRenderErrorList } from '@/hook/useRenderErrorList';
import { AUTH_COOKIE_NAME, USER_ADMIN_ROUTE } from '@/constants';

import styles from './index.module.scss';

export const getServerSideProps = (async context => {
  const authCookie = context.req.cookies[AUTH_COOKIE_NAME];

  if (authCookie) {
    return {
      redirect: {
        destination: USER_ADMIN_ROUTE,
        permanent: false,
      },
    };
  }

  return { props: {} };
}) satisfies GetServerSideProps<{}>;

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
  const router = useRouter();

  const handleChange = (event: FormEvent<HTMLInputElement>): void => {
    const { name, value } = event.currentTarget;
    dispatch({ name, value });
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    var result = await authenticate('user/login', {
      emailAddress: formData.email,
      password: formData.password,
    });

    if (result.status === 200) router.push(USER_ADMIN_ROUTE);
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

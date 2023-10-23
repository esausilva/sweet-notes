import { FormEvent, useReducer, useEffect, useState } from 'react';
import Head from 'next/head';
import Link from 'next/link';

import { MainLayout } from '../app/MainLayout';
import { SignupForm, FormFieldData, ApiError } from './types';
import { authenticate } from '../app/services/authService';
import { useRenderErrorList } from '../app/hooks/useRenderErrorList';

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

    var result = await authenticate('user/signup', {
      firstName: formData.firstName,
      lastName: formData.lastName,
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
        <title>Create an Account | Sweet Notes</title>
      </Head>

      <section id={styles.formWrapper}>
        <form id={styles.loginForm} onSubmit={handleSubmit}>
          <h1>Signup</h1>

          <>{useRenderErrorList(errors)}</>

          <label htmlFor="firstName">First Name</label>
          <input type="text" name="firstName" onChange={handleChange} />

          <label htmlFor="lastName">Last Name</label>
          <input type="text" name="lastName" onChange={handleChange} />

          <label htmlFor="email">Email</label>
          <input type="text" name="email" onChange={handleChange} />

          <label htmlFor="password">Password</label>
          <input type="password" name="password" onChange={handleChange} />

          <button type="submit">Signup</button>
        </form>
        <Link href="/">Already have an account?</Link>
      </section>
    </MainLayout>
  );
}
